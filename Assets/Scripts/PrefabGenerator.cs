using System.Collections.Generic;
using UnityEngine;

public class PrefabGenerator : MonoBehaviour{

    [SerializeField] private int truckMaxWeight = 21;

    private Queue<int> boxIndex1;
    private Queue<int> boxIndex2;
    private Queue<int> truckIndex1;
    private Queue<int> truckIndex2;

	void Awake () {
        boxIndex1 = new Queue<int>();
        boxIndex2 = new Queue<int>();

        truckIndex1 = new Queue<int>();
        truckIndex2 = new Queue<int>();
        
        for (int i = 0; i < 50; i++) {
            int addNum = Random.Range(0, 5);
            boxIndex1.Enqueue(addNum);
            boxIndex2.Enqueue(addNum);
        }

        for(int i = 0; i < 10; i++) {
            int addNum = Random.Range(9, truckMaxWeight);
            truckIndex1.Enqueue(addNum);
            truckIndex2.Enqueue(addNum);
        }
	}

    public int PopCreateBoxIndex(int playerNum) {
        if (playerNum == 1) {
            int index = boxIndex1.Dequeue();
            boxIndex1.Enqueue(index);
            return index;
        } else {
            int index = boxIndex2.Dequeue();
            boxIndex2.Enqueue(index);
            return index;
        }
    }

    public int PopCreateTruckWeight(int playerNum) {
        if(playerNum == 1) {
            int weight = truckIndex1.Dequeue();
            truckIndex1.Enqueue(weight);
            return weight;
        } else {
            int weight = truckIndex2.Dequeue();
            truckIndex2.Enqueue(weight);
            return weight;
        }
    }
}
