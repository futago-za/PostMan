using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float moveSpeed = 3;

    public float turnSmoothTime = 0.2f;
    public float speedSmoothTime = 0.1f;

    float turnSmoothVelocity;
    float speedSmoothVelocity;
    float currentSpeed;

    Rigidbody rigidbody;

	void Start () {
        rigidbody = GetComponent<Rigidbody>();
	}
	
	void FixedUpdate () {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 movementDir = movement.normalized;
        
        if(movementDir != Vector3.zero) {
            float targetRotation = Mathf.Atan2(movementDir.x, movementDir.z) * Mathf.Rad2Deg;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);
        }

        currentSpeed = Mathf.SmoothDamp(currentSpeed, moveSpeed, ref speedSmoothVelocity, speedSmoothTime);

        Vector3 velocity = transform.forward * currentSpeed;

        rigidbody.MovePosition(transform.position + velocity * Time.deltaTime);

        currentSpeed = velocity.magnitude;
	}
}
