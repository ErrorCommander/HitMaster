using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Text _numberCheckPoint;
    [SerializeField] private Slider _progressInCheckPoint;
    [SerializeField] [Range(0,5)] private float _animationSliderSpeed = 0.2f;

    private int _indexer = 0;
    private CheckPoint _currentCheckPoint;
    private const float ReverseAnimationSpeed = 7f;

    private void OnEnable()
    {
        _progressInCheckPoint.normalizedValue = 0;
        _numberCheckPoint.text = "0";
        _player.OnMoveToCheckPoint += NextCheckPoint;
    }

    private void OnDisable()
    {
        _player.OnMoveToCheckPoint -= NextCheckPoint;
    }

    private void NextCheckPoint(CheckPoint checkPoint)
    {
        ResetProgressSlider();
        _indexer++;
        RefreshText();

        if(_currentCheckPoint != null)
            _currentCheckPoint.EnemyDie -= EnemyKilled;

        _currentCheckPoint = checkPoint;
        checkPoint.EnemyDie += EnemyKilled;
    }

    private void EnemyKilled()
    {
        float progress = (_currentCheckPoint.EnemiesCount - _currentCheckPoint.LivingEnemiesCount) / (float)_currentCheckPoint.EnemiesCount;
        StartCoroutine(AmimationProgressSlider(progress, _animationSliderSpeed));
    }

    private IEnumerator AmimationProgressSlider(float progress, float speed)
    {
        float value;
        value = _progressInCheckPoint.normalizedValue + speed * Time.deltaTime;

        while (value < progress)
        {
            _progressInCheckPoint.normalizedValue = value;
            yield return null;
            value = _progressInCheckPoint.normalizedValue + speed * Time.deltaTime;
        }

        _progressInCheckPoint.normalizedValue = progress;
    }

    private IEnumerator AmimationProgressSliderToZero()
    {
        float value;
        value = _progressInCheckPoint.normalizedValue - ReverseAnimationSpeed * Time.deltaTime;

        while (value > 0)
        {
            _progressInCheckPoint.normalizedValue = value;
            yield return null;
            value = _progressInCheckPoint.normalizedValue - ReverseAnimationSpeed * Time.deltaTime;
        }

        _progressInCheckPoint.normalizedValue = 0;
    }

    private void ResetProgressSlider()
    {
        StopAllCoroutines();
        StartCoroutine(AmimationProgressSliderToZero());
    }

    private void RefreshText() => _numberCheckPoint.text = _indexer.ToString();
}
