using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiResultController : MonoBehaviour {

    [SerializeField] private GameObject truckNum;
    [SerializeField] private Text price1Text;
    [SerializeField] private Text bonus1Text;
    [SerializeField] private Text price2Text;
    [SerializeField] private Text bonus2Text;
    [SerializeField] private Text sumPrice1Text;
    [SerializeField] private Text sumPrice2Text;
    [SerializeField] private Text result1Text;
    [SerializeField] private Text result2Text;
    [SerializeField] private GameObject selectForm;

    private List<TruckInfo> truckInfos1;
    private List<TruckInfo> truckInfos2;
    private int sumPrice1 = 0;
    private int sumPrice2 = 0;
    private int sumBonus1 = 0;
    private int sumBonus2 = 0;
    private int score1 = 0;
    private int score2 = 0;
    private bool isDrawn = false;

    void Start() {
        GetComponent<FadeController>().FadeIn();

        truckInfos1 = new List<TruckInfo>();
        truckInfos2 = new List<TruckInfo>();

        List<TruckInfo> truckInfos = MainGameController.getTruckInfos();
        foreach(TruckInfo truckInfo in truckInfos) {
            if (truckInfo.TruckName.Equals("Truck1"))
                truckInfos1.Add(truckInfo);
            else
                truckInfos2.Add(truckInfo);
        }

        int diff = Mathf.Abs(truckInfos1.Count - truckInfos2.Count);
        if(truckInfos1.Count < truckInfos2.Count) {
            for(int i = 0; i < diff; i++) {
                truckInfos1.Add(new TruckInfo(0, "Truck1"));
            }
        } else {
            for(int i = 0; i < diff; i++) {
                truckInfos2.Add(new TruckInfo(0, "Truck2"));
            }
        }

        Debug.Log("Truck1 " + truckInfos1.Count);
        Debug.Log("Truck2 " + truckInfos2.Count);
    }
    
    void Update() {
        if (GetComponent<FadeController>().GetIsFading())
            return;

        if (!isDrawn) {
            StartCoroutine("DrawResultCoroutine");
            isDrawn = true;
        }
    }

    IEnumerator DrawResultCoroutine() {
        truckNum.gameObject.SetActive(true);
        
        for(int index = 0; index < truckInfos1.Count; index++) {
            truckNum.GetComponentInChildren<Text>().text = (index + 1).ToString() + "台目";

            bool bonusFlag1 = false;
            bool bonusFlag2 = false;
            int price1 = 0;
            int price2 = 0;
            float bonus1 = 0;
            float bonus2 = 0;

            if (truckInfos1[index].SumWeight == truckInfos1[index].MaxWeight)
                bonusFlag1 = true;

            if (truckInfos2[index].SumWeight == truckInfos2[index].MaxWeight)
                bonusFlag2 = true;

            while (truckInfos1[index].CanPop()) {
                price1 += truckInfos1[index].Pop().Price;
            }
            while (truckInfos2[index].CanPop()) {
                price2 += truckInfos2[index].Pop().Price;
            }

            if (bonusFlag1)
                bonus1 = price1 * 0.5f;

            if (bonusFlag2)
                bonus2 = price2 * 0.5f;

            int max = Mathf.Max(price1, price2);

            for(int i = 0; i < max; i += 10) {
                if (i < price1) {
                    sumPrice1 += 10;
                }
                if (i < bonus1) {
                    sumBonus1 += 10;
                }

                if (i < price2) {
                    sumPrice2 += 10;
                }
                if (i < bonus2) {
                    sumBonus2 += 10;
                }

                price1Text.text = sumPrice1.ToString() + "円";
                price2Text.text = sumPrice2.ToString() + "円";
                bonus1Text.text = sumBonus1.ToString() + "円";
                bonus2Text.text = sumBonus2.ToString() + "円";


                yield return null;
            }
            yield return new WaitForSeconds(1);
        }

        truckNum.gameObject.SetActive(false);
        score1 = sumPrice1 + sumBonus1;
        score2 = sumPrice2 + sumBonus2;
        sumPrice1Text.text = score1.ToString() + "円";
        sumPrice2Text.text = score2.ToString() + "円";
        yield return new WaitForSeconds(1);

        result1Text.gameObject.SetActive(true);
        result2Text.gameObject.SetActive(true);
        if(score1 == score2) {
            result1Text.text = result2Text.text = "引き分け";
        }
        if (score1 > score2) {
            result1Text.text = "勝ち";
            result2Text.text = "負け";
        } else {
            result1Text.text = "負け";
            result2Text.text = "勝ち";
        }

        isDrawn = true;
        yield break;
    }

    public void OnClickDecideButton() {
        if (isDrawn) {
            selectForm.SetActive(true);
        }
    }

    public void OnClickStageButton() {
        GetComponent<FadeController>().FadeOut("StageSelect2");
    }

    public void OnClickTitleButton() {
        GetComponent<FadeController>().FadeOut("Title");
    }
}
