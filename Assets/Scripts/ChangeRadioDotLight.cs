using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeRadioDotLight : MonoBehaviour{

    public Material[] mat;
    Renderer rend;
    public bool isLit;

    void Start(){
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = mat[0];
        isLit = false;
    }

    // Update is called once per frame
    void Update(){
        if(Input.GetButtonDown("Fire1")){
            LittingButton();
        }
    }
    
    void LittingButton(){
        if(isLit == false){
            rend.sharedMaterial = mat[1]; 
            isLit = true; 
        }else{
            rend.sharedMaterial = mat[0];
            isLit = false;
        }
        
    }


}
