using UnityEngine.Events;
using UnityEngine;

public static class GlobalEventSystem
{
    public static UnityEvent OnStartGame = new UnityEvent();
    public static UnityEvent OnGameOver = new UnityEvent();

    public static void SendStartGame() => OnStartGame?.Invoke();
    public static void SendGameOver() => OnGameOver?.Invoke();
}
