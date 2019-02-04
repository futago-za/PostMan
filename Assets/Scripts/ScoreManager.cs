using UnityEngine;

public class ScoreManager : MonoBehaviour {

    public static void SaveData(int[] highScore) {
        PlayerPrefsX.SetIntArray("Scores", highScore);
    }

    public static int[] LoadData(int stageNum) {
        return PlayerPrefsX.GetIntArray("Scores", 1000, stageNum);
    }
}
