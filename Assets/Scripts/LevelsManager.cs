using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelsManager : MonoBehaviour {
    [SerializeField]
    private List<LevelConfig> _levels;

    [SerializeField]
    private EnemiesManager _enemiesManager;

    [SerializeField]
    private BackgroundManager _backgroundManager;

    private int curLevel = 0;

    public void ResetLevels() {
        curLevel = 0;
    }

    public void StartNextLevel() {
        curLevel++;
        StartLevel(curLevel);
    }

    private void StartLevel(int levelIndex) {
        levelIndex--;
        LevelConfig config = levelIndex < _levels.Count ? _levels[levelIndex] : _levels.Last();
        _backgroundManager.ChangeBackground(levelIndex);
        SpawnEnemies(config);
    }

    private void SpawnEnemies(LevelConfig config) {
        for (int i = 0; i < config.EnemyAmount; i++) {
            EnemyType type = config.PossibleEnemies[Random.Range(0, config.PossibleEnemies.Count)];
            _enemiesManager.SpawnEnemy(type);
        }
    }
}