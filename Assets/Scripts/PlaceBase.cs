using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceBase : MonoBehaviour {

    public GameObject cardBoardBox;

    public void Support(Vector3 createPosition) {
        cardBoardBox = GameObject.Find("GameDirector").GetComponent<BoxGenerator>().Generate(createPosition);
        cardBoardBox.transform.position = createPosition;
    }
}
