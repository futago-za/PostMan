using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainGameController : MonoBehaviour {

    [SerializeField] private GameObject signal;
    [SerializeField] private Sprite readySprite;
    [SerializeField] private Sprite goSprite;
    [SerializeField] private Sprite finishSprite;

    public static List<TruckInfo> truckInfos;

    private Image signalImage;
    private bool isDisplay;

    void Start() {
        truckInfos = new List<TruckInfo>();
        signalImage = signal.GetComponent<Image>();
        isDisplay = true;
        StartSignal();
    }
    
    public void StartSignal() {
        signalImage.sprite = readySprite;
        StartCoroutine("StartSignalCoroutine");
    }

    private IEnumerator StartSignalCoroutine() {
        while(GetComponent<FadeController>().GetIsFading())
            yield return null;
        yield return new WaitForSeconds(1);

        signal.SetActive(true);
        signal.transform.localScale = Vector3.zero;
        for (float i = 20f; i >= 0; i -= 1f) {
            signal.transform.localScale += new Vector3(0.05f, 0.05f, 0.05f);
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(1);
        signalImage.sprite = goSprite;
        yield return new WaitForSeconds(1);
        signal.SetActive(false);
        isDisplay = false;
        yield break;
    }

    public void finishSignal() {
        signalImage.sprite = finishSprite;
        signal.transform.localScale = new Vector3(369.8f,91,0);
        StartCoroutine("FinishSignalCoroutine");
    }

    private IEnumerator FinishSignalCoroutine() {
        signal.SetActive(true);
        signal.transform.localScale = Vector3.zero;
        for (float i = 20f; i >= 0; i -= 1f) {
            signal.transform.localScale += new Vector3(0.05f, 0.05f, 0.05f);
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(1.5f);
        GetComponent<FadeController>().FadeOut("Result");
        yield break;
    }

    public static List<TruckInfo> getTruckInfos() {
        return truckInfos;
    }

    public void Save(TruckInfo truckInfo) {
        truckInfos.Add(truckInfo);
    }

    public bool GetIsDisplay() {
        return isDisplay;
    }
}
