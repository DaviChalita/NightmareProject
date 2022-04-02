using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitRunningAudioScript : MonoBehaviour
{
    public GameObject start;

    void OnTriggerEnter(Collider other){
        // Debug.Log("tocou no trigger do running audio");
        start.SetActive(true);
    }
}
