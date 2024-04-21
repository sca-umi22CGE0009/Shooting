using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MasterTexture : MonoBehaviour
{
    [SerializeField] Slider MasterSlider;
    [SerializeField] Sprite[] Icon;
    private Image image;

    void Start()
    {
        image = GetComponent<Image>();
    }


    void Update()
    {
        if (MasterSlider.value == 0)
        {
            image.sprite = Icon[0];
        }
        else if (MasterSlider.value < 0 && MasterSlider.value > -40)
        {
            image.sprite = Icon[1];
        }
        else if (MasterSlider.value <= -40 && MasterSlider.value > -80)
        {
            image.sprite = Icon[2];
        }
        else if (MasterSlider.value == -80)
        {
            image.sprite = Icon[3];
        }
    }
}
