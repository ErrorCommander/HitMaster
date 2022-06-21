using UnityEngine;
using UnityEngine.UI;

public class HealthbarHandler : MonoBehaviour
{
    [SerializeField] private HealthBar _healthBarPrefab;
    [SerializeField] private Vector3 _offseetHealthbar;
    [SerializeField] private Creature _target;
    [SerializeField] private bool _isVisibleAfterFirstDamage;

    private HealthBar _healthBar;

    private void Initialize()
    {
        if (_isVisibleAfterFirstDamage)
            _target.OnTakeDamage += InitializeHB;
        else
            InitializeHB();
    }

    private void InitializeHB()
    {
        _target.OnTakeDamage += Refresh;
        _target.OnDie += DestroyHealthBar;
        _target.OnTakeDamage -= InitializeHB;

        _healthBar ??= Instantiate(_healthBarPrefab, _target.transform.position + _offseetHealthbar, Quaternion.identity, _target.transform);
        _healthBar.Initialize();
        _healthBar.Activate();
        Refresh();
    }

    private void Refresh()
    {
        _healthBar.ChangeValue(_target.PartHealth);
    }

    private void DisableHealthBar()
    {
        _target.OnTakeDamage -= Refresh;

        if (_healthBar != null)
            _healthBar.Deactivate();
    }

    private void DestroyHealthBar()
    {
        DisableHealthBar();
        _target.OnDie -= DestroyHealthBar;  

        if (_healthBar != null)
            Destroy(_healthBar.gameObject);
    }

    private void OnDestroy() => DestroyHealthBar();
    private void OnDisable() => DisableHealthBar();
    private void OnEnable() => Initialize();

}
