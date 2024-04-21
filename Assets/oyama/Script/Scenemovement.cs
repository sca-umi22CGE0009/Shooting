using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartController : MonoBehaviour
{
    [SerializeField]
    AudioClip audioClip;

    AudioSource audioSource;
    
    private void Start()
    {
        audioSource = GameObject.Find("BGM").GetComponent<AudioSource>();
        audioSource.clip = audioClip;
        audioSource.Play();
    }


    public void SwitchScene()
    {
        SceneManager.LoadScene("02_CharacterSelect");
    }
}