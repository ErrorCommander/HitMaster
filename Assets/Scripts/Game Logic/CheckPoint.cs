using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private Transform _viewPoint;
    [SerializeField] private List<Creature> _enemies;
    [SerializeField] private List<Creature> _frendly;

    public bool IsPassed => _enemiesCount <= 0;
    public event Action CheckPointPassed;
    public event Action EnemyKilled;
    public event Action FrendlyKilled;
    
    private int _enemiesCount = 0;
    private int _frendlyCount = 0;

    public Vector3 GetPosition()
    {
        return _viewPoint.position;
    }

    private void Start()
    {
        VerifyCreatures();
        EnableCreatures(false);
        enabled = false;
    }

    private void VerifyCreatures()
    {
        VerifyEnemys();
        VerifyFrendly();
    }

    private void VerifyEnemys()
    {
        _enemiesCount = VerifyList(_enemies, DieEnemy);
    }

    private void VerifyFrendly()
    {
        _frendlyCount = VerifyList(_frendly, DieFrendly);
    }

    private int VerifyList(List<Creature> creatures, Action listener)
    {
        int counter = 0;

        foreach (var creature in creatures)
        {
            creature.OnDie -= listener;
            if (creature.IsAlive)
            {
                counter++;
                creature.OnDie += listener;
            }
        }

        return counter;
    }

    private void DieEnemy()
    {
        _enemiesCount--;
        EnemyKilled?.Invoke();

        if (_enemiesCount <= 0)
        {
            VerifyEnemys();
            if (_enemiesCount <= 0)
            {
                CheckPointPassed?.Invoke();
                GameplayEventSystem.SendPassedCheckPoint();
            }
        }
    }

    private void DieFrendly()
    {
        _frendlyCount--;
        FrendlyKilled?.Invoke();

        if (_frendlyCount <= 0)
        {
            VerifyFrendly();
            if (_frendlyCount <= 0)
                Debug.Log("so why did you kill everyone?");
        }
        else
            Debug.Log("don't Attack peaceful!");
    }

    private void OnEnable()
    {
        EnableCreatures(true);
    }

    private void OnDisable()
    {
        EnableCreatures(false);
    }

    private void EnableCreatures(bool state)
    {
        foreach (var creature in _enemies)
            creature.enabled = state;

        foreach (var creature in _frendly)
            creature.enabled = state;
    }

    private void OnApplicationQuit()
    {
        _enemies.Clear();
        _frendly.Clear();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + transform.forward);
        Gizmos.DrawSphere(transform.position, 0.3f);
    }
}
