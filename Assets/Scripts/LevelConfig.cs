using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/LevelConfig", fileName = "LevelConfig", order = 1)]
public class LevelConfig : ScriptableObject {
    public int EnemyAmount = 1;
    public List<EnemyType> PossibleEnemies = new List<EnemyType>();
}