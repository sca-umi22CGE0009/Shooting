using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGMTexture : MonoBehaviour
{
    [SerializeField] Slider BGMSlider;
    [SerializeField] Sprite[] Icon;
    private Image image;

    void Start()
    {
        image = GetComponent<Image>();
    }

    void Update()
    {
        if (BGMSlider.value == 0)
        {
            image.sprite = Icon[0];
        }
        else if (BGMSlider.value < 0 && BGMSlider.value > -40)
        {
            image.sprite = Icon[1];
        }
        else if (BGMSlider.value <= -40 && BGMSlider.value > -80)
        {
            image.sprite = Icon[2];
        }
        else if (BGMSlider.value == -80)
        {
            image.sprite = Icon[3];
        }
    }
}
