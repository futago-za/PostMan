using System.Collections;
using System.Collections.Generic;

public class CardBoardBoxInfo {

    private int weight;
    private int price;
    
    public CardBoardBoxInfo(int weight, int price) {
        this.weight = weight;
        this.price = price;
    }

    public int Weight {
        set { this.weight = value; }
        get { return this.weight; }
    }

    public int Price {
        set { this.price = value; }
        get { return this.price; }
    }
}
