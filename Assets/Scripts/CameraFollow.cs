using UnityEngine;

public class CameraFollow : MonoBehaviour {
    [SerializeField]
    private Transform _target;

    private Transform _transform;

    private void Awake() {
        _transform = transform;
    }

    private void LateUpdate() {
        Vector3 targetPos = _target.position;
        targetPos.z = _transform.position.z;
        _transform.position = targetPos;
    }
}