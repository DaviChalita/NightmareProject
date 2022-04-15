using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleInteract : Interactable
{
    public GameObject black;
    public GameObject schopenhauer;
    public override void OnFocus()
    {
        // print("Looking at "+ key.name);
    }

    public override void OnInteract()
    {
       black.SetActive(true);
       schopenhauer.SetActive(true);
       StartCoroutine(Timer());
    }

    public override void OnLoseFocus()
    {
        // print("Stopped looking at "+ key.name);
    }

    IEnumerator Timer()
    {        
        yield return new WaitForSeconds(5.0f);        
       Application.Quit();
    }
}
