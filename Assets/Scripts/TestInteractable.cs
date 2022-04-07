using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInteractable : Interactable
{
    public Transform tpTarget;
    public GameObject player;
    public Animator animator;

    public override void OnFocus(){
        // print("Looking at "+ gameObject.name);
        
    }

    public override void OnInteract(){
       animator.SetTrigger("FadeOut"); 
       player.transform.position = tpTarget.transform.position;
       animator.SetTrigger("FadeIn");
    }

    public override void OnLoseFocus(){
       // print("Stopped looking at "+ gameObject.name);
    }

}
