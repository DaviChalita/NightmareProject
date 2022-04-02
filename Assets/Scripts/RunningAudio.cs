using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningAudio : MonoBehaviour{

    public GameObject start;
    public GameObject end;
    public float speed;

void Update() {
         float step = speed * Time.deltaTime;
         GameObject.Find("Start").transform.position = Vector3.MoveTowards(GameObject.Find("Start").transform.position, GameObject.Find("End").transform.position, step);
         // start.transform.position = Vector3.MoveTowards(start.transform.position, end.target.position, step);
         if(GameObject.Find("Start").transform.position == GameObject.Find("End").transform.position){
             start.SetActive(false);
         }
     }

}
