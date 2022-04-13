using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyInteraction : Interactable
{
    public MeshRenderer key;

    public GameObject door;

    public GameObject text;

    public override void OnFocus()
    {
        // print("Looking at "+ key.name);
    }

    public override void OnInteract()
    {
        // print("Interacted with "+ key.name);        
        StartCoroutine(Timer());
        key.enabled = false;
        door.SetActive(false);
    }

    public override void OnLoseFocus()
    {
        // print("Stopped looking at "+ key.name);
    }

    IEnumerator Timer()
    {
        text.SetActive(true);
        yield return new WaitForSeconds(7);
        text.SetActive(false);
    }
}
