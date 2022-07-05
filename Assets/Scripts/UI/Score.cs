using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public int CurrentScore => _startScore;
    public event System.Action<int> ChangeValue;
    [SerializeField] private Player _player;
    [SerializeField] private int _startScore = 0;
    [SerializeField] private int _costEnemy = 35;
    [SerializeField] private int _costFrendly = 50;

    private CheckPoint _checkPoint;

    private void OnEnable()
    {
        _player.OnMoveToCheckPoint += Reg;
    }

    private void OnDisable()
    {
        _player.OnMoveToCheckPoint -= Reg;

        if (_checkPoint != null)
        {
            _checkPoint.EnemyDie -= EnemyKilled;
            _checkPoint.FrendlyDie -= FrendlyKilled;
        }
    }

    private void Reg(CheckPoint checkPoint)
    {
        _checkPoint = checkPoint;
        checkPoint.EnemyDie += EnemyKilled;
        checkPoint.FrendlyDie += FrendlyKilled;
    }

    private void EnemyKilled()
    {
        _startScore += _costEnemy;
        ChangeValue?.Invoke(_costEnemy);
    }

    private void FrendlyKilled()
    {
        _startScore += _costFrendly;
        ChangeValue?.Invoke(_costFrendly);
    }
}
