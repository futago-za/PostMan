using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckController : MonoBehaviour {

    public float moveSpeed = 2;
    public int maxWeight = 20;
    public TruckInfo truckInfo;
    public bool stopping = true;    //停車しているか

    float span = 5.0f;
    float delta = 0;

    Animator animator;
    
	void Start () {
        truckInfo = new TruckInfo(Random.Range(9, maxWeight));
        animator = GetComponent<Animator>();
        animator.SetBool("isopen", true);
	}
	
	void Update () {
        if(transform.position.x >= 15) {
            delta += Time.deltaTime;
            if (delta > span) {
                GameObject.Find("GameDirector").GetComponent<GameDirector>().Save(truckInfo);
                truckInfo = new TruckInfo(Random.Range(9, maxWeight));
                BackRun();
                delta = 0f;
            }
        }
	}

    public void Run() {
        stopping = false;
        GameObject.Find("GameDirector").GetComponent<GameDirector>().TurnGateEnable(!stopping);
        IEnumerator coroutine = RunCoroutine();
        StartCoroutine(coroutine);
    }

    IEnumerator RunCoroutine() {
        animator.SetBool("isopen", false);
        yield return null;
        while (true) {
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1) {
                if (transform.position.x < 15) {
                    transform.position += transform.forward * moveSpeed * Time.deltaTime;
                    yield return null;
                } else {
                    yield break;
                }
            } else {
                yield return null;
            }
        }
    }

    public void BackRun() {
        IEnumerator coroutine = BackRunCoroutine();
        StartCoroutine(coroutine);
    }

    IEnumerator BackRunCoroutine() {
        while (true) {
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1) {
                if (transform.position.x > 5) {
                    transform.position -= transform.forward * moveSpeed * Time.deltaTime;
                    yield return null;
                } else {
                    break;
                }
            } else {
                yield return null;
            }
        }
        animator.SetBool("isopen", true);
        stopping = true;
        GameObject.Find("GameDirector").GetComponent<GameDirector>().TurnGateEnable(!stopping);
        yield break;
    }

    public void Push(CardBoardBoxInfo cardBoardBox) {
        truckInfo.Push(cardBoardBox);
    }

    public CardBoardBoxInfo Pop() {
        return truckInfo.Pop();
    }
}
