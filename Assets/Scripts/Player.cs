using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField]
    private Rigidbody2D _rb;

    [SerializeField]
    private PlayerHand _hand;

    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private float _speed;

    [SerializeField]
    private int _maxHp = 3;

    [SerializeField]
    private float _dashDuration = 1;

    [SerializeField]
    private float dashSpeed = 1;

    [SerializeField]
    private float _dashCooldawntime = 3;

    private List<WeaponType> _hasWeapons = new List<WeaponType>();

    private Dictionary<WeaponType, Weapon> _weaponsD = new Dictionary<WeaponType, Weapon>();
    private WeaponType _curWeaponType;

    private Weapon CurWeapon => _weaponsD[_curWeaponType];

    private bool _isDashCooldown = false;

    private bool _isDashing;
    
    private int _curHp = 3;

    public int Hp => _curHp;

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
        _curHp--;
        UIManager.Instance.HUD.HpView.LoseHp();
    }

    public void Die() {
        gameObject.SetActive(false);
        CurWeapon.OnDie();
    }

    public void Spawn() {
        gameObject.SetActive(true);
        _curHp = _maxHp;
        UIManager.Instance.HUD.HpView.RefillHp(_maxHp);

        AddWeapon(WeaponType.BaseWeapon);
        ChangeWeapon(WeaponType.BaseWeapon);
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
            UpdateRotation();

            if (Input.GetKeyDown(KeyCode.Space) && !_isDashCooldown) {
                StartCoroutine(DashCoroutine());
            }

            if (Input.GetMouseButtonDown(0) && CurWeapon.CanShoot) {
                CurWeapon.Shoot();
            }
        }

        if (Input.GetKeyDown(KeyCode.R) && CurWeapon.CanShoot) {
            CurWeapon.StartReload();
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

    private void AddWeapon(WeaponType type) {
        _hasWeapons.Add(type);
    }

    private void ChangeWeapon(int isUp) {
        int index = _hasWeapons.IndexOf(_curWeaponType);
        index += isUp;
        index %= _hasWeapons.Count;
        ChangeWeapon(_hasWeapons[index]);
    }

    private void ChangeWeapon(WeaponType type) {
        if (!_hasWeapons.Contains(type)) {
            return;
        }

        if (_curWeaponType == type) {
            return;
        }

        if (_curWeaponType != WeaponType.None) {
            _weaponsD[_curWeaponType].gameObject.SetActive(false);
        }

        _curWeaponType = type;
        if (!_weaponsD.ContainsKey(type)) {
            WeaponConfig cnfg = Tables.GetWeaponByType(type);
            _weaponsD.Add(type, Instantiate(cnfg.WeaponPrefab, _hand.transform));
            _weaponsD[_curWeaponType].Init(cnfg);
        } else {
            _weaponsD[type].gameObject.SetActive(true);
        }
    }
}