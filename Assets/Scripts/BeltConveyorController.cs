using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeltConveyorController : PlaceBase {
    
    public float moveSpeed= 2;
    public float createPositionX;

    float createPositionZ = 6;
	
	void Update () {
        if(cardBoardBox == null) {
            Vector3 createPosition = new Vector3(createPositionX, 0.838f, 5.5f);
            Support(createPosition);
        }

        if (cardBoardBox.transform.position.z> 3.5) {
            cardBoardBox.transform.position -= transform.forward * moveSpeed * Time.deltaTime;
        }

    }
}
