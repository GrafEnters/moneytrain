using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/LevelConfig", fileName = "LevelConfig", order = 1)]
public class LevelConfig : ScriptableObject {
    
    //TODO better wave config
    public int EnemyAmountPerWave = 1;
    public List<float> _waves = new List<float>() { 0, 20, 40, 100000 };
    public float TimeToComplete = 60;
    public List<EnemyType> PossibleEnemies = new List<EnemyType>();
}