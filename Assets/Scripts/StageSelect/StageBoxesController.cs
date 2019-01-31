using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageBoxesController : MonoBehaviour {

    [SerializeField] private GameObject eventSystem;
    [SerializeField] private float moveSpeed = 2;
    private float targetPosX;

    public void Move(float moveDistance) {
        eventSystem.SetActive(false);
        targetPosX = this.transform.position.x + moveDistance;
        if (moveDistance > 0) {
            IEnumerator coroutine = RightMoveCoroutine();
            StartCoroutine(coroutine);
        } else {
            IEnumerator coroutine = LeftMoveCoroutine();
            StartCoroutine(coroutine);
        }
    }

    IEnumerator RightMoveCoroutine() {
        while (true) {
            if (targetPosX - this.transform.position.x > 0) {
                transform.position += transform.right * moveSpeed * Time.deltaTime;
                yield return null;
            } else {
                eventSystem.SetActive(true);
                yield break;
            }
        }
    }

    IEnumerator LeftMoveCoroutine() {
        while (true) {
            if (targetPosX - this.transform.position.x < 0) {
                transform.position -= transform.right * moveSpeed * Time.deltaTime;
                yield return null;
            } else {
                eventSystem.SetActive(true);
                yield break;
            }
        }
    }
}
