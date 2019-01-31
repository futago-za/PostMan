using UnityEngine;

class TableController : PlaceBase {

    [SerializeField] GameObject setPosition;

    public override bool hasBox() {
        if (this.cardBoardBox == null)
            return false;
        return true;
    }

    public override void SetBox(GameObject cardBoardbox) {
        this.cardBoardBox = cardBoardbox;
        this.cardBoardBox.transform.parent = null;
        this.cardBoardBox.transform.position = setPosition.transform.position;
    }

    public override GameObject GetBox() {
        GameObject rCardBoardBox = cardBoardBox;
        cardBoardBox = null;
        return rCardBoardBox;
    }
}
