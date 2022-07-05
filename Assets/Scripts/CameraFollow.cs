using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Cinemachine.CinemachineVirtualCamera _virtualCamera;
    [SerializeField] float _delayToViewPoint;
    [SerializeField] float _delayToPlayer;

    //private void Start()
    //{
    //    GameplayEventSystem.OnPlayerMoveNextPoint.AddListener(SetTargetPlayer);
    //    _player.OnArrivedInPoint += SetTargetViewPoint;
    //}

    //private void SetTargetViewPoint()
    //{
    //    StartCoroutine(SetTargetWait(viewPoint.transform, _delayToViewPoint));
    //    //_virtualCamera.Follow = viewPoint;
    //}

    //private void SetTargetPlayer(CheckPoint viewPoint)
    //{
    //    StartCoroutine(SetTargetWait(_player.transform, _delayToPlayer));
    //}

    //private IEnumerator SetTargetWait(Transform viewPoint, float delay)
    //{
    //    yield return new WaitForSeconds(delay);
    //    _virtualCamera.Follow = viewPoint;
    //}
}
