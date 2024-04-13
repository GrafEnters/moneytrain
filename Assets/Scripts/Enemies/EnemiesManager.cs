using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class EnemiesManager : MonoBehaviour {
    [SerializeField]
    private List<EnemyConfig> _enemies;

    private List<Enemy> _aliveEnemies = new List<Enemy>();

    [SerializeField]
    private PlayerManager _playerManager;

    [SerializeField]
    private WagonsManager _wagonsManager;

    [SerializeField]
    private Transform _indicationHolder;

    private List<Transform> _spawnPoints => _wagonsManager.SpawnPoints;

    public void SpawnEnemy(EnemyType type) {
        Enemy prefab = _enemies.First(e => e.Type == type).Prefab;
        var point = _spawnPoints[Random.Range(0, _spawnPoints.Count)];
        Enemy obj = Instantiate(prefab, point);
        obj.transform.position = point.position;
        obj.Init(_playerManager.Player, OnEnemyDie);
        SpawnIndication(obj);
        _aliveEnemies.Add(obj);
    }

    private void SpawnIndication(Enemy enemy) {
        TriangleIndication obj = Instantiate(enemy.IndicationPrefab, _indicationHolder);
        obj.Init(Camera.main.transform, enemy.transform);
        enemy.OnUpdate += delegate(Enemy enemy1) { obj.UpdatePos();};
        enemy.OnDie += delegate(Enemy enemy1) { Destroy(obj.gameObject); };
    }

    public IEnumerator WaitForEnemiesToDie() {
        while (_aliveEnemies.Count > 0) {
            yield return new WaitForSeconds(1);
        }
    }

    public void ResetEnemies() {
        foreach (var VARIABLE in _aliveEnemies) {
            Destroy(VARIABLE.GameObject());
        }

        _aliveEnemies = new List<Enemy>();
    }

    private void OnEnemyDie(Enemy died) {
        _aliveEnemies.Remove(died);
        Destroy(died.gameObject);
    }
}