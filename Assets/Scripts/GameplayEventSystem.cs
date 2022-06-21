using UnityEngine.Events;
using UnityEngine;

public static class GameplayEventSystem
{
    public static UnityEvent OnStartGame = new UnityEvent();
    public static UnityEvent OnGameOver = new UnityEvent();
    public static UnityEvent OnPassedCheckPoint = new UnityEvent();
    public static UnityEvent<Transform> OnPlayerMoveNextPoint = new UnityEvent<Transform>();
    public static UnityEvent<Transform> OnPlayerArrivedInPoint = new UnityEvent<Transform>();

    public static void SendStartGame() => OnStartGame?.Invoke();
    public static void SendGameOver() => OnGameOver?.Invoke();
    public static void SendPassedCheckPoint() => OnPassedCheckPoint?.Invoke();
    public static void SendPlayerMoveNextPoint(Transform trf) => OnPlayerMoveNextPoint?.Invoke(trf);
    public static void SendPlayerArrivedInPoint(Transform trf) => OnPlayerArrivedInPoint?.Invoke(trf);
}
