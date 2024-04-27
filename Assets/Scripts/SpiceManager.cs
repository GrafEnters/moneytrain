using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using UnityEngine;

public class SpiceManager : MonoBehaviour {
    public List<BoxOfSpice> listOfSpice = new List<BoxOfSpice>();

    private int _countOfSpice = 0;

    public void AddNewSpice(List<BoxOfSpice> spicesFromWagon) {
        for (int i = 0; i < spicesFromWagon.Count; i++) {
            listOfSpice.Add(spicesFromWagon[i]);
        }
    }

    private void Update() {
        _countOfSpice = listOfSpice.Count;
        UIManager.Instance.HUD.SpiceCounter.text = $"ящиков: {_countOfSpice}";
    }

    public void ResetSpiceList() {
        listOfSpice = new List<BoxOfSpice>();
    }
}