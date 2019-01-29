using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectController : MonoBehaviour {

    [SerializeField] private int stageNum;
    [SerializeField] private float distance = 0.88f;
    [SerializeField] private GameObject leftArrow;
    [SerializeField] private GameObject rightArrow;
    [SerializeField] private GameObject stageName;
    [SerializeField] private GameObject stageBoxes;

    Color leftArrowColor, rightArrowColor;
    Text stageText;
    int nowSelectStage = 0;

    void Start() {
        leftArrowColor = leftArrow.GetComponent<Image>().color;
        rightArrowColor = rightArrow.GetComponent<Image>().color;
        stageText = stageName.GetComponent<Text>();
    }

    void Update () {
        leftArrowColor.a = 1f;
        leftArrow.GetComponent<Image>().color = leftArrowColor;
        rightArrowColor.a = 1f;
        rightArrow.GetComponent<Image>().color = rightArrowColor;

        if (nowSelectStage == 0) {
            leftArrowColor.a = 0.5f;
            leftArrow.GetComponent<Image>().color = leftArrowColor;
        }

        if (nowSelectStage == stageNum - 1) {
            rightArrowColor.a = 0.5f;
            rightArrow.GetComponent<Image>().color = rightArrowColor;
        }
	}

    public void OnClickLeftAllow() {
        if (nowSelectStage == 0)
            return;

        nowSelectStage--;
        stageText.text = "ステージ" + (nowSelectStage + 1).ToString();
        stageBoxes.GetComponent<StageBoxesController>().Move(distance);
    }

    public void OnClickRightAllow() {
        if (nowSelectStage == stageNum - 1)
            return;

        nowSelectStage++;
        stageText.text = "ステージ" + (nowSelectStage + 1).ToString();
        stageBoxes.GetComponent<StageBoxesController>().Move(-1 *  distance);
    }

    public void OnClickBackButton() {
        SceneManager.LoadScene("Title");
    }

    public void OnClickDecideButton() {
        switch (nowSelectStage) {
            case 0:
                SceneManager.LoadScene("Main");
                break;
            case 1:
                break;
            default:
                Debug.LogError("選択できません");
                break;
        }
    }
}
