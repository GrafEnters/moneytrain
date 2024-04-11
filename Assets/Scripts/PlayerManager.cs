using System;
using UnityEngine;

public class PlayerManager : MonoBehaviour {
    [SerializeField]
    private GameManager _gameManager;

    [SerializeField]
    private Player _player;

    public Player Player => _player;

    private bool _isPlayerAlive = false;

    public void ResetPlayer() {
        _isPlayerAlive = true;
        _player.Spawn();
    }

    public void SetControlsEnabled(bool isEnabled) {
        Player.SetControlsEnabled(isEnabled);
    }

    private void Update() {
        if (!_isPlayerAlive) {
            return;
        }

        if (_player.Hp <= 0) {
            _gameManager.StopGameLoop();
            _isPlayerAlive = false;
            _player.Die();
            SetControlsEnabled(false);
        }
    }
}