using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ProgressManager : MonoBehaviour {
    [SerializeField]
    private List<LevelStageConfig> _levels;

    [SerializeField]
    private EnemiesManager _enemiesManager;

    [SerializeField]
    private BackgroundManager _backgroundManager;

    private int _curStage = 0;

    private float _progressPercent = 0;

    //will be x2 when train is boosted and 0.5 or 0.1, when cabin is damaged
    private float _progressMultiplier = 1;

    private Coroutine _progressCoroutine;

    public void ResetProgress() {
        _curStage = 0;
        StopProgress();
    }

    public void StartNextLevel() {
        _curStage++;
        StartStage(_curStage);
    }

    private void StartStage(int levelIndex) {
        StopProgress();

        levelIndex--;
        LevelStageConfig stageConfig = levelIndex < _levels.Count ? _levels[levelIndex] : _levels.Last();
        _backgroundManager.ChangeBackground(levelIndex);
        SpawnEnemies(stageConfig);
    }

    private LevelStageConfig GetStage(int levelIndex) {
        return levelIndex < _levels.Count ? _levels[levelIndex] : _levels.Last();
    }

    private void SpawnEnemies(LevelStageConfig stageConfig) {
        for (int i = 0; i < stageConfig.EnemyAmountPerWave; i++) {
            EnemyType type = stageConfig.PossibleEnemies[Random.Range(0, stageConfig.PossibleEnemies.Count)];
            _enemiesManager.SpawnEnemy(type);
        }
    }

    public IEnumerator ProgressCoroutine() {
        float curTime = 0;
        int curWave = 0;
        LevelStageConfig lvlStageConfig = GetStage(_curStage);
        while (curTime < lvlStageConfig.TimeToComplete) {
            curTime += Time.deltaTime * _progressMultiplier;
            _progressPercent = curTime / lvlStageConfig.TimeToComplete;
            UIManager.Instance.HUD.TrainProgressView.ChangeProgress(_progressPercent);
            if (curWave < lvlStageConfig._waves.Count) {
                if (curTime > lvlStageConfig._waves[curWave]) {
                    SpawnEnemies(lvlStageConfig);
                    curWave++;
                }
            }

            yield return new WaitForEndOfFrame();
        }
    }

    private void StopProgress() {
        if (_progressCoroutine != null) {
            StopCoroutine(_progressCoroutine);
        }
    }
}