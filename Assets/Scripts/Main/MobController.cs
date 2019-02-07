using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobController : MonoBehaviour {

    [SerializeField] private float moveSpeed = 5;

    Animator animator;

    void Start() {
        animator = GetComponent<Animator>();
        animator.SetFloat("speedPercent", 100);
    }


    void Update () {
        transform.Translate(-0.0025f, 0, 0.025f);
        if(Mathf.Abs(transform.position.z) > 10) {
            Destroy(gameObject);
        }
	}
}
