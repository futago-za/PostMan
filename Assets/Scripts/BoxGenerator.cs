using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxGenerator : MonoBehaviour {

    public GameObject cardBoardBoardPrefab;
    
    public GameObject Generate() {
        GameObject boardBoard = Instantiate(cardBoardBoardPrefab) as GameObject;
        return boardBoard;
    }

    public GameObject Generate(CardBoardBoxInfo cardBoardBoxInfo) {
        GameObject boardBoard = Instantiate(cardBoardBoardPrefab) as GameObject;
        boardBoard.GetComponent<CardBoardBox>().SetValue(cardBoardBoxInfo);
        return boardBoard;
    }
}
