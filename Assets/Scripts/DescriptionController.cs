using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class DescriptionController : MonoBehaviour {

    [SerializeField] private Sprite[] descriptSprite;
    [SerializeField] private Image descriptImage;
    [SerializeField] private GameObject group;
    [SerializeField] private GameObject selectForm;
    [SerializeField] private GameObject movie;
    [SerializeField] private GameObject leftArrow;
    [SerializeField] private GameObject rightArrow;
    [SerializeField] private GameObject DecideButton;

    Color leftArrowColor, rightArrowColor;
    int nowSelectStage = 0;

	void Start () {
        leftArrowColor = leftArrow.GetComponent<Image>().color;
        rightArrowColor = rightArrow.GetComponent<Image>().color;
        descriptImage.sprite = descriptSprite[0];

        GetComponent<FadeController>().FadeIn();
    }
	
	void Update () {
        descriptImage.sprite = descriptSprite[nowSelectStage];
        movie.SetActive(false);
        DecideButton.SetActive(false);

        leftArrowColor.a = 1f;
        leftArrow.GetComponent<Image>().color = leftArrowColor;
        rightArrowColor.a = 1f;
        rightArrow.GetComponent<Image>().color = rightArrowColor;

        if (nowSelectStage == 0) {
            leftArrowColor.a = 0.5f;
            leftArrow.GetComponent<Image>().color = leftArrowColor;
            movie.SetActive(true);
        }

        if (nowSelectStage == descriptSprite.Length - 1) {
            rightArrowColor.a = 0.5f;
            rightArrow.GetComponent<Image>().color = rightArrowColor;
            DecideButton.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            selectForm.SetActive(!selectForm.activeSelf);
        }
    }

    public void OnClickLeftAllow() {
        if (nowSelectStage == 0)
            return;

        GameObject.Find("Video Player").GetComponent<VideoPlayer>().time = 0;
        nowSelectStage--;
    }

    public void OnClickRightAllow() {
        if (nowSelectStage == descriptSprite.Length - 1)
            return;

        nowSelectStage++;
    }

    public void OnDecideButton() {
        group.SetActive(false);
    }

    public void OnBackButton() {
        selectForm.SetActive(false);
    }

    public void OnTitleButton() {
        GetComponent<FadeController>().FadeOut("Title");
    }
}
