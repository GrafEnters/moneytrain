using UnityEngine;

public class WeaponViewContainer : MonoBehaviour {
    private WeaponView _curWeaponView;
    private WeaponType _curWeaponType;
    public WeaponView CurWeaponView => _curWeaponView;

    public void ChangeWeaponView(WeaponType type) {
        if (_curWeaponType == type) {
            return;
        }

        WeaponConfig cnfg = Tables.GetWeaponByType(type);
        Destroy(CurWeaponView);
        _curWeaponView = Instantiate(cnfg.WeaponViewPrefab, transform);
    }
}