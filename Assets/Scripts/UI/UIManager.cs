using System;
using UnityEngine;

public class UIManager : MonoBehaviour {
    public static UIManager Instance;

    [SerializeField]
    private GameObject _startGameDialog, _diedDialog, _wagonSelectDialog;

    [SerializeField]
    private HUD _hud;

    public HUD HUD => _hud;

    private void Awake() {
        Instance = this;
    }

    public void ShowStartGameDialog() {
        HideHud();
        _startGameDialog.SetActive(true);
    }

    public void ShowDiedDialog() {
        HideHud();
        _diedDialog.SetActive(true);
    }

    public void ShowWagonSelectDialog() {
        _wagonSelectDialog.SetActive(true);
    }

    public void HideHud() {
        _hud.gameObject.SetActive(false);
    }

    public void ShowHud() {
        _hud.gameObject.SetActive(true);
    }
}