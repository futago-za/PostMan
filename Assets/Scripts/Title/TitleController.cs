using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour {

    void Start() {
        GetComponent<FadeController>().FadeIn();
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
        //if (!GetComponent<FadeController>().GetIsFading()
        //GetComponent<FadeController>().FadeOut("StageSelect");
    }

    public void OnClickQuitText() {
        if (!GetComponent<FadeController>().GetIsFading())
            Application.Quit();
    }
}
