using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScream : MonoBehaviour
{
    public AudioClip screamClip = default;
    public AudioSource audioSource;
    public GameObject trigger;
    void OnTriggerEnter(Collider other){
        audioSource.PlayOneShot(screamClip);
        trigger.SetActive(false);
    }
}
