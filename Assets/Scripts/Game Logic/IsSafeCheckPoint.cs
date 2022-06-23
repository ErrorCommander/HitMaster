using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsSafeCheckPoint : MonoBehaviour
{
    [SerializeField] private CheckPoint _checkPoint;
    [SerializeField] private List<Animator> _animators;

    private string _animVarIsSafe = "IsSafe";

    private void Start()
    {
        _checkPoint.CheckPointPassed += SetInAnimators;
    }

    private void SetInAnimators()
    {
        _checkPoint.CheckPointPassed -= SetInAnimators;
        foreach (var amin in _animators)
        {
            amin.SetBool(_animVarIsSafe, true);
            amin.transform.LookAt(_checkPoint.position);
        }
    }
}
