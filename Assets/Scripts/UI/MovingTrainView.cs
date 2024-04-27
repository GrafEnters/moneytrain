using TMPro;
using UnityEngine;

public class MovingTrainView : MonoBehaviour {
    [SerializeField]
    private TextMeshProUGUI _percentText;

    public void SetPercent(float percent) {
        _percentText.text = $"{Mathf.FloorToInt(percent * 100)}%";
    }
}