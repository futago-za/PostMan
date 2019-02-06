using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameDirector : MonoBehaviour {

    public GameObject truck;

    Text weightText;

	void Start () {
        weightText = GameObject.Find("WeightText").GetComponent<Text>();
	}
	
	void Update () {
        TruckInfo truckInfo = truck.GetComponent<TruckController>().truckInfo;
        weightText.text = truckInfo.SumWeight + "/" + truckInfo.MaxWeight;
	}

    public virtual bool CheckStartFlag() {
        return !GetComponent<FadeController>().GetIsFading();
    }
}
