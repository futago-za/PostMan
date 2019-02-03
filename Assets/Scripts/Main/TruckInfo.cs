using System.Collections.Generic;

public class TruckInfo {

    private int sumWeight;  // トラックに積まれている重さの合計
    private int maxWeight;  // トラックに積める最大の重さ
    private string truckName;
    private Stack<CardBoardBoxInfo> cardBoardBoxInfos;

    public TruckInfo(int maxWeight, string truckName) {
        this.sumWeight = 0;
        this.maxWeight = maxWeight;
        this.truckName = truckName;
        cardBoardBoxInfos = new Stack<CardBoardBoxInfo>();
    }

    public int SumWeight {
        get { return this.sumWeight; }
    }

    public int MaxWeight {
        get { return this.maxWeight; }
    }

    public string TruckName {
        get { return this.truckName; }
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
        return cardBoardBoxInfos.Count == 0 ? false : true;
    }
}
