using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiGameDirector : GameDirector {

    [SerializeField] private GameObject truck1;
    [SerializeField] private GameObject truck2;
    [SerializeField] private GameObject description1;
    [SerializeField] private GameObject description2;

    Text weightText1;
    Text weightText2;

    bool readyP1 = false;
    bool readyP2 = false;

    void Start() {
        weightText1 = GameObject.Find("WeightText1").GetComponent<Text>();
        weightText2 = GameObject.Find("WeightText2").GetComponent<Text>();
    }

    void Update() {
        if (GetComponent<FadeController>().GetIsFading())
            return;
        
        TruckInfo truckInfo1 = truck1.GetComponent<TruckController>().truckInfo;
        TruckInfo truckInfo2 = truck2.GetComponent<TruckController>().truckInfo;

        if (truckInfo1 == null || truckInfo2 == null)
            return;

        weightText1.text = truckInfo1.SumWeight + "/" + truckInfo1.MaxWeight;
        weightText2.text = truckInfo2.SumWeight + "/" + truckInfo2.MaxWeight;
        
        if (!readyP1 && Input.GetKeyDown(KeyCode.E)) {
            readyP1 = true;
            description1.SetActive(false);
        }
        if(!readyP2 && Input.GetKeyDown(KeyCode.Keypad9)){
            readyP2 = true;
            description2.SetActive(false);
        }
    }

    public override bool CheckStartFlag() {
        return !(readyP1 && readyP2);
    }
}
