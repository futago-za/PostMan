using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour {

    [SerializeField] private GameObject mobPrefabs;
    [SerializeField] private Transform startPoint1;
    [SerializeField] private Transform startPoint2;

    float span = 2.0f;
    float delta = 0;
	
	void Update () {
        this.delta += Time.deltaTime;
        if(this.delta > this.span) {
            this.delta = 0;
            GameObject mob1 = Instantiate(mobPrefabs) as GameObject;
            mob1.transform.position = startPoint1.position;
            mob1.transform.parent = GameObject.Find("stage").transform;

            GameObject mob2 = Instantiate(mobPrefabs) as GameObject;
            mob2.transform.rotation = Quaternion.Euler(0,180,0);
            mob2.transform.position = startPoint2.position;
            mob2.transform.parent = GameObject.Find("stage").transform;
        }
	}
}
