using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeypadControllerBunker : MonoBehaviour
{
    // public DoorController door;
    public string password;

    public int passwordLimit;

    public Text passwordText;

    public GameObject door;

    public GameObject speechText;

    public GameObject speechTextCall;

    public GameObject hole;

    [Header("Audio")]
    public AudioSource audioSource;

    public AudioClip correctSound;

    public AudioClip wrongSound;
    
    public AudioSource generatorStartAudioSource;

    public AudioClip generatorStart;


    private void Start()
    {
        passwordText.text = "";
    }

    public void PasswordEntry(string number)
    {
        if (number == "Clear")
        {
            Clear();
            return;
        }
        else if (number == "Enter")
        {
            Enter();
            return;
        }

        int length = passwordText.text.ToString().Length;
        if (length < passwordLimit)
        {
            passwordText.text = passwordText.text + number;
        }
    }

    public void Clear()
    {
        passwordText.text = "";
        passwordText.color = Color.white;
    }

    private void Enter()
    {
        if (passwordText.text == password)
        {
            // door.lockedByPassword = false;
            if (audioSource != null) audioSource.PlayOneShot(correctSound);

            passwordText.color = Color.green;
            StartCoroutine(waitAndClear());
            door.SetActive(false);
            hole.SetActive(true);
            generatorStartAudioSource.PlayOneShot(generatorStart);
            StartCoroutine(speechTextIteraction());
        }
        else
        {
            if (audioSource != null) audioSource.PlayOneShot(wrongSound);

            passwordText.color = Color.red;
            StartCoroutine(waitAndClear());
        }
    }

    IEnumerator speechTextIteraction(){
        speechText.SetActive(true);
        yield return new WaitForSeconds(5);
        speechText.SetActive(false);
        yield return new WaitForSeconds(1);
        speechTextCall.SetActive(true);
        yield return new WaitForSeconds(5);
        speechTextCall.SetActive(false);
    }

    IEnumerator waitAndClear()
    {
        yield return new WaitForSeconds(0.75f);
        Clear();
    }
}
