using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Cinemachine.CinemachineVirtualCamera _virtualCamera;

    private void Start()
    {
        _virtualCamera.enabled = true;
        _player.OnArrivedInPoint += SetTargetViewPoint;
        _player.OnMove += SetTargetPlayer;
    }

    private void SetTargetViewPoint()
    {
        _virtualCamera.enabled = false;
    }

    private void SetTargetPlayer()
    {
        _virtualCamera.enabled = true;
    }
}
