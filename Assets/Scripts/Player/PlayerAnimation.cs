using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Player))]
public class PlayerAnimation : MonoBehaviour
{
    private Animator _animator;
    private Player _player;
    private int _animFire = Animator.StringToHash("Fire");
    private int _animFloatSpeed = Animator.StringToHash("Speed");
    private int _animBoolIsVictory = Animator.StringToHash("IsVictory");

    private void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
        _player = gameObject.GetComponent<Player>();
        _player.OnFire += Fire;
        GlobalEventSystem.OnGameOver.AddListener(Victory);
    }

    public void Fire()
    {
        _animator.Play(_animFire);
    }

    private void Update()
    {
        _animator.SetFloat(_animFloatSpeed, _player.Speed);
    }

    private void Victory()
    {
        _animator.SetBool(_animBoolIsVictory, true);
    }
}
