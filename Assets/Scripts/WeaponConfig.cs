using System;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/WeaponConfig", fileName = "WeaponConfig", order = 1)]
public class WeaponConfig : ScriptableObject {
    public WeaponType Type;

    public Weapon WeaponPrefab;
    
    public Bullet BulletPrefab;

    public WeaponView WeaponViewPrefab;

    public float BulletSpeed = 10;

    public float RecoilTime = 0.25f;

    public float RecoilForce = 0.05f;

    public int MaxBulletsAmount = 6;
}

[Serializable]
public enum WeaponType {
    None = 0,
    BaseWeapon,
    Shotgun
}