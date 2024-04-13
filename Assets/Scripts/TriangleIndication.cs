using System;
using UnityEngine;

public class TriangleIndication : MonoBehaviour {


    [SerializeField]
    private float multiplier = 0.75f;

    private Transform _view, _enemy;
    
    public void Init(Transform cameraView, Transform enemy) {
        _view = cameraView;
        _enemy = enemy;
    }
    
    public void UpdatePos() {
        //Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        Vector3 screenEdge = Camera.main.ScreenPointToRay(Vector3.zero).origin - _view.transform.position  ;
        screenEdge *= multiplier;
        Vector3 finPosDelta = _enemy.position - _view.position;
        bool isOnScreen = (finPosDelta.x > screenEdge.x && finPosDelta.x < screenEdge.x * -1) &&
                          (finPosDelta.y > screenEdge.y && finPosDelta.y < screenEdge.y * -1);
        gameObject.SetActive(!isOnScreen);

        finPosDelta.x = Mathf.Clamp(finPosDelta.x, screenEdge.x, screenEdge.x * -1);
        finPosDelta.y = Mathf.Clamp(finPosDelta.y, screenEdge.y, screenEdge.y * -1);
        transform.position = _view.position + finPosDelta;
    }
}