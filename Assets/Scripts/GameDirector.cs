using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameDirector : MonoBehaviour {

    public GameObject truck;

    Text text;

	void Start () {
        text = GameObject.Find("WeightText").GetComponent<Text>();
	}
	
	void Update () {
        TruckInfo truckInfo = truck.GetComponent<TruckController>().truckInfo;
        text.text = truckInfo.SumWeight + "/" + truckInfo.MaxWeight;
	}
}
