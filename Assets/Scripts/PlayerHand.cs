using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour {
    [SerializeField]
    private Transform _handRotationPoint;

    [SerializeField]
    private Transform _hand, _bulletPoint;

    [SerializeField]
    private Vector3 _fromRotation;
    public Transform WeaponShootPoint => _bulletPoint;
    public Vector3 ShootDir;
    public void UpdatePos(Vector3 targetPos) {
        Vector3 lookDir = targetPos - _handRotationPoint.position;
        _handRotationPoint.rotation = Quaternion.FromToRotation(_fromRotation, lookDir);
        bool isToTheRight = _hand.position.x > _handRotationPoint.position.x;
        
        Vector3 locScale = _hand.localScale;

        locScale.x = Mathf.Abs(locScale.x) * (isToTheRight ? -1 : 1);
        _hand.localScale = locScale;
        ShootDir =  lookDir;
    }

    private void UpdateRotation() { }

    private void ChangeSide() { }
}