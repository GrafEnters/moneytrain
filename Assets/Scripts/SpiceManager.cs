using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace DefaultNamespace
{
    public class SpiceManager : MonoBehaviour
    {
        public List<BoxOfSpice> listOfSpice = new List<BoxOfSpice>();

        [SerializeField] private TextMeshProUGUI _spiceCounter;


        private int _countOfSpice = 0;

        public void AddNewSpice(List<BoxOfSpice> spicesFromWagon)
        {
            for (int i = 0; i < spicesFromWagon.Count; i++)
            {
                listOfSpice.Add(spicesFromWagon[i]);
            }
        }

        private void Update()
        {
            _countOfSpice = listOfSpice.Count;
            
                _spiceCounter.text = _countOfSpice.ToString();
        }

        public void ResetSpiceList()
        {
            listOfSpice = new List<BoxOfSpice>();
        }
    }
}