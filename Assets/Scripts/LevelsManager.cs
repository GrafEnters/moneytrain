using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelsManager : MonoBehaviour {
    [SerializeField]
    private List<LevelConfig> _levels;

    [SerializeField]
    private EnemiesManager _enemiesManager;

    [SerializeField]
    private TrainProgressView _trainProgressView;

    [SerializeField]
    private BackgroundManager _backgroundManager;

    private int _curLevel = 0;

    private float _progressPercent = 0;

    //will be x2 when train is boosted and 0.5 or 0.1, when cabin is damaged
    private float _progressMultiplier = 1;

    private Coroutine _progressCoroutine;

    public void ResetLevels() {
        _curLevel = 0;
        StopProgress();
    }

    public void StartNextLevel() {
        _curLevel++;
        StartLevel(_curLevel);
    }

    private void StartLevel(int levelIndex) {
        StopProgress();

        levelIndex--;
        LevelConfig config = levelIndex < _levels.Count ? _levels[levelIndex] : _levels.Last();
        _backgroundManager.ChangeBackground(levelIndex);
        SpawnEnemies(config);
        _progressCoroutine = StartCoroutine(ProgressCoroutine());
    }

    private LevelConfig GetLevel(int levelIndex) {
        return levelIndex < _levels.Count ? _levels[levelIndex] : _levels.Last();
    }

    private void SpawnEnemies(LevelConfig config) {
        for (int i = 0; i < config.EnemyAmount; i++) {
            EnemyType type = config.PossibleEnemies[Random.Range(0, config.PossibleEnemies.Count)];
            _enemiesManager.SpawnEnemy(type);
        }
    }

    private IEnumerator ProgressCoroutine() {
        float curTime = 0;
        LevelConfig lvlConfig = GetLevel(_curLevel);
        while (curTime < lvlConfig.TimeToComplete) {
            curTime += Time.deltaTime * _progressMultiplier;
            _progressPercent = curTime / lvlConfig.TimeToComplete;
            _trainProgressView.ChangeProgress(_progressPercent);
            yield return new WaitForEndOfFrame();
        }
    }

    private void StopProgress() {
        if (_progressCoroutine != null) {
            StopCoroutine(_progressCoroutine);
        }
    }
}