using UnityEngine;

public class SetActiveOnStartGame : MonoBehaviour
{
    [SerializeField] private bool _setActive;

    private void Awake()
    {
        gameObject.SetActive(!_setActive);
        GlobalEventSystem.Instance.OnStartGame.AddListener(ChangeActive);
    }

    private void ChangeActive()
    {
        gameObject.SetActive(_setActive);
        GlobalEventSystem.Instance.OnStartGame.RemoveListener(ChangeActive);
    }
}
