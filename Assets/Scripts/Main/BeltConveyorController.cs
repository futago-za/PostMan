using UnityEngine;

class BeltConveyorController : PlaceBase {

    [SerializeField] int playerNum;
    [SerializeField] Transform startPosition;
    [SerializeField] Transform stopPosition;
    [SerializeField] float moveSpeed= 2;
    
	
	void Update () {
        if(cardBoardBox == null) {
            cardBoardBox = GameObject.Find("GameDirector").GetComponent<BoxGenerator>().Generate(playerNum, startPosition.position);
        }
        
        float distance = (cardBoardBox.transform.position - stopPosition.position).magnitude;
        if (distance > 0.05f) {
            cardBoardBox.transform.position -= transform.forward * moveSpeed * Time.deltaTime;
        }

    }

    public override bool hasBox() {
        if (this.cardBoardBox == null)
            return false;
        return true;
    }

    public override void SetBox(GameObject cardBoardBox) {
        this.cardBoardBox = cardBoardBox;
    }

    public override GameObject GetBox() {
        GameObject rCardBoardBox = cardBoardBox;
        cardBoardBox = null;
        return rCardBoardBox;
    }
}
