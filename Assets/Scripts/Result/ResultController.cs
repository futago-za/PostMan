using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultController : MonoBehaviour {

    [SerializeField] private Text truckNum;
    [SerializeField] private Text priceText;
    [SerializeField] private Text bonusText;
    [SerializeField] private Text plusPrice;
    [SerializeField] private Text plusBonus;
    [SerializeField] private Text highPriceText;
    [SerializeField] private Text sumPriceText;
    [SerializeField] private GameObject selectForm;
    [SerializeField] private Button confirmButton;
    private List<TruckInfo> truckInfos;
    private int sumPrice = 0;
    private int sumBonus = 0;
    private int highScore = 0;
    private bool isDrawn = false;

	void Start () {
        GetComponent<FadeController>().FadeIn();
        truckInfos = MainGameController.getTruckInfos();

        int[] highScores = ScoreManager.GetData(SelectController.stageNum);
        highScore = highScores[SelectController.getSelectStageNum()];
        highPriceText.text = highScore.ToString();

        confirmButton.interactable = false;
    }
	
	void Update () {
        if (GetComponent<FadeController>().GetIsFading())
            return;

        if (!isDrawn) {
            StartCoroutine("DrawResultCoroutine");
            isDrawn = true;
        }
	}

    IEnumerator DrawResultCoroutine() {
        truckNum.gameObject.SetActive(true);
        plusPrice.gameObject.SetActive(true);
        plusBonus.gameObject.SetActive(true);

        int count = 1;
        foreach (TruckInfo truckInfo in truckInfos) {
            truckNum.text = count.ToString() + "台目";

            bool bonusFlag = false;
            int price = 0;
            float bonus = 0;

            if (truckInfo.SumWeight == truckInfo.MaxWeight)
                bonusFlag = true;

            while (truckInfo.CanPop()) {
                price += truckInfo.Pop().Price;
            }

            if (bonusFlag)
                bonus = price * 0.5f;

            plusPrice.text = "+" + price.ToString() + "円";
            plusBonus.text = "+" + bonus.ToString() + "円";
            for (int i = 0; i < price; i += 10) {
                sumPrice += 10;
                if(i < bonus) {
                    sumBonus += 10;
                }

                priceText.text = sumPrice.ToString() + "円";
                bonusText.text = sumBonus.ToString() + "円";
                yield return null;
            }
            yield return new WaitForSeconds(1);
            count++;
        }

        truckNum.gameObject.SetActive(false);
        plusPrice.gameObject.SetActive(false);
        plusBonus.gameObject.SetActive(false);
        sumPriceText.text = (sumPrice + sumBonus).ToString() + "円";

        if ((sumPrice + sumBonus) > highScore) {
            int[] highScores = ScoreManager.GetData(SelectController.stageNum);
            highScores[SelectController.getSelectStageNum()] = sumPrice + sumBonus;
            ScoreManager.SetData(highScores);
        }

        confirmButton.interactable = true;
        yield break;
    }

    public void OnClickConfirmButton() {
        if (isDrawn) {
            selectForm.SetActive(true);
        }
    }

    public void OnClickStageButton() {
        GetComponent<FadeController>().FadeOut("StageSelect1");
    }

    public void OnClickTitleButton() {
        GetComponent<FadeController>().FadeOut("Title");
    }
}
