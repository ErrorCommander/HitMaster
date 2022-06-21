using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Creature))]
[RequireComponent(typeof(Animator))]
public class CreatureAnimationTakeDamage : MonoBehaviour
{
    private Creature _creature;
    private Animator _animator;
    private string _animTakeDamage = "Apply Damade";

    private void Awake()
    {
        _creature = gameObject.GetComponent<Creature>();
        _animator = gameObject.GetComponent<Animator>();

        _creature.OnTakeDamage += TakeDamage;
    }

    private void TakeDamage()
    {
        _animator.Play(_animTakeDamage);
    }
}
