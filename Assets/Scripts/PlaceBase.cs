using UnityEngine;

abstract class PlaceBase : MonoBehaviour {

    [SerializeField]
    protected GameObject cardBoardBox;

    public abstract bool hasBox();
    public abstract void SetBox(GameObject cardBoardbox);
    public abstract GameObject GetBox();

    public CardBoardBoxInfo getCardBoardInfo() {
        return cardBoardBox.GetComponent<CardBoardBox>().cardBoardBoxInfo;
    }
}
