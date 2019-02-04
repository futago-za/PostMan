using UnityEngine;
using UnityEngine.UI;

public class DrawerFollowTarget : MonoBehaviour {

    [SerializeField] private Camera camera;
    [SerializeField] private GameObject drawer;
    [SerializeField] private Vector3 targetPos;

    RectTransform rectTransform;
    Text infoText;

	void Start () {
        rectTransform = drawer.GetComponent<RectTransform>();
        infoText = drawer.transform.Find("InfomationText").GetComponent<Text>();
	}
	
	void Update () {
        rectTransform.position = RectTransformUtility.WorldToScreenPoint(camera, targetPos);
        rectTransform.position += new Vector3(0, 45, 0);
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
