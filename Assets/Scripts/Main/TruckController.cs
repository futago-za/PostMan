using System.Collections;
using UnityEngine;

class TruckController : PlaceBase {

    [SerializeField] int playerNum;
    [SerializeField] int maxWeight = 20;
    [SerializeField] float moveSpeed = 2;
    [SerializeField] float span = 5.0f;
    [SerializeField] GameObject gameDirector;
    [SerializeField] GameObject gate;

    public TruckInfo truckInfo;
    public bool isStopped = true;    //停車しているか

    Animator animator;
    float delta = 0;
    float targetX;      //発進後の目標値
    float stopX;        //停車位置
    
	void Start () {
        truckInfo = new TruckInfo(gameDirector.GetComponent<PrefabGenerator>().PopCreateTruckIndex(playerNum), this.name);
        animator = GetComponent<Animator>();
        animator.SetBool("isopen", true);
        stopX = transform.position.x;
        targetX = stopX + 15;
	}
	
	void Update () {
        if(transform.position.x >= targetX) {
            delta += Time.deltaTime;
            if (delta > span) {
                if(truckInfo.SumWeight == 0) {
                    truckInfo.Push(new CardBoardBoxInfo(0, 0));
                }
                gameDirector.GetComponent<BaseGameController>().Save(truckInfo);
                truckInfo = new TruckInfo(gameDirector.GetComponent<PrefabGenerator>().PopCreateTruckIndex(playerNum), this.name);
                BackRun();
                delta = 0f;
            }
        }
	}

    public void Run() {
        isStopped = false;
        gate.SetActive(true);
        IEnumerator coroutine = RunCoroutine();
        StartCoroutine(coroutine);
    }

    IEnumerator RunCoroutine() {
        animator.SetBool("isopen", false);
        yield return null;
        while (true) {
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1) {
                if (transform.position.x < targetX) {
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
                if (transform.position.x > stopX) {
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
        gate.SetActive(false);
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

        GameObject cardBoardBox = gameDirector.GetComponent<BoxGenerator>().Generate(playerNum, Vector3.zero);
        cardBoardBox.GetComponent<CardBoardBox>().cardBoardBoxInfo = truckInfo.Pop();
        return cardBoardBox;
    }
}
