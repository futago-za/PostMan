using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameDirector : MonoBehaviour {

    public GameObject truck;
    public GameObject gate;
    public List<TruckInfo> truckInfos;

    Text text;

	void Start () {
        truckInfos = new List<TruckInfo>();
        text = GameObject.Find("WeightText").GetComponent<Text>();
	}
	
	void Update () {
        TruckInfo truckInfo = truck.GetComponent<TruckController>().truckInfo;
        text.text = truckInfo.SumWeight + "/" + truckInfo.MaxWeight;
	}

    public void TurnGateEnable(bool enable) {
        gate.SetActive(enable);
    }

    public void Save(TruckInfo truckInfo) {
        truckInfos.Add(truckInfo);
    }
}
