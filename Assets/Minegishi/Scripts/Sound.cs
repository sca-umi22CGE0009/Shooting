using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Sound : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Slider MasterSlider;
    [SerializeField] Slider BGMSlider;
    [SerializeField] Slider SESlider;

    void Start()
    {
        //Master
        audioMixer.GetFloat("Master", out float masterVolume);
        MasterSlider.value = masterVolume; ;
        //BGM
        audioMixer.GetFloat("BGM", out float bgmVolume);
        BGMSlider.value = bgmVolume;
        //SE
        audioMixer.GetFloat("SE", out float seVolume);
        SESlider.value = seVolume;
    }

    void Update()
    {
        
    }

    public void SetMaster(float volume)
    {
        audioMixer.SetFloat("Master", volume);
    }

    public void SetBGM(float volume)
    {
        audioMixer.SetFloat("BGM", volume);
    }

    public void SetSE(float volume)
    {
        audioMixer.SetFloat("SE", volume);
    }
}
