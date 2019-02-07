using UnityEngine;

public class FirstPlayManager : MonoBehaviour {

    public static void SetTrue() {
        PlayerPrefs.SetInt("FirstPlay", 1);
    }

    public static bool GetData() {
        int value = PlayerPrefs.GetInt("FirstPlay", 0);
        return value == 1;
    }
}
