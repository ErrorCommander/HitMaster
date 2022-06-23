using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] [Range(0, 100)] private int _damage = 5;
    [field: SerializeField] public float Speed { get; private set; }
    [SerializeField] [Range(0, 25)] private float _lifeTime = 10;

    private float _timer = 0;

    public Projectile(float speed, float lifeTime, int damage)
    {
        SetParam(speed, lifeTime, damage);
    }

    public void SetParam(float speed, float lifeTime, int damage)
    {
        Speed = speed;
        _damage = damage;
        _lifeTime = lifeTime;
    }

    public void OnEnable()
    {
        _timer = 0;
    }

    private void FixedUpdate()
    {
        transform.position += Speed * Time.fixedDeltaTime * transform.forward.normalized;

        _timer += Time.fixedDeltaTime;
        if (_timer >= _lifeTime)
            gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<IDamageable>(out IDamageable damageable))
        {
            damageable.TakeDamage(_damage);
        }

        gameObject.SetActive(false);
    }
}
