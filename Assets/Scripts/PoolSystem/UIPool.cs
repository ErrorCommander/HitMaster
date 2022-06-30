using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UIPool<TValue>  where TValue : Component
{
    [SerializeField] private TValue _prefab;
    [SerializeField] private int _initPoolSize;
    [SerializeField] private RectTransform _parent;

    public TValue Tag => _prefab;
    public int PoolSize => _queueComponents == null ? _initPoolSize : _queueComponents.Count;

    private Queue<TValue> _queueComponents;
    private ushort _componentID = 0;

    public UIPool(TValue prefab, RectTransform parent, int poolSize)
    {
        _prefab = prefab;
        _initPoolSize = poolSize;
        _parent = parent;
    }

    public void Initialize()
    {
        _queueComponents = new Queue<TValue>();
        FillPool();
    }

    public void ChangePoolSize(int poolSize)
    {
        if (_queueComponents.Count >= poolSize)
            return;

        _initPoolSize = poolSize;
        FillPool();
    }

    public TValue TakeGameObject()
    {
        TValue result = null;

        if (_queueComponents.Peek().gameObject.activeSelf)
            result = AddGameObject();
        else
            result = _queueComponents.Dequeue();

        _queueComponents.Enqueue(result);
        result.gameObject.SetActive(true);
        return result;
    }

    public void ClearPool()
    {
        _initPoolSize = 0;
        foreach (var go in _queueComponents)
            MonoBehaviour.Destroy(go);

        _queueComponents.Clear();
    }

    public void DestroyPool()
    {
        ClearPool();
    }

    private void FillPool()
    {
        if (_queueComponents.Count >= _initPoolSize)
            return;

        for (int i = _queueComponents.Count; i < _initPoolSize; i++)
            _queueComponents.Enqueue(AddGameObject());
    }

    private TValue AddGameObject()
    {
        TValue component = MonoBehaviour.Instantiate(_prefab);
        component.transform.SetParent(_parent, false);
        component.gameObject.SetActive(false);
        component.gameObject.name = string.Format("{0} {1:000}", _prefab.name, _componentID++);

        return component;
    }
}
