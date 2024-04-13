using UnityEngine;

public class DepthUpdater : MonoBehaviour {
    public int Shift = 0;

    private SpriteRenderer _renderer;

    private void Awake() {
        _renderer = GetComponent<SpriteRenderer>();
    }

    void Update() {
        _renderer.sortingOrder = Shift + Mathf.RoundToInt(_renderer.transform.position.y * -100);
    }
}