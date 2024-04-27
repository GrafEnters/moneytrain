using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    [SerializeField]
    private GameConfig _gameConfig;

    [SerializeField]
    private LevelsManager _levelsManager;

    [SerializeField]
    private EnemiesManager _enemiesManager;

    [SerializeField]
    private WagonsManager _wagonsManager;

    [SerializeField]
    private PlayerManager _playerManager;

    [SerializeField]
    private UIManager _uiManager;

    private Coroutine _gameLoopCoroutine;

    private void Start() {
        _uiManager.ShowStartGameDialog();
    }

    private IEnumerator GameLoop() {
        while (true) {
            _playerManager.SetControlsEnabled(true);
            _levelsManager.StartNextLevel();
            yield return StartCoroutine(_enemiesManager.WaitForEnemiesToDie());
            _playerManager.SetControlsEnabled(false);
            yield return StartCoroutine(_wagonsManager.AddWagonCoroutine());
        }
    }

    public void StopGameLoop() {
        if (_gameLoopCoroutine == null) {
            return;
        }

        StopCoroutine(_gameLoopCoroutine);
        _gameLoopCoroutine = null;
        _enemiesManager.ResetEnemies();
        _uiManager.ShowDiedDialog();
    }

    public void StartGameLoop() {
        if (_gameLoopCoroutine != null) {
            return;
        }

        _levelsManager.ResetLevels();
        _wagonsManager.ResetTrain();
        _playerManager.ResetPlayer();
        _gameLoopCoroutine = StartCoroutine(GameLoop());
    }

    public void ReloadScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}