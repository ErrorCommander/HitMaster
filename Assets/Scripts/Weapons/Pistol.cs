using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : RangeWeapon
{
    [SerializeField] private Vector3 _offsetSpawnPositionProjectile;

    public override void Attack(Vector3 point)
    {
        Vector3 pos = transform.position + _offsetSpawnPositionProjectile;
        GameObject projectile = _pooler.Spawn(Tag, pos, Quaternion.LookRotation(point - pos));
        Debug.DrawLine(transform.position, point, Color.red, 10f);
    }
}
