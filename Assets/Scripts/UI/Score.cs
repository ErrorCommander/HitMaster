using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public int CurrentScore => _startScore;
    public event System.Action<int> ChangeValue;
    [SerializeField] private int _startScore = 0;
    [SerializeField] private int _costEnemy = 35;
    [SerializeField] private int _costFrendly = 50;

    private void OnEnable()
    {
        //    GameplayEventSystem.OnEnemyDie.AddListener(DieEnemy);
        //    GameplayEventSystem.OnFrendlyDie.AddListener(DieFrendly);
    }

    private void OnDisable()
    {
        //    GameplayEventSystem.OnEnemyDie.RemoveListener(DieEnemy);
        //    GameplayEventSystem.OnFrendlyDie.RemoveListener(DieFrendly);
    }

    private void DieEnemy()
    {
        _startScore += _costEnemy;
        ChangeValue?.Invoke(_costEnemy);
    }

    private void DieFrendly()
    {
        _startScore += _costFrendly;
        ChangeValue?.Invoke(_costFrendly);
    }
}
