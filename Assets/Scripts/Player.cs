using System.Collections;
using System.Collections.Generic;
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

    [SerializeField]
    private float _dashDuration = 1;

    [SerializeField]
    private float dashSpeed = 1;

    [SerializeField]
    private float _dashCooldawntime = 3;

    [SerializeField]
    private CameraFollow _cameraFollow;

    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private float _recoilForce = 2;

    [SerializeField]
    private CursorController _cursorController;

    private bool _isDashCooldown = false;

    private bool _isRecoil = false;

    private bool _isReload = false;

    private int _maxBulletsAmount = 6;
    private int _currentBulletsAmount = 0;

    private bool _isDashing;

    public int Hp = 3;

    private bool _isControlsEnabled = false;
    private static readonly int Dash = Animator.StringToHash("Dash");

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
        UIManager.Instance.HUD.HpView.LoseHp();
    }

    public void Die() {
        gameObject.SetActive(false);
        _isRecoil = false;
    }

    public void Spawn() {
        gameObject.SetActive(true);
        Hp = _maxHp;
        UIManager.Instance.HUD.HpView.RefillHp(_maxHp);

        _currentBulletsAmount = _maxBulletsAmount;
        UIManager.Instance.HUD.WeaponView.ReloadInstant(_maxBulletsAmount);
    }

    private void FixedUpdate() {
        if (!_isControlsEnabled) {
            _rb.velocity = Vector2.zero;
            _rb.angularVelocity = 0;
            return;
        }

        if (_isDashing) {
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

        if (!_isDashing) {
            Vector3 dir = UpdateRotation();

            if (Input.GetKeyDown(KeyCode.Space) && !_isDashCooldown) {
                StartCoroutine(DashCoroutine());
            }

            if (Input.GetMouseButtonDown(0) && !_isRecoil && !_isReload) {
                StartCoroutine(ShootCoroutine(dir));
            }
        }

        if (Input.GetKeyDown(KeyCode.R) && !_isReload) {
            StartCoroutine(ReloadCoroutine());
        }
    }

    private Vector3 UpdateRotation() {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        Vector3 direction = mousePos - transform.position;
        transform.up = direction;
        _hand.UpdatePos(mousePos);
        return direction;
    }

    private IEnumerator ShootCoroutine(Vector3 direction) {
        _isRecoil = true;
        if (_currentBulletsAmount == 0) {
            yield return StartCoroutine(ReloadCoroutine());
        } else {
            _currentBulletsAmount--;
            UIManager.Instance.HUD.WeaponView.Shoot();
            SpawnBullet(direction);
            _cameraFollow.RecoilShake(direction, _recoilForce);
            if (_currentBulletsAmount > 0) {
                _cursorController.ChangeCursor(CursorState.Red);
            }
        }

        yield return new WaitForSeconds(_recoilTime);
        if (_currentBulletsAmount > 0) {
            _cursorController.ChangeCursor(CursorState.Normal);
        } else {
            _cursorController.ChangeCursor(CursorState.Reload);
        }

        _isRecoil = false;
    }

    private IEnumerator ReloadCoroutine() {
        _isReload = true;
        _cursorController.ChangeCursor(CursorState.Reload);
        yield return StartCoroutine(UIManager.Instance.HUD.WeaponView.Reload(_maxBulletsAmount));
        _currentBulletsAmount = _maxBulletsAmount;
        _cursorController.ChangeCursor(CursorState.Normal);
        _isReload = false;
    }

    private IEnumerator DashCoroutine() {
        _isDashing = true;
        Vector3 direction = Vector3.zero;

        if (Input.GetKey(KeyCode.A)) {
            direction += Vector3.left;
        }

        if (Input.GetKey(KeyCode.W)) {
            direction += Vector3.up;
        }

        if (Input.GetKey(KeyCode.D)) {
            direction += Vector3.right;
        }

        if (Input.GetKey(KeyCode.S)) {
            direction += Vector3.up * -1;
        }

        ChangeRbName("PlayerDash");

        float curTime = 0;
        _animator.SetTrigger(Dash);
        transform.up = direction;
        while (curTime < _dashDuration) {
            Vector2 shift = direction.normalized * (Time.fixedDeltaTime * dashSpeed);
            _rb.MovePosition(shift + _rb.position);

            curTime += Time.fixedDeltaTime;
            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }

        ChangeRbName("Player");

        _isDashing = false;
        _isDashCooldown = true;
        yield return new WaitForSeconds(_dashCooldawntime);
        _isDashCooldown = false;
    }

    private void ChangeRbName(string layerName) {
        var layer = LayerMask.NameToLayer(layerName);
        List<Collider2D> cols = new List<Collider2D>();
        _rb.GetAttachedColliders(cols);
        foreach (var col in cols) {
            col.gameObject.layer = layer;
        }
    }

    private void SpawnBullet(Vector3 direction) {
        var point = _hand.WeaponShootPoint;
        Bullet bullet = Instantiate(_bulletPrefab, point.position, quaternion.identity);
        bullet.Init(_bulletSpeed, _hand.ShootDir);
    }
}