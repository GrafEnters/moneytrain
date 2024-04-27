using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class Wagon : MonoBehaviour {
    [SerializeField]
    private Transform _leftHook, _rightHook;

    [SerializeField]
    private GameObject _leftPlayerWall, _rightPlayerWall;

    [SerializeField]
    private List<BoxOfSpice> _countOfSpice = new List<BoxOfSpice>();

    public List<BoxOfSpice> CountOfSpice => _countOfSpice;

    public Transform LeftHook => _leftHook;
    public Transform RightHook => _rightHook;

    [SerializeField]
    private List<Transform> _spawnPoints = new List<Transform>();

    public List<Transform> SpawnPoints => _spawnPoints;

    public void Attach(Transform hook, bool isAttachToLeft) {
        Transform tr = transform;
        Vector3 hookShift = tr.position - (isAttachToLeft ? _rightHook : _leftHook).position;
        tr.position = hook.position + hookShift;

        DisableWall(!isAttachToLeft);
    }

    public void DisableWall(bool isLeft) {
        if (isLeft) {
            _leftPlayerWall.SetActive(false);
        } else {
            _rightPlayerWall.SetActive(false);
        }
    }
}