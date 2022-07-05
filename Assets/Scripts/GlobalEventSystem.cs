using UnityEngine.Events;
using UnityEngine;

public class GlobalEventSystem
{
    public static GlobalEventSystem Instance => _instance ??= new GlobalEventSystem();
    private static GlobalEventSystem _instance;
    private GlobalEventSystem() { }

    public UnityEvent OnStartGame = new UnityEvent();
    public UnityEvent OnGameOver = new UnityEvent();

    public void SendStartGame() => OnStartGame?.Invoke();
    public void SendGameOver() => OnGameOver?.Invoke();
}
