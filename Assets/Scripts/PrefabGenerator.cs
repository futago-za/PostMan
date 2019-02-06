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
            return boxIndex1.Dequeue();
        } else {
            return boxIndex2.Dequeue();
        }
    }

    public int PopCreateTruckIndex(int playerNum) {
        if(playerNum == 1) {
            return truckIndex1.Dequeue();
        } else {
            return truckIndex2.Dequeue();
        }
    }
}
