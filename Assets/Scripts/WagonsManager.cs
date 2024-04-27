using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;

public class WagonsManager : MonoBehaviour {
    [SerializeField] 
    private SpiceManager _spiceManager;

    [SerializeField]
    private List<Wagon> _wagonPrefab;

    [SerializeField]
    private Transform _wagonsHolder;

    private List<Wagon> _wagons = new List<Wagon>();

    public bool IsTrainAlive => true;

    private bool _isWagonSelected = false;

    public List<Transform> SpawnPoints => CollectSpawnPoints();
    
    public IEnumerator AddWagonCoroutine() {
        _isWagonSelected = false;
        UIManager.Instance.ShowWagonSelectDialog();
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
            
            _spiceManager.AddNewSpice(wagon.CountOfSpice);
            
            return;
        }

        if (isLeft) {
            Wagon attachTo = _wagons.First();
            attachTo.DisableWall(true);
            wagon.Attach(attachTo.LeftHook, true);
            _wagons = _wagons.Prepend(wagon).ToList();
           
        } else {
            Wagon attachTo = _wagons.Last();
            attachTo.DisableWall(false);
            wagon.Attach(attachTo.RightHook, false);
            _wagons.Add(wagon);
        }
        _spiceManager.AddNewSpice(wagon.CountOfSpice);
    }

    private List<Transform>  CollectSpawnPoints() {
        List<Transform> points = new List<Transform>();
        foreach (Wagon wagon in _wagons) {
            points.AddRange(wagon.SpawnPoints);
        }

        return points;
    }
}