using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Text _numberCheckPoint;
    [SerializeField] private Slider _progressInCheckPoint;
    [SerializeField] [Range(0,5)] private float _animationSliderSpeed = 0.2f;

    private int _indexer = 0;
    private CheckPoint _currentCheckPoint;
    private const float ReverseAnimationSpeed = 7f;

    private void Awake()
    {
        _progressInCheckPoint.normalizedValue = 0;
        _numberCheckPoint.text = "0";
    }

    private void EnemyDie()
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

    private void ResetProgressSlider(CheckPoint point)
    {
        StopAllCoroutines();
        StartCoroutine(AmimationProgressSliderToZero());
        _currentCheckPoint = point;
    }

    private void PlayerMoveNextPoint(CheckPoint point)
    {
        _indexer++;
        RefreshText();
    }

    private void RefreshText() => _numberCheckPoint.text = _indexer.ToString();
}
