using UnityEngine;
using UnityEngine.UI;

public class DrawerFollowTarget : MonoBehaviour {

    [SerializeField] private Camera camera;
    [SerializeField] private GameObject drawer;
    [SerializeField] private Vector3 targetPos;
    [SerializeField] bool drawedLeft = true;  //吹き出しの尖っている部分を左に表示するか

    RectTransform rectTransform;
    Text infoText;

	void Start () {
        rectTransform = drawer.GetComponent<RectTransform>();
        infoText = drawer.transform.Find("InfomationText").GetComponent<Text>();
        if (!drawedLeft) {
            GameObject background = drawer.transform.Find("Background").gameObject;
            Vector3 size = background.transform.localScale;
            size.x *= -1;
            background.transform.localScale = size;
        }
	}
	
	void Update () {
        rectTransform.position = RectTransformUtility.WorldToScreenPoint(camera, targetPos);
        if (drawedLeft) {
            rectTransform.position += new Vector3(0, 45, 0);
        } else {
            rectTransform.position += new Vector3(-160, 45,0);
        }
	}

    public void Appear(GameObject place) {
        if (!place.GetComponent<PlaceBase>().hasBox())
            return;

        if (place.tag.Equals("Truck"))
            return;

        drawer.SetActive(true);

        if (place.tag.Equals("BeltConveyor")) {
            targetPos = place.transform.Find("StopPosition").position;
        } else if(place.tag.Equals("Place")){
            targetPos = place.transform.Find("SetPosition").position;
        }

        CardBoardBoxInfo cardBoardBoxInfo = place.GetComponent<PlaceBase>().getCardBoardInfo();
        infoText.text = "重さ: " + cardBoardBoxInfo.Weight + "kg\n" + "価格: " + cardBoardBoxInfo.Price + "円";
    }

    public void Disappear() {
        drawer.SetActive(false);
    }
}
