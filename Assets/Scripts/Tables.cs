using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tables : MonoBehaviour {
    public static Tables Instance;

    [SerializeField]
    private List<WeaponConfig> _weapons;

    public static WeaponConfig GetWeaponByType(WeaponType type) => Instance._weapons.FirstOrDefault(w => w.Type == type);

    private void Awake() {
        Instance = this;
    }
}