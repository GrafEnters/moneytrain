using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour {
    [SerializeField]
    private HpView _hpView;

    public HpView HpView => _hpView;

    [SerializeField]
    private TrainProgressView _trainProgressView;

    public TrainProgressView TrainProgressView => _trainProgressView;

    [SerializeField]
    private WeaponView _weaponView;

    public WeaponView WeaponView => _weaponView;

    [SerializeField]
    private TextMeshProUGUI _spiceCounter;

    public TextMeshProUGUI SpiceCounter => _spiceCounter;
}