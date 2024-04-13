using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour {
    [SerializeField]
    private float _hp = 3;

    [SerializeField]
    private float _speed = 1;

    [SerializeField]
    private float _followStopDist = 0.5f;

    [SerializeField]
    private TriangleIndication _indicationPrefab;

    public TriangleIndication IndicationPrefab => _indicationPrefab;

    [SerializeField]
    private Rigidbody2D _rb;

    private Player _player;

    public Action<Enemy> OnDie;
    public Action<Enemy> OnUpdate;

    public void Init(Player player, Action<Enemy> onDie) {
        _player = player;
        OnDie += onDie;
        DoEnemyStuff();
    }

    protected virtual void DoEnemyStuff() {
        StartCoroutine(EnemyStuff());
    }

    private IEnumerator EnemyStuff() {
        while (_hp > 0) {
            Vector3 dist = _player.transform.position - transform.position;
            if (dist.magnitude > _followStopDist) {
                Vector2 shift = Time.fixedDeltaTime * _speed * dist.normalized;
                _rb.MovePosition(_rb.position + shift);
            }

            OnUpdate?.Invoke(this);
            yield return new WaitForFixedUpdate();
        }

        Die();
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.attachedRigidbody != null && col.attachedRigidbody.CompareTag("PlayerBullet")) {
            TakeDamage();
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