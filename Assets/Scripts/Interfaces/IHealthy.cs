using System;

public interface IHealthy
{
    int CurentHealth { get; }
    float PartHealth { get; }

    public event Action OnDie;
}
