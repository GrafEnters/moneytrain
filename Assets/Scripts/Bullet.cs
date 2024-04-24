using System;
using UnityEngine;

public class Bullet : MonoBehaviour {
    [SerializeField]
    private Rigidbody2D _rb;

    private Vector3 _direction;
    private float _speed;

    public void Init(float speed, Vector3 dir) {
        _speed = speed;
        _direction = dir.normalized;
        transform.up = _direction;
        Destroy(gameObject, 5);
    }

    private void FixedUpdate() {
        Vector2 shift = Time.fixedDeltaTime * _speed * _direction;
        _rb.MovePosition(_rb.position + shift);
    }

    private void OnTriggerEnter2D(Collider2D col) {
        Destroy(gameObject, 0.01f);
    }
}