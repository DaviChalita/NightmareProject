using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeRadioStat : MonoBehaviour{

    public AudioSource[] radioSoundArray;
    private AudioSource radioSound;
    public bool isPlaying = false;

    void Start(){
        radioSound = gameObject.GetComponent<AudioSource> ();
        
        // radioSound.volume = 0f;
        // radioSound.Play();

    }

    // Update is called once per frame
    void Update(){
        if(Input.GetButtonDown("Fire1")){
            PlayingAudio();
        }
    }
    
    void PlayingAudio(){
        if(isPlaying == false){   
            radioSound.volume = 0.04f; 
            isPlaying = true;
        }else{
            radioSound.volume = 0f;
            isPlaying = false;
        }
        
    }


}
