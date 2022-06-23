using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameplayEventSystem;


public class PlayerBehavior : MonoBehaviour
{
    [SerializeField] private Way _way;
    [SerializeField] private Player _player;
    [SerializeField] [Range(0,3)] private float _delayAfterPassedCheckPoint = 0.5f;

    private bool _readyToAttack = false;
    private bool _enableControl = false;
    private PlayerInput _input;

    private void Awake()
    {
        _input = new PlayerInput();
        _input.Player.Tap.performed += FirstTap;
    }

    private void Start()
    {
        CheckPoint currentCheckPoint = _way.GetStartCheckPoint();
        _player.transform.SetPositionAndRotation(currentCheckPoint.transform.position, currentCheckPoint.transform.rotation);

        OnPlayerArrivedInPoint.AddListener((Transform t) => _readyToAttack = _enableControl = true);
        OnPassedCheckPoint.AddListener((point) => {
            _readyToAttack = false;
            StartCoroutine(TimedDisableControl(_delayAfterPassedCheckPoint));  });
        OnGameOver.AddListener(() => _enableControl = false);
    }

    private void FirstTap(UnityEngine.InputSystem.InputAction.CallbackContext input)
    {
        _input.Player.Tap.performed -= FirstTap;
        _input.Player.Tap.performed += TapToScreen;
        _enableControl = true;
        SendStartGame();
        PlayerMoveToPoint();
    }

    private void TapToScreen(UnityEngine.InputSystem.InputAction.CallbackContext input)
    {
        Vector2 screenPosition = input.ReadValue<Vector2>();

        if (_enableControl)
        {
            if (_readyToAttack)
                PlayerAttack(screenPosition);
            else
                PlayerMoveToPoint();
        }
    }

    private void PlayerMoveToPoint()
    {
        var point = _way.GetNextCheckPoint();
        if (point == null)
            SendGameOver();
        else
            SendPlayerMoveNextPoint(point.transform);

        _enableControl = false;
    }

    private void PlayerAttack(Vector2 screenPosition)
    {
        Vector3 worldPosition;
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            //Debug.Log(hit.transform.name);
            worldPosition = hit.point;
        }
        else
        {
            worldPosition = new Vector3(screenPosition.x, screenPosition.y, 30f);
            worldPosition = Camera.main.ScreenToWorldPoint(worldPosition);
        }

        Debug.DrawLine(Camera.main.transform.position, worldPosition, Color.green, 10f);
        _player.Attack(worldPosition);
    }
       
    private IEnumerator TimedDisableControl(float time)
    {
        _enableControl = false;
        yield return new WaitForSeconds(time);
        _enableControl = true;
    }

    private void OnEnable()
    {
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
    }
}
