using UnityEngine;

public class TrainProgressView : MonoBehaviour {
    [SerializeField]
    private Transform _end, _start;

    [SerializeField]
    private MovingTrainView _movingTrainView;

    public void ChangeProgress(float percent) {
        if (percent > 1) {
            percent = 1;
        }

        _movingTrainView.transform.position = Vector3.Lerp(_start.position, _end.position, percent);
        _movingTrainView.SetPercent(percent);
    }

    public void ShowEnemiesAlive(int enemiesAmount) {
        _movingTrainView.transform.position = _end.position;
        _movingTrainView.ShowEnemiesAlive(enemiesAmount);
    }
}