using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tables : MonoBehaviour {
    public static Tables Instance;

    [SerializeField]
    private List<WeaponConfig> _weapons;

    public static WeaponConfig GetWeaponByType(WeaponType type) => Instance._weapons.FirstOrDefault(w => w.Type == type);

    [SerializeField]
    private List<LevelConfig> _levels;

    public static LevelConfig GetLevelByIndex(int index) => Instance._levels[index];

    private void Awake() {
        Instance = this;
    }
}