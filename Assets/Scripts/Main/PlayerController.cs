using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField] private string horizontalString;
    [SerializeField] private string verticalString;
    [SerializeField] private string fire1String;
    [SerializeField] private GameObject truck;
    [SerializeField] private GameObject DrawerController;

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
        if (GameObject.Find("GameDirector").GetComponent<BaseGameController>().GetIsDisplay())
            return;

        DrawerController.GetComponent<DrawerFollowTarget>().Disappear();
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
        Vector3 movement = new Vector3(Input.GetAxis(horizontalString), 0, Input.GetAxis(verticalString));
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

            if (place.tag.Equals("Wall") || place.tag.Equals("Button"))
                return;

            if (!place.GetComponent<PlaceBase>().hasBox())
                return;

            DrawerController.GetComponent<DrawerFollowTarget>().Appear(place);
            if (Input.GetButtonDown(fire1String)) {
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
        if (Physics.Raycast(ray, out hit, frontRayDistance) && Input.GetButtonDown(fire1String)) {
            GameObject place = hit.collider.gameObject;

            if (place.tag.Equals("BeltConveyor") || place.tag.Equals("Wall"))
                return;

            if (!place.tag.Equals("Truck") && place.GetComponent<PlaceBase>().hasBox())
                return;

            TruckInfo truckInfo = truck.GetComponent<TruckController>().truckInfo;
            int weight = cardBoardBox.GetComponent<CardBoardBox>().cardBoardBoxInfo.Weight;
            if (place.tag.Equals("Truck") && truckInfo.SumWeight + weight > truckInfo.MaxWeight)
                return;

            place.GetComponent<PlaceBase>().SetBox(cardBoardBox);
            boxIK.leftHandTransform = null;
            boxIK.rightHandTransform = null;
            isGetted = false;
        }
    }

    void OnTriggerStay(Collider other) {
        if(other.tag.Equals("Button") &&
           Input.GetButtonDown(fire1String) &&
           truck.GetComponent<TruckController>().isStopped) {
           truck.GetComponent<TruckController>().Run();
        }
    }
}
