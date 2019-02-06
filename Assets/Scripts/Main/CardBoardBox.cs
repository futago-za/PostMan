using UnityEngine;

public class CardBoardBox : MonoBehaviour {
    
    public CardBoardBoxInfo cardBoardBoxInfo;

    int[] weights = { 2, 3, 5, 6, 9 };
    int[] prices = { 200, 400, 700, 1000, 1400 };

    public void Init(int playerNum) {
        int index = GameObject.Find("GameDirector").GetComponent<PrefabGenerator>().PopCreateBoxIndex(playerNum);
        cardBoardBoxInfo = new CardBoardBoxInfo(weights[index], prices[index]);
    }
}
