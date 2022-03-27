using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeRadioStat : MonoBehaviour{

    public AudioSource radioSound;
    public bool isPlaying = false;

    void Start(){
        radioSound.volume = 0f;
        radioSound.Play();
    }

    // Update is called once per frame
    void Update(){
        if(Input.GetButtonDown("Fire1")){
            PlayingAudio();
        }
    }
    
    void PlayingAudio(){
        if(isPlaying == false){   
            radioSound.volume = 1f; 
            isPlaying = true;
        }else{
            radioSound.volume = 0f;
            isPlaying = false;
        }
        
    }


}
