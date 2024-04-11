using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemiesManager : MonoBehaviour {
    [SerializeField]
    private List<EnemyConfig> _enemies;

    private List<Enemy> _aliveEnemies = new List<Enemy>();

    [SerializeField]
    private PlayerManager _playerManager;

    [SerializeField]
    private List<Transform> _spawnPoints;

    public void SpawnEnemy(EnemyType type) {
        Enemy prefab = _enemies.First(e => e.Type == type).Prefab;
        var point = _spawnPoints[Random.Range(0, _spawnPoints.Count)];
        Enemy obj = Instantiate(prefab,point );
        obj.transform.position = point.position;
        obj.Init(_playerManager.Player, OnEnemyDie);
        _aliveEnemies.Add(obj);
    }

    public IEnumerator WaitForEnemiesToDie() {
        while (_aliveEnemies.Count > 0) {
            yield return new WaitForSeconds(1);
        }
    }

    private void OnEnemyDie(Enemy died) {
        _aliveEnemies.Remove(died);
        Destroy(died.gameObject);
    }
}