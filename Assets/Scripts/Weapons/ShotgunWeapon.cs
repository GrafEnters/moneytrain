using UnityEngine;

public class ShotgunWeapon : Weapon {
    [SerializeField]
    private float _spreadAngle = 15;

    [SerializeField]
    private float _shellsAmount = 5;

    protected override void SpawnBullet(Vector3 direction) {
        for (int i = 0; i < _shellsAmount; i++) {
            float rndAngle = Random.Range(-_spreadAngle, _spreadAngle);
            Vector3 rndDir = Quaternion.Euler(0, 0, rndAngle) * direction;
            base.SpawnBullet(rndDir);
        }
    }
}