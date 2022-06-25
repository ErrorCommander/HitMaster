using UnityEngine;
using UnityEngine.UI;

public class HealthbarHandler : MonoBehaviour
{
    [SerializeField] private HealthBar _healthBarPrefab;
    [SerializeField] private Vector3 _offseetHealthbar;
    [SerializeField] private Creature _target;
    [SerializeField] private bool _isVisibleIncompleteHealth;

    private HealthBar _healthBar;
    private Pooler _pooler;
    private delegate void EnableHealthBar();
    EnableHealthBar _enableHealthBar;

    private void Awake()
    {
        _pooler = Pooler.Instance;
    }

    private void Start()
    {
        Initialize();
        _enableHealthBar += Initialize;
    }

    private void Initialize()
    {
        if (_isVisibleIncompleteHealth && _target.PartHealth == 1)
            _target.OnTakeDamage += InitializeHB;
        else
            InitializeHB();
    }

    private void InitializeHB()
    {
        _target.OnDie += DisableHealthBar;
        _target.OnTakeDamage -= InitializeHB;
        _target.OnTakeDamage += Refresh;

        if (_healthBar == null)
            _healthBar = _pooler.Spawn(_healthBarPrefab.gameObject, transform.position + _offseetHealthbar).GetComponent<HealthBar>();
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
        _target.OnDie -= DisableHealthBar;

        if (_healthBar != null)
        {
            _healthBar.gameObject.SetActive(false);
            _healthBar = null;
        }
    }

    private void OnDisable() => DisableHealthBar();
    private void OnEnable() => _enableHealthBar?.Invoke();
}
