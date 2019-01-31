using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultController : MonoBehaviour {
    
	void Start () {
        GameObject.Find("ResultController").GetComponent<FadeController>().FadeIn();
    }
	
	void Update () {
		
	}
}
