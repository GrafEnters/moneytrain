using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    [SerializeField]
    private Transform _target;

    [SerializeField]
    private Transform _stickTarget;
    
    private Transform _transform;

    private void Awake() {
        _transform = transform;
    }

    private void LateUpdate() {
        Vector3 targetPos = _target.position;
        targetPos.y = Mathf.Lerp(targetPos.y, _stickTarget.position.y, 0.5f);
        targetPos.z = _transform.position.z;
        _transform.position = targetPos;
    }
}