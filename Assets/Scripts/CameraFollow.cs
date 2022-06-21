using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private Cinemachine.CinemachineVirtualCamera _virtualCamera;
    [SerializeField] float _delayToViewPoint;
    [SerializeField] float _delayToPlayer;

    private void Start()
    {
        GameplayEventSystem.OnPlayerMoveNextPoint.AddListener(SetTargetPlayer);
        GameplayEventSystem.OnPlayerArrivedInPoint.AddListener(SetTargetViewPoint);
    }

    private void SetTargetViewPoint(Transform viewPoint)
    {
        StartCoroutine(SetTargetWait(viewPoint, _delayToViewPoint));
        //_virtualCamera.Follow = viewPoint;
    }

    private void SetTargetPlayer(Transform viewPoint)
    {
        StartCoroutine(SetTargetWait(_player, _delayToPlayer));
    }

    private IEnumerator SetTargetWait(Transform viewPoint, float delay)
    {
        yield return new WaitForSeconds(delay);
        _virtualCamera.Follow = viewPoint;
    }
}
