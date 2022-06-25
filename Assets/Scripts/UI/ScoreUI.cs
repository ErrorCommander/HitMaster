using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] Score _score;
    [Header("Text settings")]
    [SerializeField] private Text _textScore;
    [SerializeField] private Text _textDeltaScore;
    [SerializeField] [Range(0, 5)] private float _timeDuration = 0.5f;
    [SerializeField] private float _offsetYPosStart = 0;
    [SerializeField] private float _offsetYPosEnd = -200;
    [SerializeField] private Color _decreaseColorText = Color.red;
    [SerializeField] private Color _increaseColorText = Color.green;

    private Pooler _pooler;
    //private ComponentPool<Text> _pool;

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

        //I will fix this horror
        Text textDeltaScore = _pooler.Spawn(_textDeltaScore.gameObject, Vector3.zero).GetComponent<Text>();
        //Text textDeltaScore = _pool.TakeGameObject();
        if (deltaScore < 0)
            textDeltaScore.text = deltaScore.ToString();
        else
            textDeltaScore.text = $"+{deltaScore}";

        SetParametrsText(textDeltaScore);
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

    private Text SetParametrsText(Text text)
    {
        text.transform.SetParent(transform);
        text.transform.localScale = Vector3.one;
        text.rectTransform.offsetMin = _textDeltaScore.rectTransform.offsetMin;
        text.rectTransform.offsetMax = _textDeltaScore.rectTransform.offsetMax;
        text.canvasRenderer.SetAlpha(1f);

        return text;
    }

    private void Awake()
    {
        _pooler = Pooler.Instance;
        _pooler.AddPool(new Pool(_textDeltaScore.gameObject, 2));
        //_pool = new ComponentPool<Text>(_textScore, 2);
        //_pool.Initialize();
        _textDeltaScore.gameObject.SetActive(false);
        _score.ChangeValue += SetScore;
    }

    private void OnEnable()
    {
        _textDeltaScore.gameObject.SetActive(false);
        _textScore.text = _score.CurrentScore.ToString();
    }
}
