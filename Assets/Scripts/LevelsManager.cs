using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelsManager : MonoBehaviour {
    [SerializeField]
    private List<LevelConfig> _levels;

    [SerializeField]
    private EnemiesManager _enemiesManager;

    [SerializeField]
    private List<GameObject> _backgrounds;

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
        ChangeBackground(levelIndex);
        SpawnEnemies(config);
    }

    private void SpawnEnemies(LevelConfig config) {
        for (int i = 0; i < config.EnemyAmount; i++) {
            EnemyType type = config.PossibleEnemies[Random.Range(0, config.PossibleEnemies.Count)]; 
            _enemiesManager.SpawnEnemy(type);
        }
    }

    private void ChangeBackground(int levelIndex) {
        int backIndex = levelIndex % _backgrounds.Count;
        for (int i = 0; i < _backgrounds.Count; i++) {
            _backgrounds[i].SetActive(i == backIndex);
        }
    }
}