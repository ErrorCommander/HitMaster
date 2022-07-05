using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrendlyAnimIsSafe : MonoBehaviour
{
    [SerializeField] private CheckPoint _checkPoint;
    [SerializeField] private List<Animator> _animators;

    private string _animVarIsSafe = "IsSafe";

    private void Start()
    {
        _checkPoint.CheckPointPassed += SafeAnimation;
    }

    private void SafeAnimation()
    {
        _checkPoint.CheckPointPassed -= SafeAnimation;
        foreach (var amin in _animators)
        {
            amin.SetBool(_animVarIsSafe, true);
            amin.transform.LookAt(_checkPoint.position);
        }
    }
}
