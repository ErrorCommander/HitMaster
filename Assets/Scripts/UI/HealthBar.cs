using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private Image _fill;
    [SerializeField] private Gradient _fillColor;

    private Canvas _canvas;

    public void Initialize()
    {
        _slider.normalizedValue = 1f;
        _fill.color = _fillColor.Evaluate(1f);
        _canvas = GetComponent<Canvas>();
        _canvas.enabled = false;
    }

    public void Activate()
    {
        _canvas.enabled = true;
    }

    public void Deactivate()
    {
        _canvas.enabled = false;
        gameObject.SetActive(false);
    }

    public void ChangeValue(float partHealth)
    {
        _slider.normalizedValue = partHealth;
        _fill.color = _fillColor.Evaluate(partHealth);
    }
}
