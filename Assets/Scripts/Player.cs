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
    private PlayerHand _hand;

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
            _rb.velocity = Vector2.zero;
            _rb.angularVelocity = 0;
            return;
        }
        
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        if (horizontal != 0 || vertical != 0) {
            Vector3 movement = new Vector3(horizontal, vertical, 0);
            Vector2 shift = _speed * Time.fixedDeltaTime * movement;
            _rb.MovePosition(_rb.position + shift);
        } else {
            _rb.velocity = Vector2.zero;
        }
    }

    private void Update() {
        if (!_isControlsEnabled) {
            return;
        }

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        Vector3 direction = mousePos - transform.position;
       _hand.UpdatePos(mousePos);

        if (Input.GetMouseButtonDown(0) && !_isRecoil) {
            StartCoroutine(ShootCoroutine(direction));
        }
    }

    private bool _isRecoil = false;

    private IEnumerator ShootCoroutine(Vector3 direction) {
        _isRecoil = true;
        SpawnBullet(direction);
        yield return new WaitForSeconds(_recoilTime);
        _isRecoil = false;
    }

    private void SpawnBullet(Vector3 direction) {
        var point = _hand.WeaponShootPoint;
        Bullet bullet = Instantiate(_bulletPrefab, point.position, quaternion.identity);
        bullet.Init(_bulletSpeed, _hand.ShootDir);
    }
}