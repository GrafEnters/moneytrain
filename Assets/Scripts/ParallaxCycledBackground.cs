using System.Collections;
using UnityEngine;

public class ParallaxCycledBackground : MonoBehaviour {
    [SerializeField]
    private Transform _backGround;

    [SerializeField]
    private float _speed = 1;

    [SerializeField]
    private float _teleportEdge = -20;

    [SerializeField]
    private float _backgroundSize = 100;
    
    [SerializeField]
    private float _changeAlphaTime = 1;
    private void Update() {
        _backGround.transform.position += Vector3.left * _speed * Time.deltaTime;
        TryMoveBack();
    }

    private void TryMoveBack() {
        if (_backGround.position.x < _teleportEdge) {
            _backGround.position += _backgroundSize * Vector3.right;
        }
    }

    public IEnumerator ChangeAlphaAnim(float from, float to) {
        float curTime = 0;
        var renderers = GetComponentsInChildren<SpriteRenderer>();
        
        while (curTime < _changeAlphaTime) {
            curTime += Time.deltaTime;
            foreach (SpriteRenderer spriteRenderer in renderers) {
                Color tmpColor = spriteRenderer.color;
                tmpColor.a = Mathf.Lerp(from,to,curTime / _changeAlphaTime);
                spriteRenderer.color = tmpColor;
            }
            yield return new WaitForEndOfFrame();
        }

        if (to == 0) {
            gameObject.SetActive(false);
        }
    }
}