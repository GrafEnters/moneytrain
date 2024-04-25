using UnityEngine;

public class BackgroundManager : MonoBehaviour {
    [SerializeField]
    private ParallaxCycledBackground[] _backgrounds;

    private int _curActiveBackgroundIndex;

    public void ChangeBackground(int newBackIndex) {
        int clampedIndex = newBackIndex % _backgrounds.Length;
        if (_curActiveBackgroundIndex == clampedIndex) {
            return;
        }

        StartCoroutine(_backgrounds[_curActiveBackgroundIndex].ChangeAlphaAnim(1, 0));

        _backgrounds[clampedIndex].gameObject.SetActive(true);
        StartCoroutine(_backgrounds[clampedIndex].ChangeAlphaAnim(0, 1));

        _curActiveBackgroundIndex = clampedIndex;
    }
}