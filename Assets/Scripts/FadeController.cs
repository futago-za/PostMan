using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeController : MonoBehaviour {

    [SerializeField] private float fadeTime;
    [SerializeField] private GameObject eventSystem;
    [SerializeField] private GameObject background;
    
    private Image image;
    private bool isFading;

    void Awake() {
        image = background.GetComponent<Image>();
        fadeTime = 1f * fadeTime / 10f;
    }

    public void FadeIn() {
        background.SetActive(true);
        eventSystem.SetActive(false);
        isFading = true;
        StartCoroutine("FadeInCoroutine");
    }

    private IEnumerator FadeInCoroutine() {
        for (float i = 1f; i >= 0; i -= 0.1f) {
            image.color = new Color(0f, 0f, 0f, i);
            yield return new WaitForSeconds(fadeTime);
        }
        background.SetActive(false);
        eventSystem.SetActive(true);
        isFading = false;
        yield break;
    }

    public void FadeOut(string sceneName) {
        background.SetActive(true);
        eventSystem.SetActive(false);
        isFading = true;
        StartCoroutine("FadeOutCoroutine", sceneName);
    }

    private IEnumerator FadeOutCoroutine(string sceneName) {
        for (float i = 0f; i <= 1.1f; i += 0.1f){
            image.color = new Color(0f, 0f, 0f, i);
            yield return new WaitForSeconds(fadeTime);
        }
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene(sceneName);
        isFading = false;
        yield break;
    }

    public bool GetIsFading() {
        return isFading;
    }
}