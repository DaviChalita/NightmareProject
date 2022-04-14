using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoorInteraction : Interactable
{
    public GameObject text;
    public GameObject door;

    public override void OnFocus()
    {
       // print("Looking at "+ door.name);
    }

    public override void OnInteract()
    {
        // print("Interacted with "+ key.name);     
       StartCoroutine(Timer());
    }

    public override void OnLoseFocus()
    {
        // print("Stopped looking at "+ key.name);
    }

    IEnumerator Timer()
    {
        text.SetActive(true);
        yield return new WaitForSeconds(5);
        text.SetActive(false);
    }
}
