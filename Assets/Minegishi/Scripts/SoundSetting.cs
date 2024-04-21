using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignPattern;

public class SoundSetting : Singleton<SoundSetting>
{
    [SerializeField] GameObject SettingCanvas;

    [SerializeField]
    Canvas canvas;

    public override bool DestroyTragetGameObject => true;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void OnClick()
    {
        SettingCanvas.SetActive(false);
    }

    public void OpenWindow(string tagName)
    {
        canvas.worldCamera = GameObject.FindGameObjectWithTag(tagName).GetComponent<Camera>();
        SettingCanvas.SetActive(true);
    }
}