using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WagonsManager : MonoBehaviour {
    [SerializeField]
    private UIManager _uiManager;

    [SerializeField]
    private Wagon _wagonPrefab;

    [SerializeField]
    private Transform _wagonsHolder;

    private List<Wagon> _wagons = new List<Wagon>();

    public bool IsTrainAlive => true;

    private bool _isWagonSelected = false;

    public IEnumerator AddWagonCoroutine() {
        _isWagonSelected = false;
        _uiManager.ShowWagonSelectDialog();
        yield return new WaitWhile(() => _isWagonSelected);
    }

    public void SelectWagon() {
        _isWagonSelected = true;
        AddWagon();
    }

    public void ResetTrain() {
        foreach (Wagon wagon in _wagons) {
            Destroy(wagon.gameObject);
        }

        _wagons = new List<Wagon>();
        AddWagon();
    }

    private void AddWagon() {
        Wagon wagon = Instantiate(_wagonPrefab, _wagonsHolder);
        if (_wagons.Count == 0) {
            _wagons.Add(wagon);
        } else {
            if (Random.Range(0, 2) == 0) {
                wagon.Attach(_wagons.First().LeftHook, true);
                _wagons = _wagons.Prepend(wagon).ToList();
            } else {
                wagon.Attach(_wagons.Last().RightHook, false);
                _wagons.Add(wagon);
            }
        }
    }
}