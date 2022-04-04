using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScream : MonoBehaviour
{
    public AudioClip screamClip = default;
    public AudioSource audioSource;
    void OnTriggerEnter(Collider other){
        audioSource.PlayOneShot(screamClip);
    }
}
