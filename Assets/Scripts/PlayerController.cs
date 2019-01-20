using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float moveSpeed = 3f;

    public float turnSmoothTime = 0.2f;
    public float speedSmoothTime = 0.1f;

    float currentSpeed;
    float turnSmoothVelocity;
    float speedSmoothVelocity;

    Rigidbody rigidbody;
    Animator animator;

    void Start() {
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate() {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 movementDir = movement.normalized;

        if (movementDir != Vector3.zero) {
            float targetRotation = Mathf.Atan2(movementDir.x, movement.z) * Mathf.Rad2Deg;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);
        }

        currentSpeed = Mathf.SmoothDamp(currentSpeed, moveSpeed * movementDir.magnitude, ref speedSmoothVelocity, speedSmoothTime);

        Vector3 velocity = transform.forward * currentSpeed;

        rigidbody.MovePosition(transform.position + velocity * Time.deltaTime);

        currentSpeed = velocity.magnitude;

        // モーション
        float animationSpeedPercent = currentSpeed / moveSpeed;
        //animator.SetFloat("speedPercent", animationSpeedPercent);
        animator.SetFloat("speedPercent", animationSpeedPercent, speedSmoothTime, Time.deltaTime);
    }

}
