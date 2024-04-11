using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WagonsManager : MonoBehaviour {
    [SerializeField]
    private UIManager _uiManager;

    [SerializeField]
    private List<Wagon> _wagonPrefab;

    [SerializeField]
    private Transform _wagonsHolder;

    private List<Wagon> _wagons = new List<Wagon>();

    public bool IsTrainAlive => true;

    private bool _isWagonSelected = false;

    public IEnumerator AddWagonCoroutine() {
        _isWagonSelected = false;
        _uiManager.ShowWagonSelectDialog();
        yield return new WaitWhile(() => !_isWagonSelected);
    }

    public void SelectWagon(bool isLeft) {
        _isWagonSelected = true;
        AddWagon(isLeft);
    }

    public void ResetTrain() {
        foreach (Wagon wagon in _wagons) {
            Destroy(wagon.gameObject);
        }

        _wagons = new List<Wagon>();
        AddWagon(true);
    }

    private void AddWagon(bool isLeft) {
        Wagon wagon = Instantiate(_wagonPrefab[Random.Range(0,_wagonPrefab.Count)], _wagonsHolder);
        if (_wagons.Count == 0) {
            _wagons.Add(wagon);
            return;
        }

        if (isLeft) {
            wagon.Attach(_wagons.First().LeftHook, true);
            _wagons = _wagons.Prepend(wagon).ToList();
        } else {
            wagon.Attach(_wagons.Last().RightHook, false);
            _wagons.Add(wagon);
        }
    }
}