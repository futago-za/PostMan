using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameDirector : MonoBehaviour {

    public GameObject truck;
    public GameObject gate;
    public List<TruckInfo> truckInfos;

    Text weightText;

	void Start () {
        truckInfos = new List<TruckInfo>();
        weightText = GameObject.Find("WeightText").GetComponent<Text>();
	}
	
	void Update () {
        TruckInfo truckInfo = truck.GetComponent<TruckController>().truckInfo;
        weightText.text = truckInfo.SumWeight + "/" + truckInfo.MaxWeight;
	}

    public void TurnGateEnable(bool enable) {
        gate.SetActive(enable);
    }

    public void Save(TruckInfo truckInfo) {
        truckInfos.Add(truckInfo);
    }
}
