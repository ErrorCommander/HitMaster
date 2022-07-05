using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Frendly : CreatureFromHitBoxes
{
    private Animator _animator;
    private string _animVarIsSafe = "IsSafe";

    public void YouSafe(Transform lookPoint)
    {
        _animator.SetBool(_animVarIsSafe, true);
        _animator.transform.LookAt(lookPoint);
    }

    private void Awake()
    {
        _animator = gameObject.GetComponent<Animator>();
    }
}
