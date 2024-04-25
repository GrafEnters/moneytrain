using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class WeaponView : MonoBehaviour {
    [SerializeField]
    private GameObject _bulletPrefab;

    [SerializeField]
    private Transform _bulletsHolder;

    [SerializeField]
    private TextMeshProUGUI _amountText;

    private List<GameObject> _bullets = new List<GameObject>();

    private int _currentAmount;
    private int _totalAmount;

    private void Reload(int amount) {
        _currentAmount = amount;
        _totalAmount = amount;
        foreach (var VARIABLE in _bullets) {
            Destroy(VARIABLE.gameObject);
        }

        _bullets = new List<GameObject>();

        for (int index = 0; index < amount; index++) {
            GameObject b = Instantiate(_bulletPrefab, _bulletsHolder);
            b.SetActive(true);
            _bullets.Add(b);
        }

        UpdateText();
    }

    public void Shoot() {
        _currentAmount--;
        GameObject b = _bullets.Last();
        _bullets.Remove(b);
        Destroy(b.gameObject);
        UpdateText();
    }

    private void UpdateText() {
        _amountText.text = $"{_currentAmount}/ {_totalAmount}";
    }
}