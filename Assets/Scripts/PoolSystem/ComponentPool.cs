using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ComponentPool<TValue>  where TValue : Component
{
    [SerializeField] private TValue _prefab;
    [SerializeField] private int _initPoolSize;

    public TValue Tag => _prefab;
    public int PoolSize => _queueComponents == null ? _initPoolSize : _queueComponents.Count;

    private Queue<TValue> _queueComponents;
    private Transform _parentPool;
    private ushort _componentID = 0;

    public ComponentPool(TValue prefab, int poolSize)
    {
        _prefab = prefab;
        _initPoolSize = poolSize;
    }

    public void Initialize()
    {
        _queueComponents = new Queue<TValue>();
        _parentPool = new GameObject("Pool: " + _prefab.gameObject.name).transform;
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
        MonoBehaviour.Destroy(_parentPool.gameObject);
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
        TValue component = MonoBehaviour.Instantiate<TValue>(_prefab);
        component.gameObject.SetActive(false);
        component.gameObject.name = string.Format("{0} {1:000}", _prefab.name, _componentID++);

        return component;
    }
}
