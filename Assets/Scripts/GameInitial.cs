using UnityEngine;

public class GameInitial{
    [RuntimeInitializeOnLoadMethod]
    static void OnRuntimeMethodLoad() {
        Screen.SetResolution(1280, 720, false, 60);
    }
}
