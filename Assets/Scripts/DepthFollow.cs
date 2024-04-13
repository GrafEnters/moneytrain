using UnityEngine;

public class DepthFollow : MonoBehaviour {
    public int Shift = 0;

    [SerializeField]
    private DepthUpdater _parent;

    private SpriteRenderer _renderer;

    private void Awake() {
        _renderer = GetComponent<SpriteRenderer>();
    }

    void LateUpdate() {
        _renderer.sortingOrder = Shift + _parent.SortingOrder;
    }
}