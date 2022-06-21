using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IHealthy))]
public class ActivateRagdollOnDie : MonoBehaviour
{
    private IHealthy _body;
    [SerializeField] private Animator _animator;

    private void Awake()
    {
        _body = gameObject.GetComponent<IHealthy>();
        _body.OnDie += Die;
    }

    private void Die()
    {
        _animator.enabled = false;
    }
}
