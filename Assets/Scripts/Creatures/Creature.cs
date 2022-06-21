using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Creature : MonoBehaviour, IDamageable, IHealthy
{
    [SerializeField] private int _maxHealth = 100;

    public event Action OnDie;
    public event Action OnTakeDamage;

    public bool IsAlive => CurentHealth > 0;
    public int CurentHealth { get; private set; }
    public float PartHealth => (float)CurentHealth / _maxHealth;

    private void Awake()
    {
        CurentHealth = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (enabled && IsAlive)
        {
            CurentHealth -= System.Math.Abs(damage);
            OnTakeDamage?.Invoke();
            if (!IsAlive)
            {
                CurentHealth = 0;
                OnDie?.Invoke();
                //Debug.Log("Die " + gameObject.name);
            }
        }
        //Debug.Log($"CurentHealth = {CurentHealth}; PercentageHealth = {PercentageHealth}");
    }
}