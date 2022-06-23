using UnityEngine.Events;
using UnityEngine;

public static class GameplayEventSystem
{
    public static UnityEvent OnStartGame = new UnityEvent();
    public static UnityEvent OnGameOver = new UnityEvent();
    public static UnityEvent OnEnemyDie = new UnityEvent();
    public static UnityEvent OnFrendlyDie = new UnityEvent();
    public static UnityEvent<CheckPoint> OnPassedCheckPoint = new UnityEvent<CheckPoint>();
    public static UnityEvent<CheckPoint> OnPlayerMoveNextPoint = new UnityEvent<CheckPoint>();
    public static UnityEvent<CheckPoint> OnPlayerArrivedInPoint = new UnityEvent<CheckPoint>();

    public static void SendStartGame() => OnStartGame?.Invoke();
    public static void SendGameOver() => OnGameOver?.Invoke();
    public static void SendEnemyDie() => OnEnemyDie?.Invoke();
    public static void SendFrendlyDie() => OnFrendlyDie?.Invoke();
    public static void SendPassedCheckPoint(CheckPoint point) => OnPassedCheckPoint?.Invoke(point);
    public static void SendPlayerMoveNextPoint(CheckPoint point) => OnPlayerMoveNextPoint?.Invoke(point);
    public static void SendPlayerArrivedInPoint(CheckPoint point) => OnPlayerArrivedInPoint?.Invoke(point);
}
