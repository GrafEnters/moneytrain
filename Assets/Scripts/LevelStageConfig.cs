using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/LevelStageConfig", fileName = "LevelStageConfig", order = 1)]
public class LevelStageConfig : ScriptableObject {
    
    //TODO better wave config
    public int EnemyAmountPerWave = 1;
    public List<float> _waves = new List<float>() { 0, 20, 40, 100000 };
    public float TimeToComplete = 60;
    public List<EnemyType> PossibleEnemies = new List<EnemyType>();
}