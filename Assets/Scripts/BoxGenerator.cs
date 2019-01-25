using UnityEngine;

public class BoxGenerator : MonoBehaviour {

    public GameObject cardBoardBoxPrefab;
    
    public GameObject Generate(Vector3 createPosition) {
        GameObject cardBoardBox = Instantiate(cardBoardBoxPrefab) as GameObject;
        cardBoardBox.transform.position = createPosition;
        return cardBoardBox;
    }
}
