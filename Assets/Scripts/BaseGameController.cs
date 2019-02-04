using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGameController : MonoBehaviour {

    private bool isDisplay;

    public virtual bool GetIsDisplay() {
        return false;
    }

    public virtual void Save(TruckInfo truckInfo) {
        return;
    }
}
