using System.Collections;
using UnityEngine;

class TruckController : PlaceBase {

    public TruckInfo truckInfo;
    public bool isStopped = true;    //停車しているか

    [SerializeField] int maxWeight = 20;
    [SerializeField] float moveSpeed = 2;
    [SerializeField] float span = 5.0f;

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
                if(truckInfo.SumWeight == 0) {
                    truckInfo.Push(new CardBoardBoxInfo(0, 0));
                }
                GameObject.Find("GameDirector").GetComponent<MainGameController>().Save(truckInfo);
                truckInfo = new TruckInfo(Random.Range(9, maxWeight));
                BackRun();
                delta = 0f;
            }
        }
	}

    public void Run() {
        isStopped = false;
        GameObject.Find("GameDirector").GetComponent<GameDirector>().TurnGateEnable(!isStopped);
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
        isStopped = true;
        GameObject.Find("GameDirector").GetComponent<GameDirector>().TurnGateEnable(!isStopped);
        yield break;
    }

    public CardBoardBoxInfo Pop() {
        return truckInfo.Pop();
    }

    public override bool hasBox() {
        return truckInfo.CanPop();
    }

    public override void SetBox(GameObject cardBoardBox) {
        truckInfo.Push(cardBoardBox.GetComponent<CardBoardBox>().cardBoardBoxInfo);
        Destroy(cardBoardBox);
    }

    public override GameObject GetBox() {
        if (!truckInfo.CanPop())
            return null;

        GameObject cardBoardBox = GameObject.Find("GameDirector").GetComponent<BoxGenerator>().Generate(Vector3.zero);
        cardBoardBox.GetComponent<CardBoardBox>().cardBoardBoxInfo = truckInfo.Pop();
        return cardBoardBox;
    }
}
