using UnityEngine;

public class PlayerHand : MonoBehaviour {
    [SerializeField]
    private Transform _handRotationPoint;

    [SerializeField]
    private Transform _hand;

    [SerializeField]
    private Vector3 _fromRotation;

    public void UpdatePos(Vector3 targetPos) {
        Vector3 lookDir = targetPos - _handRotationPoint.position;
        _handRotationPoint.rotation = Quaternion.FromToRotation(_fromRotation, lookDir);
        bool isToTheRight = _hand.position.x > _handRotationPoint.position.x;

        Vector3 locScale = _hand.localScale;

        locScale.x = Mathf.Abs(locScale.x) * (isToTheRight ? -1 : 1);
        _hand.localScale = locScale;
    }
}