using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MultiStageSelector: MonoBehaviour {
    
    [SerializeField] private GameObject stageName;

    Text stageText;
    int nowSelectStage = 0;

    void Start() {
        GetComponent<FadeController>().FadeIn();
        stageText = stageName.GetComponent<Text>();
    }

    public void OnClickStageButton(int number) {
        stageText.text = "ステージ" + (number + 1).ToString();
        nowSelectStage = number;
    }

    public void OnClickBackButton() {
        this.GetComponent<FadeController>().FadeOut("Title");
    }

    public void OnClickDecideButton() {
        switch (nowSelectStage) {
            case 0:
                this.GetComponent<FadeController>().FadeOut("MultiPlay");
                break;
            case 1:
                break;
            default:
                Debug.LogError("選択できません");
                break;
        }
    }
}
