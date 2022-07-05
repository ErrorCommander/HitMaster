using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Player : MonoBehaviour, IMovable
{
    [SerializeField] private Weapon _weapon;
    [SerializeField] private Transform _fixedPointWeapon;
    [SerializeField] private Vector3 _offsetPosition;
    [SerializeField] private Vector3 _offsetRotation;

    public float Speed => _agent == null ? 0 : _agent.velocity.magnitude;
    public event Action OnFire;
    public event Action OnArrivedInPoint;
    public event Action<CheckPoint> OnMoveToCheckPoint;

    private NavMeshAgent _agent;

    private void Start()
    {
        _agent = gameObject.GetComponent<NavMeshAgent>();
        SetWeapon(_weapon);
    }

    public void SetWeapon(Weapon weapon)
    {
        if (_weapon.gameObject.scene.isLoaded)
            Destroy(_weapon.gameObject);

        if (weapon == null)
        {
            _weapon = null;
            return;
        }

        _weapon = Instantiate(weapon);
        _weapon.transform.parent = _fixedPointWeapon;
        _weapon.transform.localPosition = _offsetPosition;
        _weapon.transform.localRotation = Quaternion.Euler(_offsetRotation);
    }

    public void MoveTo(Vector3 position)
    {
        _agent.destination = position;
    }

    public void MoveToCheckPoint(CheckPoint point)
    {
        MoveTo(point.position);
        OnMoveToCheckPoint?.Invoke(point);
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

    private IEnumerator CheckArrivedInPoint(CheckPoint target)
    {
        yield return null;
        yield return new WaitWhile(() => _agent.hasPath);
        OnArrivedInPoint?.Invoke();
    }
}