using UnityEngine;

public class ScoreManager : MonoBehaviour {

    public static void SetData(int[] highScore) {
        PlayerPrefsX.SetIntArray("Scores", highScore);
    }

    public static int[] GetData(int stageNum) {
        return PlayerPrefsX.GetIntArray("Scores", 1000, stageNum);
    }
}
