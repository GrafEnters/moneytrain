using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class Weapon : MonoBehaviour {
    [SerializeField]
    private Transform _shootPoint;
    
    private WeaponConfig _config;
    private bool _isRecoil = false;
    private bool _isReload = false;
    private int _currentBulletsAmount = 0;

    public Transform ShootPoint => _shootPoint;
    public bool CanShoot => !_isRecoil && !_isReload;
    public bool CanReload => !_isReload && _currentBulletsAmount < _config.MaxBulletsAmount;

    private WeaponViewContainer _viewContainer => UIManager.Instance.HUD.WeaponViewContainer;
    public void FirstTimeInit(WeaponConfig config) {
        _config = config;

        _currentBulletsAmount =_config. MaxBulletsAmount;
        InitView();
        _viewContainer.CurWeaponView.ReloadInstant(_config.MaxBulletsAmount);
    }

    public void InitView() {
        _viewContainer.ChangeWeaponView(_config.Type);
        _viewContainer.CurWeaponView.SetDataInstant(_currentBulletsAmount, _config.MaxBulletsAmount);
    }

    public void StartReload() {
        StartCoroutine(ReloadCoroutine());
    }

    private IEnumerator ReloadCoroutine() {
        _isReload = true;
        CursorController.ChangeCursor(CursorState.Reload);
        yield return StartCoroutine(_viewContainer.CurWeaponView.Reload(_config.MaxBulletsAmount));
        _currentBulletsAmount =_config. MaxBulletsAmount;
        CursorController.ChangeCursor(CursorState.Normal);
        _isReload = false;
    }

    protected virtual void SpawnBullet(Vector3 direction) {
        var point = _shootPoint;
        Bullet bullet = Instantiate(_config.BulletPrefab, point.position, quaternion.identity);
        bullet.Init(_config, direction);
    }

    public void Shoot() {
        StartCoroutine(ShootCoroutine(GetShootDirection()));
    }
    
    private Vector3 GetShootDirection() {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        return mousePos - _shootPoint.position;
    }

    private IEnumerator ShootCoroutine(Vector3 direction) {
        _isRecoil = true;
        if (_currentBulletsAmount == 0) {
            yield return StartCoroutine(ReloadCoroutine());
        } else {
            _currentBulletsAmount--;
            _viewContainer.CurWeaponView.Shoot();
            SpawnBullet(direction);
            CameraFollow.Instance.RecoilShake(direction,_config. RecoilForce);
            if (_currentBulletsAmount > 0) {
                CursorController.ChangeCursor(CursorState.Red);
            }
        }

        yield return new WaitForSeconds(_config.RecoilTime);
        if (_currentBulletsAmount > 0) {
            CursorController.ChangeCursor(CursorState.Normal);
        } else {
            CursorController.ChangeCursor(CursorState.Reload);
        }

        _isRecoil = false;
    }

    public void OnDie() {
        _isReload = false;
    }
}