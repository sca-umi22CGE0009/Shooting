using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SETexture : MonoBehaviour
{
    [SerializeField] Slider SESlider;
    [SerializeField] Sprite[] Icon;
    private Image image;

    void Start()
    {
        image = GetComponent<Image>();
    }

    void Update()
    {
        if (SESlider.value == 0)
        {
            image.sprite = Icon[0];
        }
        else if (SESlider.value < 0 && SESlider.value > -40)
        {
            image.sprite = Icon[1];
        }
        else if (SESlider.value <= -40 && SESlider.value > -80)
        {
            image.sprite = Icon[2];
        }
        else if (SESlider.value == -80)
        {
            image.sprite = Icon[3];
        }
    }
}
