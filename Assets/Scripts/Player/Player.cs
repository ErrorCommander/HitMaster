using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static GameplayEventSystem;

[RequireComponent(typeof(NavMeshAgent))]

public class Player : MonoBehaviour, IMovable
{
    [SerializeField] private Weapon _weapon;
    [SerializeField] private Transform _fixedPointWeapon;
    [SerializeField] private Vector3 _offsetPosition;
    [SerializeField] private Vector3 _offsetRotation;

    public float Speed => _agent == null ? 0 : _agent.speed;
    public event Action OnFire;

    private NavMeshAgent _agent;
    private const float _marginError = 0.3f;

    private void Start()
    {
        _agent = gameObject.GetComponent<NavMeshAgent>();
        if (_weapon != null)
        {
            _weapon = Instantiate(_weapon);
            _weapon.transform.parent = _fixedPointWeapon;
            _weapon.transform.localPosition = _offsetPosition;
            _weapon.transform.localRotation = Quaternion.Euler(_offsetRotation);
        }

        OnPlayerMoveNextPoint.AddListener(MoveToNextPoint);
    }

    public void MoveTo(Vector3 position)
    {
        _agent.destination = position;
    }

    public void MoveToNextPoint(Transform point)
    {
        MoveTo(point.position);
        StartCoroutine(CheckArrivedInPoint(point));
    }

    public void Attack(Vector3 point)
    {
        if (_weapon == null)
        {
            Debug.LogWarning("weapon not equipped");
            return;
        }

        OnFire?.Invoke();
        TurnToPoint(point);
        StartCoroutine(ShootEndOfFrame(point));
    }

    private void TurnToPoint(Vector3 point)
    {
        transform.rotation = Quaternion.LookRotation(point - transform.position);
    }

    private IEnumerator ShootEndOfFrame(Vector3 point)
    {
        yield return new WaitForEndOfFrame();
        _weapon.Attack(point);
    }

    private IEnumerator CheckArrivedInPoint(Transform target)
    {
        yield return new WaitWhile(() => (target.position - transform.position).magnitude > _marginError);
        SendPlayerArrivedInPoint(target);
    }
}