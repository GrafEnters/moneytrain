using UnityEngine;

public class OneHeartView : MonoBehaviour {
    [SerializeField]
    private GameObject _one, _two, _empty, _outlineFull, _outlineHalf;

    [SerializeField]
    private Animation _animation;

    private int _amount;

    public void RefillHp(int amount) {
        _animation.Play("HpChange");
        _amount = amount;
    }

    public void LoseHp() {
        _amount--;
        _animation.Play("HpLose");
    }

    //triggered from animation
    public void SetHp() {
        _empty.SetActive(_amount <= 0);
        _one.SetActive(_amount > 0);
        _outlineHalf.SetActive(_amount == 1);
        _two.SetActive(_amount > 1);
        _outlineFull.SetActive(_amount > 1);
    }
}