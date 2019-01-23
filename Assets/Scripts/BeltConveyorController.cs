using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeltConveyorController : MonoBehaviour {

    public GameObject cardBoardBox;
    public float moveSpeed= 2;
    public float createPositionX;

    float createPositionZ = 6;

	void Start () {
		
	}
	
	void Update () {
        if (cardBoardBox.transform.position.z> 3.5) {
            cardBoardBox.transform.position -= transform.forward * moveSpeed * Time.deltaTime;
        }

    }

    public void Create() {
        cardBoardBox = GameObject.Find("GameDirector").GetComponent<BoxGenerator>().Generate();
        cardBoardBox.transform.position = new Vector3(createPositionX, cardBoardBox.transform.position.y, createPositionZ);
    }
}
