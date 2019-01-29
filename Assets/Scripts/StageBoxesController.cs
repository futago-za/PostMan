using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageBoxesController : MonoBehaviour {

    [SerializeField] private float moveSpeed = 2;
    private float targetPosX;

    public void Move(float moveDistance) {
        targetPosX = this.transform.position.x + moveDistance;
        Debug.Log(targetPosX);
        Debug.Log(transform.up);
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
                yield break;
            }
        }
    }
}
