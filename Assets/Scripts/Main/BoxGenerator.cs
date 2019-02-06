using UnityEngine;

public class BoxGenerator : MonoBehaviour {

    public GameObject cardBoardBoxPrefab;
    
    public GameObject Generate(int playerNum, Vector3 createPosition) {
        GameObject cardBoardBox = Instantiate(cardBoardBoxPrefab) as GameObject;
        cardBoardBox.GetComponent<CardBoardBox>().Init(playerNum);
        cardBoardBox.transform.position = createPosition;
        return cardBoardBox;
    }
}