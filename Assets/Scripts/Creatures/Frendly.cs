using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Frendly : CreatureFromHitBoxes
{
    private Animator _animator;
    private int _animVarIsSafe = Animator.StringToHash("IsSafe");

    public void InSafety(Transform lookPoint)
    {
        _animator.SetBool(_animVarIsSafe, true);
        _animator.transform.LookAt(lookPoint);
    }

    private void Awake()
    {
        _animator = gameObject.GetComponent<Animator>();
    }
}
