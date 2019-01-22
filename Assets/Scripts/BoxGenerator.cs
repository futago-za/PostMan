using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxGenerator : MonoBehaviour {

    public GameObject cardBoardBoardPrefab;
    
    public GameObject Generate() {
        GameObject boardBoard = Instantiate(cardBoardBoardPrefab) as GameObject;
        return boardBoard;
    }
}
