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
    public bool isGetted = false;    //段ボール箱を持っているか

    Rigidbody rigidbody;
    Animator animator;
    BoxIK boxIK;
    GameObject boxCheckPoint;
    GameObject placeCheckPoint;
    GameObject cardBoardBox;

    void Start() {
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        boxIK = GetComponent<BoxIK>();
        boxCheckPoint = transform.Find("BoxCheckPoint").gameObject;
        placeCheckPoint = transform.Find("PlaceCheckPoint").gameObject;
    }

    void FixedUpdate() {
        if (GameObject.Find("GameDirector").GetComponent<MainGameController>().GetIsDisplay())
            return;

        GameObject.Find("GameDirector").GetComponent<DrawerFollowTarget>().Disappear();
        CheckIsGround();
        if (!isGetted) {
            Lift();
        } else {
            Put();
        }
        Move();
    }

    void CheckIsGround() {
        Ray ray = new Ray(boxCheckPoint.transform.position, transform.up * -1);
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

    void Lift() {
        Ray ray = new Ray(boxCheckPoint.transform.position, transform.forward);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, frontRayDistance)) {
            GameObject place = hit.collider.gameObject;

            if (place.tag.Equals("Wall"))
                return;

            if (!place.GetComponent<PlaceBase>().hasBox())
                return;

            GameObject.Find("GameDirector").GetComponent<DrawerFollowTarget>().Appear(place);
            if (GetActionButton()) {
                cardBoardBox = place.GetComponent<PlaceBase>().GetBox();
                if (cardBoardBox == null)
                    return;

                cardBoardBox.transform.parent = this.transform;
                cardBoardBox.transform.localPosition = new Vector3(0f, 1.369f, 0.599f);
                cardBoardBox.transform.localRotation = Quaternion.Euler(-90f, 180f, 90f);
                boxIK.leftHandTransform = cardBoardBox.transform.Find("LeftHand").transform;
                boxIK.rightHandTransform = cardBoardBox.transform.Find("RightHand").transform;
                isGetted = true;
            }
        }
    }

    void Put() {
        Ray ray = new Ray(placeCheckPoint.transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, frontRayDistance) && GetActionButton()) {
            GameObject place = hit.collider.gameObject;

            if (place.tag.Equals("BeltConveyor") || place.tag.Equals("Wall"))
                return;

            if (!place.name.Equals("Truck") && place.GetComponent<PlaceBase>().hasBox())
                return;

            TruckInfo truckInfo = GameObject.Find("Truck").GetComponent<TruckController>().truckInfo;
            int weight = cardBoardBox.GetComponent<CardBoardBox>().cardBoardBoxInfo.Weight;
            if (place.name.Equals("Truck") && truckInfo.SumWeight + weight > truckInfo.MaxWeight)
                return;

            place.GetComponent<PlaceBase>().SetBox(cardBoardBox);
            boxIK.leftHandTransform = null;
            boxIK.rightHandTransform = null;
            isGetted = false;
        }
    }

    void OnTriggerStay(Collider other) {
        if(other.name.Equals("Button") &&
           GetActionButton() &&
           GameObject.Find("Truck").GetComponent<TruckController>().isStopped) {
            GameObject.Find("Truck").GetComponent<TruckController>().Run();
        }
    }

    bool GetActionButton() {
        return Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("Fire1");
    }
}
