using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxGenerator : MonoBehaviour {

    public GameObject cardBoardBoardPrefab;
    
    public GameObject Generate(Vector3 createPosition) {
        GameObject boardBoard = Instantiate(cardBoardBoardPrefab) as GameObject;
        boardBoard.transform.position = createPosition;
        return boardBoard;
    }
}
