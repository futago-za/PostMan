using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectController : MonoBehaviour {

    [SerializeField] private float distance = 0.88f;
    [SerializeField] private GameObject leftArrow;
    [SerializeField] private GameObject rightArrow;
    [SerializeField] private GameObject stageName;
    [SerializeField] private GameObject stageBoxes;
    [SerializeField] private Text highScoreText;

    //ステージ数が増えるとこの値を大きくする
    public static int stageNum = 3;

    Color leftArrowColor, rightArrowColor;
    Text stageText;
    static int nowSelectStage;
    int[] highScores;

    void Start() {
        nowSelectStage = 0;
        leftArrowColor = leftArrow.GetComponent<Image>().color;
        rightArrowColor = rightArrow.GetComponent<Image>().color;
        stageText = stageName.GetComponent<Text>();
        highScores = ScoreManager.GetData(stageNum);

        GameObject.Find("SelectController").GetComponent<FadeController>().FadeIn();
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
        
        highScoreText.text = highScores[nowSelectStage].ToString();
	}

    public static int getSelectStageNum() {
        return nowSelectStage;
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
        this.GetComponent<FadeController>().FadeOut("Title");
    }

    public void OnClickDecideButton() {
        GetComponent<FadeController>().FadeOut("SinglePlay" + (nowSelectStage+1));
    }
}
