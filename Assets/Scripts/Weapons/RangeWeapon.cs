using UnityEngine;

public abstract class RangeWeapon : Weapon
{
    public GameObject Tag => _pool.Tag;
        
    [SerializeField] private Pool _pool;
    [Header("Projectile settings")]
    [SerializeField] [Range(0, 50)] private float _speedProjectile;
    [SerializeField] [Range(0, 20)] private float _lifeTimeProjectile;

    private Projectile _prefabProjectile;
    protected Pooler _pooler;

    private void Start()
    {
        _pooler = Pooler.Instance;
        _prefabProjectile = _pool.Tag.GetComponent<Projectile>() ?? _pool.Tag.AddComponent<Projectile>();
        _prefabProjectile.SetParam(_speedProjectile, _lifeTimeProjectile, Damage);
        _pooler.AddPool(_pool, _prefabProjectile.gameObject);
    }
}
