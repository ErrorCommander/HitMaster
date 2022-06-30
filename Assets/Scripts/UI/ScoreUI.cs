using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] Score _score;
    [SerializeField] RectTransform _parentUI;
    [Header("Text settings")]
    [SerializeField] private Text _textScore;
    [SerializeField] private Text _prefabTextDeltaScore;
    [SerializeField] [Range(0, 5)] private float _timeDuration = 0.5f;
    [SerializeField] private float _offsetYPosStart = 0;
    [SerializeField] private float _offsetYPosEnd = -200;
    [SerializeField] private Color _decreaseColorText = Color.red;
    [SerializeField] private Color _increaseColorText = Color.green;

    private UIPool<Text> _pool;

    public void SetScore(int deltaScore)
    {
        _textScore.text = _score.CurrentScore.ToString();

        if (deltaScore > 0)
            StartCoroutine(ChangeScoreAnimation(deltaScore, _increaseColorText, _offsetYPosStart, _offsetYPosEnd));
        else
            StartCoroutine(ChangeScoreAnimation(deltaScore, _decreaseColorText, -_offsetYPosStart, -_offsetYPosEnd));
    }

    private IEnumerator ChangeScoreAnimation(int deltaScore, Color color, float startYPosition, float endYPosition)
    {
        if (deltaScore == 0)
            yield break;

        float delta = (endYPosition - startYPosition) / _timeDuration;
        float timeAnimation = 0;

        Text textDeltaScore = _pool.TakeGameObject();

        if (deltaScore < 0)
            textDeltaScore.text = deltaScore.ToString();
        else
            textDeltaScore.text = $"+{deltaScore}";

        textDeltaScore.transform.localPosition = Vector3.up * startYPosition;
        textDeltaScore.color = color;

        while (timeAnimation < _timeDuration)
        {
            textDeltaScore.canvasRenderer.SetAlpha(1 - timeAnimation / _timeDuration);
            timeAnimation += Time.deltaTime;
            textDeltaScore.transform.localPosition = textDeltaScore.transform.localPosition + Vector3.up * Time.deltaTime * delta;
            yield return null;
        }

        textDeltaScore.gameObject.SetActive(false);
    }

    private void Awake()
    {
        _pool = new UIPool<Text>(_prefabTextDeltaScore, _parentUI, 2);
        _pool.Initialize();
    }

    private void OnDisable()
    {
        _score.ChangeValue -= SetScore;
    }

    private void OnEnable()
    {
        _score.ChangeValue += SetScore;
        _textScore.text = _score.CurrentScore.ToString();
    }
}
