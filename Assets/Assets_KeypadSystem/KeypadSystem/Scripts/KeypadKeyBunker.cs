using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeypadKeyBunker : MonoBehaviour
{
    public string key;
    
    public void SendKey()
    {
        this.transform.GetComponentInParent<KeypadControllerBunker>().PasswordEntry(key);
    }

}
