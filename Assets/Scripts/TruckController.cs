using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckController : MonoBehaviour {

    public float moveSpeed = 2;

    Stack<CardBoardBoxInfo> cardBoardBoxInfos;

    Animator animator;
    
	void Start () {
        cardBoardBoxInfos = new Stack<CardBoardBoxInfo>();
        animator = GetComponent<Animator>();
        animator.SetBool("isopen", true);
	}
	
	void Update () {
        if (Input.GetKey(KeyCode.Space)) {
            animator.SetBool("isopen", false);
        }

        if (!animator.GetBool("isopen") && transform.position.x <= 15) {
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }
	}

    public void Push(CardBoardBoxInfo cardBoardBox) {
        Debug.Log(cardBoardBox.Weight + "kg," + cardBoardBox.Price + "円");
        cardBoardBoxInfos.Push(cardBoardBox);
    }

    public CardBoardBoxInfo Pop() {
        return cardBoardBoxInfos.Pop();
    }
}
