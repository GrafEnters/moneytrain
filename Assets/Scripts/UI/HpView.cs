using System.Collections.Generic;
using UnityEngine;

public class HpView : MonoBehaviour {
    [SerializeField]
    private OneHeartView _oneHeartViewPrefab;

    [SerializeField]
    private Transform _hearthsHolder;

    private List<OneHeartView> _heartViews = new List<OneHeartView>();

    private int _curHpAmount;

    public void RefillHp(int maxAmount) {
        if (maxAmount > _heartViews.Count * 2) {
            int toCreate = (maxAmount - _heartViews.Count * 2) / 2;
            for (int i = 0; i < toCreate; i++) {
                OneHeartView view = Instantiate(_oneHeartViewPrefab, _hearthsHolder);
                _heartViews.Add(view);
            }
        }

        for (int i = 0; i < maxAmount; i += 2) {
            OneHeartView view = _heartViews[i / 2];

            view.RefillHp(maxAmount - i > 1 ? 2 : 1);
        }

        _curHpAmount = maxAmount;
    }

    public void LoseHp() {
        if (_curHpAmount <= 0) {
            return;
        }

        OneHeartView view = GetLastViewWithHp();
        view.LoseHp();
        _curHpAmount--;
    }

    private OneHeartView GetLastViewWithHp() {
        return _heartViews[(_curHpAmount - 1) / 2];
    }
}