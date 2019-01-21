using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckController : MonoBehaviour {

    public float moveSpeed = 2;

    Animator animator;
    
	void Start () {
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
}
