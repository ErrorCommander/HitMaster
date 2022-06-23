using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Text _numberCheckPoint;
    [SerializeField] private Slider _progressInCheckPoint;

    private int _indexer = 0;
    private CheckPoint _currentCheckPoint;

    private void Awake()
    {
        _progressInCheckPoint.normalizedValue = 0;
        _numberCheckPoint.text = "0";
        GameplayEventSystem.OnPlayerMoveNextPoint.AddListener(PlayerMoveNextPoint);
        GameplayEventSystem.OnPlayerMoveNextPoint.AddListener(ResetProgressSlider);
        GameplayEventSystem.OnEnemyDie.AddListener(EnemyDie);
    }

    private void EnemyDie()
    {
        float progress = (_currentCheckPoint.EnemiesCount - _currentCheckPoint.LivingEnemiesCount) / (float)_currentCheckPoint.EnemiesCount;
        _progressInCheckPoint.normalizedValue = progress;
    }

    private void ResetProgressSlider(CheckPoint point)
    {
        _progressInCheckPoint.normalizedValue = 0;
        _currentCheckPoint = point;
    }

    private void PlayerMoveNextPoint(CheckPoint point)
    {
        _indexer++;
        RefreshText();
    }

    private void RefreshText() => _numberCheckPoint.text = _indexer.ToString();
}
