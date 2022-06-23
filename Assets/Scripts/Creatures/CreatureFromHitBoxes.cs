using System.Collections.Generic;
using UnityEngine;

public class CreatureFromHitBoxes : Creature
{
    [SerializeField] private List<HitBoxParameters> _hitBoxes;

    private void Start()
    {
        foreach (var hitBox in _hitBoxes)
        {
            hitBox.HitBox.ParentDamageable = this;
            hitBox.HitBox.MultiplierDamage = hitBox.MultiplierDamage;
        }
    }
}