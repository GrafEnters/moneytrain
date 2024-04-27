using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class Wagon : MonoBehaviour {
    [SerializeField]
    private Transform _leftHook, _rightHook;

    [SerializeField]
    private List<BoxOfSpice> _countOfSpice = new List<BoxOfSpice>();

    public List<BoxOfSpice> CountOfSpice => _countOfSpice;

    public Transform LeftHook => _leftHook;
    public Transform RightHook => _rightHook;
    
    [SerializeField]
    private List<Transform> _spawnPoints = new List<Transform>();

    public List<Transform> SpawnPoints => _spawnPoints;
    
    public void Attach(Transform hook, bool isLeft) {
        Transform tr = transform;
        Vector3 hookShift = tr.position - (isLeft ? _rightHook : _leftHook).position;
        tr.position = hook.position + hookShift;
    }
}