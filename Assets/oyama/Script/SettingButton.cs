using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignPattern;

public class SettingButton : MonoBehaviour
{
    public void OnClick()
    {
        Singleton<SoundSetting>.Instance.OpenWindow("UICamera");
    }
}