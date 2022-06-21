using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameplayEventSystem;

//не знаю как лучше навать этот класс
public class GameLogic : MonoBehaviour
{
    [SerializeField] private Way _way;
    [SerializeField] private Player _player;
    [SerializeField] [Range(0,3)] private float _delayPassedCheckPoint = 0.5f;

    private CheckPoint _currentCheckPoint;
    private bool _playerInCheckPoint = false;
    private bool _enableControl = false;
    private PlayerInput _input;

    private void Awake()
    {
        _input = new PlayerInput();
        _input.Player.Tap.performed += TapToScreen;
    }

    private void Start()
    {
        _currentCheckPoint = _way.GetStartCheckPoint();
        _player.transform.SetPositionAndRotation(_currentCheckPoint.transform.position, _currentCheckPoint.transform.rotation);

        OnPassedCheckPoint.AddListener(NextCheckPoint);
        OnPlayerArrivedInPoint.AddListener((Transform t) => _playerInCheckPoint = true);
        StartCoroutine(TimedDisableControl(_delayPassedCheckPoint));
        StartCoroutine(TimedActions(_delayPassedCheckPoint / 2, NextCheckPoint)); ;
    }

    private void TapToScreen(UnityEngine.InputSystem.InputAction.CallbackContext input)
    {
        Vector2 screenPosition = input.ReadValue<Vector2>();
        Vector3 worldPosition;

        if (_enableControl)
        {
            if (!_playerInCheckPoint)
            {
                SendPlayerMoveNextPoint(_currentCheckPoint.transform);
            }
            else
            {
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
        }
    }

    private IEnumerator TimedDisableControl(float time)
    {
        _enableControl = false;
        yield return new WaitForSeconds(time);
        _enableControl = true;
    }
    
    private IEnumerator TimedActions(float time, System.Action action)
    {
        yield return new WaitForSeconds(time);
        action.Invoke();
    }

    private void NextCheckPoint()
    {
        _playerInCheckPoint = false;
        _currentCheckPoint = _way.GetNextCheckPoint();

        if (_currentCheckPoint != null)
        {
            _currentCheckPoint.enabled = true;
            StartCoroutine(TimedDisableControl(_delayPassedCheckPoint));
        }
        else
        {
            GameplayEventSystem.SendGameOver();
            _enableControl = false;
        }
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
