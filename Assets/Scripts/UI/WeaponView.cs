using System.Collections;
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

    [SerializeField]
    private Animation _animation;

    private List<GameObject> _bullets = new List<GameObject>();

    private int _currentAmount;
    private int _totalAmount;

    public void SetDataInstant(int amount, int maxAmount) {
        _currentAmount = amount;
        _totalAmount = maxAmount;
        UpdateText();
        RespawnBullets();
    }
    
    public void ReloadInstant(int amount) {
        _currentAmount = amount;
        _totalAmount = amount;
        UpdateText();
        RespawnBullets();
    }
    
    public IEnumerator Reload(int amount) {
        _currentAmount = amount;
        _totalAmount = amount;
        _animation.Play("Reload");
        yield return new WaitWhile(() => _animation.isPlaying);
        UpdateText();
    }

    //triggered by animation
    public void RespawnBullets() {
        foreach (var bullet in _bullets) {
            Destroy(bullet.gameObject);
        }

        _bullets = new List<GameObject>();

        for (int index = 0; index < _currentAmount; index++) {
            GameObject b = Instantiate(_bulletPrefab, _bulletsHolder);
            b.SetActive(true);
            _bullets.Add(b);
        }
    }

    public void Shoot() {
        _currentAmount--;
        GameObject b = _bullets.Last();
        _bullets.Remove(b);
        Destroy(b.gameObject);
        UpdateText();
        _animation.Play("Shoot");
    }

    private void UpdateText() {
        _amountText.text = $"{_currentAmount}/ {_totalAmount}";
    }
}