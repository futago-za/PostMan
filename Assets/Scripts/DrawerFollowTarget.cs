using UnityEngine;

public class DrawerFollowTarget : MonoBehaviour {

    RectTransform rectTransform;
    [SerializeField] Transform target = null;

	void Start () {
        rectTransform = GetComponent<RectTransform>();
	}
	
	void Update () {
        rectTransform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, target.position);
        rectTransform.position += new Vector3(0,30,0);
	}
}
