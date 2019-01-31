using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour {

    [SerializeField] private GameObject loadUI;
    [SerializeField] private Slider slider;
    private AsyncOperation async;
    private string loadSceneName;

    public void NextScene(string loadSceneName) {
        this.loadSceneName = loadSceneName;
        loadUI.SetActive(true);
        StartCoroutine("LoadData");
    }

    IEnumerator LoadData() {
        async = SceneManager.LoadSceneAsync(loadSceneName);

        while (!async.isDone) {
            float progressVal = Mathf.Clamp01(async.progress / 0.9f);
            slider.value = progressVal;
            yield return null;
        }
    }
}
