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
    private List<TruckInfo> truckInfos;
    private int sumPrice = 0;
    private int sumBonus = 0;
    private bool isDrawn = false;

	void Start () {
        GetComponent<FadeController>().FadeIn();
        truckInfos = MainGameController.getTruckInfos();
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
            int price = truckInfo.Pop().Price;
            plusPrice.text = "+" + price.ToString() + "円";
            for (float i = 0; i < price; i += 10) {
                sumPrice += 10;
                priceText.text = sumPrice.ToString() + "円";
                yield return null;
            }
            yield return new WaitForSeconds(1);
            count++;
        }

        truckNum.gameObject.SetActive(false);
        plusPrice.gameObject.SetActive(false);
        plusBonus.gameObject.SetActive(false);
        sumPriceText.text = (sumPrice + sumBonus).ToString() + "円";

        isDrawn = true;
        yield break;
    }

    public void OnClickDecideButton() {
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
