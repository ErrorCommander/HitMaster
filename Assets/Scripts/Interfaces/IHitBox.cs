using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHitBox
{
    public float MultiplierDamage { get; set; }
    IDamageable ParentDamageable { get; set; }
}
