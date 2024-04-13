using UnityEngine;

public class DepthUpdater : MonoBehaviour {
    public int Shift = 0;

    private SpriteRenderer _renderer;
    private int _sortingOrder;
    public int SortingOrder => _sortingOrder;
    private void Awake() {
        _renderer = GetComponent<SpriteRenderer>();
    }

    void Update() {
        _sortingOrder = Shift + Mathf.RoundToInt(_renderer.transform.position.y * -100);
        _renderer.sortingOrder = _sortingOrder;
    }
}