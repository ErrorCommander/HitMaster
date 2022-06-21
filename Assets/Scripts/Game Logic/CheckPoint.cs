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

    private int _enemiesCount = 0;
    private int _frendlyCount = 0;

    public Vector3 GetPosition()
    {
        return _viewPoint.position;
    }

    private void Start()
    {
        RegisterCreatures();
        DeactivateCreatures();
        enabled = false;
    }

    private void RegisterCreatures()
    {
        RegisterEnemys();
        RegisterFrendly();
    }

    private void RegisterEnemys()
    {
        _enemiesCount = Register(_enemies, DieEnemy);
    }

    private void RegisterFrendly()
    {
        _frendlyCount = Register(_frendly, DieFrendly);
    }

    private int Register(List<Creature> creatures, Action listener)
    {
        int counter = 0;

        foreach (var creature in creatures)
            if (creature.IsAlive)
            {
                counter++;
                creature.OnDie -= listener;
                creature.OnDie += listener;
            }

        return counter;
    }

    private void DieEnemy()
    {
        _enemiesCount--;

        if (_enemiesCount <= 0)
        {
            RegisterEnemys();
            if (_enemiesCount <= 0)
                GameplayEventSystem.SendPassedCheckPoint();
        }
    }

    private void DieFrendly()
    {
        _frendlyCount--;

        if (_frendlyCount <= 0)
        {
            RegisterFrendly();
            if (_frendlyCount <= 0)
                Debug.Log("so why did you kill everyone?");
        }
        else
            Debug.Log("don't Attack peaceful!");
    }

    private void OnEnable()
    {
        ActivateCreatures();
    }

    private void OnDisable()
    {
        DeactivateCreatures();
    }

    private void ActivateCreatures()
    {
        foreach (var creature in _enemies)
            creature.enabled = true;

        foreach (var creature in _frendly)
            creature.enabled = true;
    }

    private void DeactivateCreatures()
    {
        foreach (var creature in _enemies)
            creature.enabled = false;

        foreach (var creature in _frendly)
            creature.enabled = false;
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
