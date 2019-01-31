using System.Collections.Generic;

public class TruckInfo {

    private int sumWeight;  // トラックに積まれている重さの合計
    private int maxWeight;  // トラックに積める最大の重さ
    private Stack<CardBoardBoxInfo> cardBoardBoxInfos;

    public TruckInfo(int maxWeight) {
        this.sumWeight = 0;
        this.maxWeight = maxWeight;
        cardBoardBoxInfos = new Stack<CardBoardBoxInfo>();
    }

    public int SumWeight {
        get { return this.sumWeight; }
    }

    public int MaxWeight {
        get { return this.maxWeight; }
    }

    public void Push(CardBoardBoxInfo cardBoardBoxInfo) {
        sumWeight += cardBoardBoxInfo.Weight;
        cardBoardBoxInfos.Push(cardBoardBoxInfo);
    }

    public CardBoardBoxInfo Pop() {
        CardBoardBoxInfo cardBoardBoxInfo = cardBoardBoxInfos.Pop();
        sumWeight -= cardBoardBoxInfo.Weight;
        return cardBoardBoxInfo;
    }

    public bool CanPop() {
        return cardBoardBoxInfos.Count <= 0 ? false : true;
    }
}
