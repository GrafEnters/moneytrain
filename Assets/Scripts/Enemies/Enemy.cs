using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour {
    [SerializeField]
    private float _hp = 3;

    [SerializeField] 
    private Bullet _bulletPrefab;

    [SerializeField]
    private float _speed = 1;

    [SerializeField]
    private float _followStopDist = 0.5f;

    [SerializeField]
    private TriangleIndication _indicationPrefab;

    private Vector3 _direction;

    [SerializeField]
    private float _miny = 0;
    
    [SerializeField]
    private float _maxy = 0;
    
    [SerializeField]
    private float _maxx = 0;
    
    [SerializeField]
    private float _minx = 0;

    public TriangleIndication IndicationPrefab => _indicationPrefab;

    [SerializeField]
    private Rigidbody2D _rb;

    private Player _player;

    private float maxtimeToChangeDir = 5;

    private float timeToChangeDir;

    private float _currentRecoil;

    [SerializeField] private float _enemyBulletSpeed = 10;

    [SerializeField] private float _maxRecoil = 5;

    public Action<Enemy> OnDie;
    public Action<Enemy> OnUpdate;

    public void Init(Player player, Action<Enemy> onDie)
    {
        _direction = (player.transform.position - transform.position).normalized;
        _player = player;
        OnDie += onDie;
        DoEnemyStuff();
        RotateToTrain();
    }

    private void RotateToTrain()
    {
        if (transform.position.y <= 0)
        {
            transform.up = transform.up * -1;
        }
    }

    protected virtual void DoEnemyStuff() {
        StartCoroutine(EnemyStuff());
    }

    private IEnumerator EnemyStuff() {
        while (_hp > 0) {
            Vector2 shift = Time.fixedDeltaTime * _speed * _direction;
            _rb.MovePosition(_rb.position + shift);

            CheckMovementBounced();
            Recoil();
            ChangeDir();
            
            

            OnUpdate?.Invoke(this);
            yield return new WaitForFixedUpdate();
        }

        Die();
    }

    private void ChangeDir()
    {
        timeToChangeDir += Time.fixedDeltaTime;
        if (timeToChangeDir >= maxtimeToChangeDir)
        {
            timeToChangeDir = 0;
            maxtimeToChangeDir = Random.Range(3, 8f);

            if (Random.Range(0,2) == 0)
            {
                _direction = (_player.transform.position - transform.position).normalized;
            }
            else
            {
                _direction = (new Vector3(Random.Range(-1, 1f), Random.Range(-1, 1f)).normalized);
            }
        }
    }

    private void Recoil()
    {
        _currentRecoil += Time.fixedDeltaTime;
        if (_currentRecoil >= _maxRecoil)
        {
            _currentRecoil = 0;
            BulletShot();
        }
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.attachedRigidbody != null && col.attachedRigidbody.CompareTag("PlayerBullet")) {
            TakeDamage();
        }
    }

    private void BulletShot()
    {
        Bullet bullet = Instantiate(_bulletPrefab, transform.position, transform.rotation);
        bullet.Init(_enemyBulletSpeed, transform.up * -1);
    }

    private void CheckMovementBounced()
    {
        if (Mathf.Abs(_rb.position.y) > _maxy)
        {
            _direction.y = Mathf.Sign(_rb.position.y) * -1 * Mathf.Abs(_direction.y);
        }
            
        if (Mathf.Abs(_rb.position.y) < _miny)
        {
            _direction.y = Mathf.Sign(_rb.position.y) * 1 * Mathf.Abs(_direction.y);
        }
            
        if (_rb.position.x > _maxx)
        {
            _direction.x = -1 * Mathf.Abs(_direction.x);
        }
            
        if (_rb.position.x < _minx)
        {
            _direction.x = 1 * Mathf.Abs(_direction.x);
        }
    }

    private void TakeDamage() {
        _hp--;
    }

    public void Die() {
        OnUpdate = null;
        OnDie?.Invoke(this);
        OnDie = null;
    }
}