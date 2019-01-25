using UnityEngine;

public class CardBoardBox : MonoBehaviour {

    public CardBoardBoxInfo cardBoardBoxInfo;

    int[] weights = { 2, 3, 5, 6, 9 };
    int[] prices = { 200, 400, 700, 1000, 1400 };

	void Awake () {
        int index = Random.Range(0, 5);
        cardBoardBoxInfo = new CardBoardBoxInfo(weights[index], prices[index]);
	}

    public void SetValue(CardBoardBoxInfo cardBoardBoxInfo) {
        this.cardBoardBoxInfo = cardBoardBoxInfo;
    }
}
