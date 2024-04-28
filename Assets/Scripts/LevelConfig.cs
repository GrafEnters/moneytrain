using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/LevelConfig", fileName = "LevelConfig", order = 1)]
public class LevelConfig : ScriptableObject {
    [SerializeField]
    private List<LevelStageConfig> _stages = new List<LevelStageConfig>();
    public List<LevelStageConfig> Stages => _stages;
}