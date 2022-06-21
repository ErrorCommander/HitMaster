using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected int _damage;

    public abstract void Attack(Vector3 point);
}
