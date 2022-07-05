using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _camera;
    [SerializeField] private Transform _viewPoint;
    [SerializeField] private List<Creature> _enemies;
    [SerializeField] private List<Frendly> _frendly;

    public int EnemiesCount => _enemies.Count;
    public int FrendlyCount => _frendly.Count;
    public int LivingEnemiesCount { get; private set; }
    public int LivingFrendlyCount { get; private set; }
    public Vector3 position => _viewPoint.position;
    public bool IsPassed => LivingEnemiesCount <= 0;

    public event Action CheckPointPassed;
    public event Action EnemyDie;
    public event Action FrendlyDie;

    public void EnableCamera(bool enable)
    {
        if (_camera != null)
            _camera.enabled = enable;
    }

    private void Start()
    {
        VerifyCreatures();
        enabled = false;
    }

    private void VerifyCreatures()
    {
        VerifyEnemys();
        VerifyFrendly();
    }

    private void VerifyEnemys()
    {
        LivingEnemiesCount = VerifyList(_enemies, DieEnemy);
    }

    private void VerifyFrendly()
    {
        LivingFrendlyCount = VerifyList(_frendly, DieFrendly);
    }

    private int VerifyList<TValue>(List<TValue> creatures, Action listener) where TValue : Creature
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
        LivingEnemiesCount--;
        EnemyDie?.Invoke();

        if (LivingEnemiesCount <= 0)
        {
            VerifyEnemys();
            if (LivingEnemiesCount <= 0)
            {
                CheckPointPassed?.Invoke();
                enabled = false;

                foreach (var unit in _frendly)
                {
                    if (unit.IsAlive)
                    {
                        unit.InSafety(_viewPoint);
                    }
                }
            }
        }
    }

    private void DieFrendly()
    {
        LivingFrendlyCount--;
        FrendlyDie?.Invoke();

        if (LivingFrendlyCount <= 0)
        {
            VerifyFrendly();
            if (LivingFrendlyCount <= 0)
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
        if(_camera!=null)
            _camera.enabled = false;
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
