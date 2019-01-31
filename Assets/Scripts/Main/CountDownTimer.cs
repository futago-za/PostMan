using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CountDownTimer : MonoBehaviour {

    [SerializeField] int minute;
    [SerializeField] float seconds;

    float totalTime;
    float oldSeconds;
    Text timeText;

	void Start () {
        totalTime = minute * 60 + seconds;
        oldSeconds = 0;
        timeText = GameObject.Find("TimeText").GetComponent<Text>();
        timeText.text = minute.ToString("00") + ":" + ((int)seconds).ToString("00");

        GetComponent<FadeController>().FadeIn();
    }
	
	void Update () {
        if (GetComponent<MainGameController>().GetIsDisplay())
            return;

		if(totalTime < 0f) {
            GetComponent<MainGameController>().finishSignal();
            Destroy(this);
        }

        totalTime = minute * 60 + seconds;
        totalTime -= Time.deltaTime;

        minute = (int)totalTime / 60;
        seconds = totalTime - minute * 60;

        if((int)seconds != (int)oldSeconds) {
            timeText.text = minute.ToString("00") + ":" + ((int)seconds).ToString("00");
        }
        oldSeconds = seconds;
	}
}
