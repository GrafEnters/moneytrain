using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

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

    [SerializeField] private float _dashDuration = 1;

    [SerializeField] private float dashSpeed = 1;

    [SerializeField] private float _dashCooldawntime = 3;
    
    private bool _isDashCooldown = false;

    private bool _isRecoil = false;

    private bool _isDashing;

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

        if (_isDashing)
        {
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
        transform.up = direction;
       _hand.UpdatePos(mousePos);

       if (Input.GetKeyDown(KeyCode.Space) && !_isDashing && !_isDashCooldown)
       {
           StartCoroutine(DashCoroutine());
       }

       if (Input.GetMouseButtonDown(0) && !_isRecoil && !_isDashing) {
            StartCoroutine(ShootCoroutine(direction));
       }
    }

    private IEnumerator ShootCoroutine(Vector3 direction) {
        _isRecoil = true;
        SpawnBullet(direction);
        yield return new WaitForSeconds(_recoilTime);
        _isRecoil = false;
    }
    
    private IEnumerator DashCoroutine() {
        _isDashing = true;
        Vector3 direction = Vector3.zero;

        if (Input.GetKey(KeyCode.A))
        {
            direction += Vector3.left;
        }
        if (Input.GetKey(KeyCode.W))
        {
            direction += Vector3.up;
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction += Vector3.right;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction += Vector3.up * -1;
        }
        
        

        float curTime = 0;

        while (curTime < _dashDuration)
        {
            Vector2 shift = direction * (Time.fixedDeltaTime * dashSpeed);
            _rb.MovePosition(shift + _rb.position);

            curTime += Time.fixedDeltaTime;
            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }

        _isDashing = false;
        _isDashCooldown = true;
        yield return new WaitForSeconds(_dashCooldawntime);
        _isDashCooldown = false;

    }

    private void SpawnBullet(Vector3 direction) {
        var point = _hand.WeaponShootPoint;
        Bullet bullet = Instantiate(_bulletPrefab, point.position, quaternion.identity);
        bullet.Init(_bulletSpeed, _hand.ShootDir);
    }
}