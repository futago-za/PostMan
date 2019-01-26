using System.Collections.Generic;
using UnityEngine;

public class MainGameController : MonoBehaviour {
    public static List<TruckInfo> truckInfos;

    void Start() {
        truckInfos = new List<TruckInfo>();
    }

    public static List<TruckInfo> getTruckInfos() {
        return truckInfos;
    }

    public void Save(TruckInfo truckInfo) {
        truckInfos.Add(truckInfo);
    }
}
