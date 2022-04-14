using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerStorageDoor : MonoBehaviour
{
    public GameObject trigger;
    public GameObject storageDoor;

    public GameObject triggerText;

    public AudioSource openDoorSource;
    public AudioClip openDoorClip;

    void OnTriggerEnter(Collider other){              
        storageDoor.SetActive(false);
        openDoorSource.PlayOneShot(openDoorClip);
        trigger.SetActive(false);
    }
}
