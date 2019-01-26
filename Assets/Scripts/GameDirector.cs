using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameDirector : MonoBehaviour {

    public GameObject truck;
    public GameObject gate;

    Text weightText;

	void Start () {
        weightText = GameObject.Find("WeightText").GetComponent<Text>();
	}
	
	void Update () {
        TruckInfo truckInfo = truck.GetComponent<TruckController>().truckInfo;
        weightText.text = truckInfo.SumWeight + "/" + truckInfo.MaxWeight;
	}

    public void TurnGateEnable(bool enable) {
        gate.SetActive(enable);
    }
}
