using System;

public interface IDamageable
{
    void TakeDamage(int damage);

    public event Action OnTakeDamage;
}
