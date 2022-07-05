using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Player))]
public class PlayerAnimation : MonoBehaviour
{
    private Animator _animator;
    private Player _player;
    private string _animFire = "Fire";
    private string _animFloatSpeed = "Speed";
    private string _animBoolIsVictory = "IsVictory";

    private void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
        _player = gameObject.GetComponent<Player>();
        _player.OnFire += Fire;
        GlobalEventSystem.OnGameOver.AddListener(Victory);
    }

    public void Fire()
    {
        _animator.Play(_animFire, 0);
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
