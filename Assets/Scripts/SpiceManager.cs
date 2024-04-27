using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class SpiceManager : MonoBehaviour
    {
        public List<BoxOfSpice> listOfSpice = new List<BoxOfSpice>();

        public void AddNewSpice(List<BoxOfSpice> spicesFromWagon)
        {
            for (int i = 0; i < spicesFromWagon.Count; i++)
            {
                listOfSpice.Add(spicesFromWagon[i]);
            }
        }

    }
}