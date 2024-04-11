using UnityEngine;

public class UIManager : MonoBehaviour {
    [SerializeField]
    private GameObject _startGameDialog, _diedDialog, _wagonSelectDialog;

    public void ShowStartGameDialog() {
        _startGameDialog.SetActive(true);
    }

    public void ShowDiedDialog() {
        _diedDialog.SetActive(true);
    }

    public void ShowWagonSelectDialog() {
        _wagonSelectDialog.SetActive(true);
    }
}