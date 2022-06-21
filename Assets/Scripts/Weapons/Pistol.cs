using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Pistol : RangeWeapon
{
    [SerializeField] private Transform _localPointTpawtProjectile;

    public override void Attack(Vector3 point)
    {
        Vector3 pos = transform.position + _localPointTpawtProjectile.localPosition;
        GameObject projectile = _pooler.Spawn(Tag, pos, Quaternion.LookRotation(point - pos));
        Debug.DrawLine(transform.position, point, Color.red, 10f);
    }
}
