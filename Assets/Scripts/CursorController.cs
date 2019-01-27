using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CursorController : MonoBehaviour {

    RectTransform rectTransform;

    void Start() {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update () {
        if (Input.GetKeyDown(KeyCode.W) && rectTransform.localPosition.y < 0) {
            rectTransform.localPosition += new Vector3( 0, 50, 0);
        } else if (Input.GetKeyDown(KeyCode.S) && rectTransform.localPosition.y > -150) {
            rectTransform.localPosition += new Vector3(0, -50, 0);
        }

        if (Input.GetKeyDown(KeyCode.E)){
            Select();
        }
	}

    void Select() {
        int select = (int)Mathf.Abs(rectTransform.localPosition.y / 50);
        switch (select) {
            case 0:
                SceneManager.LoadScene("Main");
                break;
            case 1:
                //SceneManager.LoadScene("Result");
                break;
            case 2:
                //SceneManager.LoadScene("Result");
                break;
            case 3:
                Application.Quit();
                break;
            default:
                Debug.LogError("選択できません");
                break;
        }
    }
}
