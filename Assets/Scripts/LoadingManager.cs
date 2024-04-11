using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour {
    [SerializeField]
    private float _fakeLoadingTime = 1;

    public void Awake() {
        StartCoroutine(LoadingCoroutine());
    }

    private IEnumerator LoadingCoroutine() {
        yield return new WaitForSeconds(_fakeLoadingTime);
        SceneManager.LoadScene("GameScene");
    }
}