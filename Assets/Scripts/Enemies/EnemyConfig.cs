using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/EnemyConfig", fileName = "EnemyConfig", order = 1)]
public class EnemyConfig : ScriptableObject {
    public EnemyType Type;
    public Enemy Prefab;
}