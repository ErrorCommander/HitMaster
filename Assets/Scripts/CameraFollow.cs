using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Cinemachine.CinemachineVirtualCamera _virtualCamera;

    private CheckPoint _checkPoint;

    private void Start()
    {
        _virtualCamera.enabled = true;
        _player.OnArrivedInPoint += SetTargetViewPoint;
        _player.OnMoveToCheckPoint += SetTargetPlayer;
    }

    private void SetTargetViewPoint()
    {
        _virtualCamera.enabled = false;
    }

    private void SetTargetPlayer(CheckPoint point)
    {
        if(_checkPoint != null)
            _checkPoint.EnableCamera(false);

        _checkPoint = point;
        point.EnableCamera(true);
        _virtualCamera.enabled = true;
    }
}
