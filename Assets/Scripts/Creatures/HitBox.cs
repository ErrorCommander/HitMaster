using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour, IDamageable, IHitBox
{
    public float MultiplierDamage { get; set; }
    public IDamageable ParentDamageable { get; set; }

    public event Action OnTakeDamage
    {
        add => ParentDamageable.OnTakeDamage += value;
        remove => ParentDamageable.OnTakeDamage -= value;
    }

    public void TakeDamage(int damage)
    {
        damage = System.Math.Abs(damage);
        damage = (int)System.Math.Round(damage * MultiplierDamage);
        //Debug.Log("Total damage: " + damage);
        ParentDamageable.TakeDamage(damage);
    }
}

[System.Serializable]
public class HitBoxParameters
{
    [SerializeField] private string _name;
    [SerializeField] private HitBox _hitBox;
    [SerializeField] private float _multiplierDamage;

    public string Name => _name;
    public IHitBox HitBox => _hitBox;
    public float MultiplierDamage => _multiplierDamage;
}