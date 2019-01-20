using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float moveSpeed = 5f;
    public float turnSmoothTime = 0.2f;
    public float speedSmoothTime = 0.1f;
    public float gravity = -9.8f;
    public float frontRayDistance = 1f;
    public float downRayDistance = 1f; 

    float currentSpeed;
    float turnSmoothVelocity;
    float speedSmoothVelocity;
    float gVelocity;
    bool isGround;

    GameObject rayStartPoint;
    Rigidbody rigidbody;
    Animator animator;
    BoxIK boxIK;

    void Start() {
        rayStartPoint = transform.Find("RayStartPoint").gameObject;
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        boxIK = GetComponent<BoxIK>();
    }

    void FixedUpdate() {
        CheckIsGround();
        Carry();
        Move();
    }

    void CheckIsGround() {
        Ray ray = new Ray(rayStartPoint.transform.position, transform.up * -1);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, downRayDistance)) {
                if (hit.collider.name.Equals("Floor")) {
                isGround = true;
                return;
            }
        }
        isGround = false;
    }

    void Move() {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 movementDir = movement.normalized;

        if (movementDir != Vector3.zero) {
            // y軸方向の回転
            float targetRotation = Mathf.Atan2(movementDir.x, movement.z) * Mathf.Rad2Deg;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);
        }

        currentSpeed = Mathf.SmoothDamp(currentSpeed, moveSpeed * movementDir.magnitude, ref speedSmoothVelocity, speedSmoothTime);

        if (isGround) {
            gVelocity = 0;
        } else {
            gVelocity += gravity * Time.deltaTime;
        }

        Vector3 velocity = transform.forward * currentSpeed + transform.up * gVelocity;

        rigidbody.MovePosition(transform.position + velocity * Time.deltaTime);

        currentSpeed = new Vector3(velocity.x, 0, velocity.z).magnitude;

        // モーション
        float animationSpeedPercent = currentSpeed / moveSpeed;
        //animator.SetFloat("speedPercent", animationSpeedPercent);
        animator.SetFloat("speedPercent", animationSpeedPercent, speedSmoothTime, Time.deltaTime);
    }

    void Carry() {
        Ray ray = new Ray(rayStartPoint.transform.position, transform.forward);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, frontRayDistance)) {
            Debug.Log(hit.collider.gameObject);
            if (Input.GetKeyDown(KeyCode.E) && hit.collider.tag.Equals("CardBoardBox")) {
                // TODO: 段ボールを受け取る
                GameObject cardBoardBox = hit.collider.gameObject;
                cardBoardBox.transform.parent = this.transform;
                cardBoardBox.transform.localPosition = new Vector3(0f, 1.369f, 0.599f);
                cardBoardBox.transform.localRotation = Quaternion.Euler(-90f, 180f, 90f);
                boxIK.leftHandTransform = cardBoardBox.transform.Find("LeftHand").transform;
                boxIK.rightHandTransform = cardBoardBox.transform.Find("RightHand").transform;
            }
        }
    }

}
