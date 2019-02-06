using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainGameController : BaseGameController {

    [SerializeField] private GameObject signal;
    [SerializeField] private GameObject selectForm;
    [SerializeField] private Sprite readySprite;
    [SerializeField] private Sprite goSprite;
    [SerializeField] private Sprite finishSprite;
    [SerializeField] private string nextScene;

    public static List<TruckInfo> truckInfos;

    private Image signalImage;

    void Start() {
        truckInfos = new List<TruckInfo>();
        signalImage = signal.GetComponent<Image>();
        isDisplay = true;
        StartSignal();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (selectForm.activeSelf) {
                selectForm.SetActive(false);
                GetComponent<CountDownTimer>().Restart();
            } else {
                selectForm.SetActive(true);
                GetComponent<CountDownTimer>().Stop();
            }
        }
    }

    public void StartSignal() {
        signalImage.sprite = readySprite;
        StartCoroutine("StartSignalCoroutine");
    }

    private IEnumerator StartSignalCoroutine() {
        while(GetComponent<GameDirector>().CheckStartFlag())
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
        isDisplay = true;
        foreach (GameObject truck in GameObject.FindGameObjectsWithTag("Truck")) {
            if (truck.GetComponent<TruckController>().isStopped && truck.GetComponent<TruckController>().truckInfo.SumWeight != 0) {
                truck.GetComponent<TruckController>().Run();
            }
        }
        
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
        while (true) {
            bool saveFlag = false;
            foreach (GameObject truck in GameObject.FindGameObjectsWithTag("Truck")) {
                if (truck.GetComponent<TruckController>().truckInfo.SumWeight == 0) {
                    saveFlag = true;
                }
            }

            if (saveFlag)
                break;

            yield return null;
        }
        GetComponent<FadeController>().FadeOut(nextScene);
        yield break;
    }

    public static List<TruckInfo> getTruckInfos() {
        return truckInfos;
    }

    public override void Save(TruckInfo truckInfo) {
        truckInfos.Add(truckInfo);
    }

    public override bool GetIsDisplay() {
        return isDisplay;
    }

    public void OnBackButton() {
        selectForm.SetActive(false);
    }

    public void OnClickStageButton() {
        GetComponent<FadeController>().FadeOut("StageSelect1");
    }

    public void OnTitleButton() {
        GetComponent<FadeController>().FadeOut("Title");
    }
}
