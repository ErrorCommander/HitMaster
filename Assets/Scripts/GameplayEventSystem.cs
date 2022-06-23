using UnityEngine.Events;
using UnityEngine;

public static class GameplayEventSystem
{
    public static UnityEvent OnStartGame = new UnityEvent();
    public static UnityEvent OnGameOver = new UnityEvent();
    public static UnityEvent<Transform> OnPassedCheckPoint = new UnityEvent<Transform>();
    public static UnityEvent<Transform> OnPlayerMoveNextPoint = new UnityEvent<Transform>();
    public static UnityEvent<Transform> OnPlayerArrivedInPoint = new UnityEvent<Transform>();

    public static void SendStartGame() => OnStartGame?.Invoke();
    public static void SendGameOver() => OnGameOver?.Invoke();
    public static void SendPassedCheckPoint(Transform transformPoint) => OnPassedCheckPoint?.Invoke(transformPoint);
    public static void SendPlayerMoveNextPoint(Transform transformPoint) => OnPlayerMoveNextPoint?.Invoke(transformPoint);
    public static void SendPlayerArrivedInPoint(Transform transformPoint) => OnPlayerArrivedInPoint?.Invoke(transformPoint);
}
