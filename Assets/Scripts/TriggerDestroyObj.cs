using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDestroyObj : MonoBehaviour
{
    public GameObject objToDestroy;

    void OnTriggerEnter(Collider other){
        objToDestroy.SetActive(false);
    }

}
