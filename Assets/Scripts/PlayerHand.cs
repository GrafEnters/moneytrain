using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour {
    [SerializeField]
    private Transform _handRotationPoint;

    [SerializeField]
    private Transform _hand;

    [SerializeField]
    private List<SpriteRenderer> _renderers;

    public Transform WeaponShootPoint => _hand.transform;
    
    public void UpdatePos(Vector3 lookDir) {
        _handRotationPoint.rotation = Quaternion.FromToRotation(Vector3.right, lookDir);
        bool isToTheRight = _hand.position.x > _handRotationPoint.position.x;
        bool isUpFront = _hand.position.y < _handRotationPoint.position.y;
        Vector3 locScale = _hand.localScale;
        foreach (var VARIABLE in _renderers) {
            VARIABLE.sortingOrder = isUpFront ? 1 : 0;
        }

        locScale.y = Mathf.Abs(locScale.y) * (isToTheRight ? -1 : 1);
        _hand.localScale = locScale;
    }

    private void UpdateRotation() { }

    private void ChangeSide() { }
}