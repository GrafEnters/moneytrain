using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    [SerializeField]
    private GameConfig _gameConfig;

    [SerializeField]
    private LevelsManager _levelsManager;
    
    [SerializeField]
    private SpiceManager _spiceManager;

    [SerializeField]
    private EnemiesManager _enemiesManager;

    [SerializeField]
    private WagonsManager _wagonsManager;

    [SerializeField]
    private PlayerManager _playerManager;

    private Coroutine _gameLoopCoroutine;

    private void Start() {
        UIManager.Instance.ShowStartGameDialog();
    }

    private IEnumerator GameLoop() {
        while (true) {
            _playerManager.SetControlsEnabled(true);
            _levelsManager.StartNextLevel();

            yield return StartCoroutine(_levelsManager.ProgressCoroutine());
            if (_enemiesManager.AliveEnemies > 0) {
                yield return StartCoroutine(_enemiesManager.WaitForEnemiesToDie());
            }
            _playerManager.SetControlsEnabled(false);
            yield return StartCoroutine(_wagonsManager.AddWagonCoroutine());
        }
    }

    public void StopGameLoop() {
        if (_gameLoopCoroutine == null) {
            return;
        }
        StopAllCoroutines();
        StopCoroutine(_gameLoopCoroutine);
        _gameLoopCoroutine = null;
        _spiceManager.ResetSpiceList();
        _enemiesManager.ResetEnemies();
        UIManager.Instance.ShowDiedDialog();
    }

    public void StartGameLoop() {
        if (_gameLoopCoroutine != null) {
            return;
        }

        _levelsManager.ResetLevels();
        _wagonsManager.ResetTrain();
        _playerManager.ResetPlayer();
        _gameLoopCoroutine = StartCoroutine(GameLoop());
        UIManager.Instance.ShowHud();
    }

    public void ReloadScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}