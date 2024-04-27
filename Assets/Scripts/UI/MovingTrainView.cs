using TMPro;
using UnityEngine;

public class MovingTrainView : MonoBehaviour {
    [SerializeField]
    private TextMeshProUGUI _percentText;

    public void SetPercent(float percent) {
        _percentText.text = $"{Mathf.FloorToInt(percent * 100)}%";
        _percentText.color = Color.white;
    }

    public void ShowEnemiesAlive(int amount) {
        _percentText.text = $"{amount}:(";
        _percentText.color = Color.red;
    }
}