using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiGameDirector : GameDirector {

    [SerializeField] private GameObject truck1;
    [SerializeField] private GameObject truck2;

    Text weightText1;
    Text weightText2;

    void Start() {
        weightText1 = GameObject.Find("WeightText1").GetComponent<Text>();
        weightText2 = GameObject.Find("WeightText2").GetComponent<Text>();
    }

    void Update() {
        TruckInfo truckInfo1 = truck1.GetComponent<TruckController>().truckInfo;
        TruckInfo truckInfo2 = truck2.GetComponent<TruckController>().truckInfo;
        weightText1.text = truckInfo1.SumWeight + "/" + truckInfo1.MaxWeight;
        weightText2.text = truckInfo2.SumWeight + "/" + truckInfo2.MaxWeight;
    }
}
