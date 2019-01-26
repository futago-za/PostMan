using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollController : MonoBehaviour {

    [SerializeField] private RectTransform nodePrefab;

    void Start() {
        List<TruckInfo> truckInfos = MainGameController.getTruckInfos();
        int sumPrice = 0;
        int index = 1;
        foreach(TruckInfo truckInfo in truckInfos) {
            RectTransform item = GameObject.Instantiate(nodePrefab) as RectTransform;
            item.SetParent(transform, false);

            int price = 0;

            while (truckInfo.CanPop()) {
                price += truckInfo.Pop().Price;
            }

            Text text = item.GetComponentInChildren<Text>();
            text.text = index.ToString() + "台目:価格      " + price.ToString() + "円";

            sumPrice += price;
            index++;
        }

        Text sumPriceText = GameObject.Find("Score").GetComponent<Text>();
        sumPriceText.text = "最終金額:  " + sumPrice.ToString() + "円";
    }
}
