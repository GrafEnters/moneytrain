using UnityEngine;

public class Wagon : MonoBehaviour {
    [SerializeField]
    private Transform _leftHook, _rightHook;

    public Transform LeftHook => _leftHook;
    public Transform RightHook => _rightHook;

    public void Attach(Transform hook, bool isLeft) {
        Transform tr = transform;
        Vector3 hookShift = tr.position - (isLeft ? _rightHook : _leftHook).position;
        tr.position = hook.position + hookShift;
    }
}