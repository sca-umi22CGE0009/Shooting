using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Slider masterSlider;
    [SerializeField] Slider bgmSlider;
    [SerializeField] Slider seSlider;
    internal static object instance;

    void Start()
    {
        masterSlider.onValueChanged.AddListener(SetAudioMixerMaster);
        bgmSlider.onValueChanged.AddListener(SetAudioMixerBGM);
        seSlider.onValueChanged.AddListener(SetAudioMixerSE);
    }

    void Update()
    {
        
    }

    public void SetAudioMixerMaster(float value)
    {
        //5�i�K�␳
        value /= 100;
        //-80�`0�ɕϊ�
        var volume = Mathf.Clamp(Mathf.Log10(value) * 20f, -80, 0f);
        //audioMixer�ɑ��
        audioMixer.SetFloat("Master", volume);
        Debug.Log($"Master:{volume}");
    }

    public void SetAudioMixerBGM(float value)
    {
        //5�i�K�␳
        value /= 100;
        //-80�`0�ɕϊ�
        var volume = Mathf.Clamp(Mathf.Log10(value) * 20f,-80,0f);
        //audioMixer�ɑ��
        audioMixer.SetFloat("BGM", volume);
        Debug.Log($"BGM:{volume}");
    }

    public void SetAudioMixerSE(float value)
    {
        //5�i�K�␳
        value /= 5;
        //-80�`0�ɕϊ�
        var volume = Mathf.Clamp(Mathf.Log10(value) * 20f, -80, 0f);
        //audioMixer�ɑ��
        audioMixer.SetFloat("SE",volume);
        Debug.Log($"SE:{volume}");
    }
}
