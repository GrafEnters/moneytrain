using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField]
    private Rigidbody2D _rb;

    [SerializeField]
    private float _speed;

    [SerializeField]
    private float _recoilTime;

    [SerializeField]
    private float _bulletSpeed = 10;

    [SerializeField]
    private int _maxHp = 3;

    [SerializeField]
    private Bullet _bulletPrefab;

    public int Hp = 3;

    private bool _isControlsEnabled = false;

    public void SetControlsEnabled(bool isEnabled) {
        _isControlsEnabled = isEnabled;
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.attachedRigidbody.CompareTag("EnemyBullet") || col.attachedRigidbody.CompareTag("EnemyTouch")) {
            TakeDamage();
        }
    }

    private void TakeDamage() {
        Hp--;
    }

    public void Die() {
        gameObject.SetActive(false);
        _isRecoil = false;
    }

    public void Spawn() {
        gameObject.SetActive(true);
        Hp = _maxHp;
    }

    private void FixedUpdate() {
        if (!_isControlsEnabled) {
            return;
        }

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        if (horizontal != 0 || vertical != 0) {
            Vector3 movement = new Vector3(horizontal, vertical, 0);
            Vector2 shift = _speed * Time.fixedDeltaTime * movement;
            _rb.MovePosition (_rb.position + shift);
        } else {
            _rb.velocity = Vector2.zero;
        }
    }

    private void Update() {
        if (!_isControlsEnabled) {
            return;
        }

        if (Input.GetMouseButtonDown(0) && !_isRecoil) {
            StartCoroutine(ShootCoroutine());
        }
    }

    private bool _isRecoil = false;

    private IEnumerator ShootCoroutine() {
        _isRecoil = true;
        SpawnBullet();
        yield return new WaitForSeconds(_recoilTime);
        _isRecoil = false;
    }

    private void SpawnBullet() {
        Bullet bullet = Instantiate(_bulletPrefab, transform.position, quaternion.identity);
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        bullet.Init(_bulletSpeed, mousePos - transform.position);
    }
}