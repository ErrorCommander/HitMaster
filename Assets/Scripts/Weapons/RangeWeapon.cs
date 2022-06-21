using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RangeWeapon : Weapon
{
    public GameObject Tag => _pool.Tag;

    [SerializeField] private Pool _pool;
    [SerializeField] private Projectile _prefabProjectile;
    [Header("Projectile settings")]
    [SerializeField] [Range(0, 50)] private float _speedProjectile;
    [SerializeField] [Range(0, 20)] private float _lifeTimeProjectile;

    protected Pooler _pooler;

    private void Start()
    {
        _pooler = Pooler.Instance;
        _prefabProjectile.SetParam(_speedProjectile, _lifeTimeProjectile, _damage);
        _pooler.AddPool(_pool, _prefabProjectile.gameObject);
    }
}
