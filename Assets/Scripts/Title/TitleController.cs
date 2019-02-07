using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour {

    [SerializeField] private Button OnePlayerButton;
    [SerializeField] private Button TwoPlayerButton;

    bool firstPlay;

    void Start() {
        //PlayerPrefs.DeleteAll();
        GetComponent<FadeController>().FadeIn();
        firstPlay = FirstPlayManager.GetData();
        Debug.Log(firstPlay);
        if (!firstPlay) {
            OnePlayerButton.interactable = false;
            TwoPlayerButton.interactable = false;
        }
    }

    public void OnClickOnePlayerButton() {
        if (!GetComponent<FadeController>().GetIsFading())
            GetComponent<FadeController>().FadeOut("StageSelect1");
    }

    public void OnClickTwoPlayerButton() {
        if(!GetComponent<FadeController>().GetIsFading())
            GetComponent<FadeController>().FadeOut("StageSelect2");
    }

    public void OnClickTutorialText() {
        if (!GetComponent<FadeController>().GetIsFading())
            GetComponent<FadeController>().FadeOut("Tutorial");
    }

    public void OnClickQuitText() {
        if (!GetComponent<FadeController>().GetIsFading())
            Application.Quit();
    }
}
