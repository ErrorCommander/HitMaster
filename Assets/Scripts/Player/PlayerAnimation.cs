using UnityEngine;
using static GameplayEventSystem;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Player))]

public class PlayerAnimation : MonoBehaviour
{
    private Animator _animator;
    private Player _player;
    private string _animFire = "Fire";
    private string _animFloatSpeed = "Speed";
    private string _animBoolIsVictory = "IsVictory";

    private void Awake()
    {
        OnPlayerMoveNextPoint.AddListener(NextPoint);
        OnPlayerArrivedInPoint.AddListener(ArrivedInPoint);
        OnGameOver.AddListener(Victory);
    }

    private void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
        _player = gameObject.GetComponent<Player>();
        _player.OnFire += Fire;
    }
    public void Fire()
    {
        _animator.Play(_animFire, 0);
    }

    private void NextPoint(Transform point)
    {
        _animator.SetFloat(_animFloatSpeed, _player.Speed);
    }

    private void ArrivedInPoint(Transform point)
    {
        _animator.SetFloat(_animFloatSpeed, 0);
    }

    private void Victory()
    {
        _animator.SetBool(_animBoolIsVictory, true);
    }
}
