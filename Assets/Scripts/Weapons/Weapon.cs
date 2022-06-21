using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [field: SerializeField] public int Damage { get; private set; }

    public abstract void Attack(Vector3 point);
}
