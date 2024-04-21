using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HorizonFade : MonoBehaviour
{
    [SerializeField] Image unMask;
    RectTransform rt;
    [SerializeField, Header("フェード速度")] float speed;
    bool isFadeOut = false;
    bool isFadeIn = false;

    bool fadeOutEnd = false;
    bool fadeInEnd = false;

    int fadeOutCount = 0;
    int fadeInCount = 0;

    /// <summary>
    /// フェードアウト終了したかどうかのフラグ
    /// </summary>
    public bool FadeOutEnd
    {
        get { return fadeOutEnd;}
    }

    /// <summary>
    /// フェードイン終了したかどうかのフラグ
    /// </summary>
    public bool FadeInEnd
    {
        get { return fadeInEnd; }
    }

    void Awake()
    {
        rt = unMask.GetComponent<RectTransform>();
    }

    void Update()
    {
        if (isFadeOut)
        {
            FadeOut();
        }

        if (isFadeIn)
        {
            FadeIn();
        }
    }

    void FadeOut()
    {
        if(rt.localPosition.x < 2000)
        {
            rt.Translate(speed * Time.deltaTime, 0, 0);
        }
        else if(rt.localPosition.x >= 2000)
        {
            rt.localPosition = new Vector3(2000, 0, 0);
            isFadeOut = false;
            isFadeIn = false;
            
            fadeOutEnd = true;
            fadeOutCount = 0;
        }
    }

    void FadeIn()
    {
        if (rt.localPosition.x < 0)
        {
            rt.Translate(speed * Time.deltaTime, 0, 0);
        }
        else if (rt.localPosition.x >= 0)
        {
            rt.localPosition = new Vector3(0, 0, 0);
            isFadeIn = false;
            isFadeOut = false;

            fadeInEnd = true;
            fadeInCount = 0;

            this.gameObject.SetActive(false);
        }
    }

    public void FadeOutStart()
    {
        this.gameObject.SetActive(true);

        if (fadeOutCount == 0)
        {
            rt.localPosition = new Vector3(0, 0, 0);
            isFadeOut = true;
            isFadeIn = false;

            fadeOutEnd = false;
            fadeOutCount++;
        }
    }

    public void FadeInStart()
    {
        this.gameObject.SetActive(true);
        if (fadeInCount == 0)
        {
            rt.localPosition = new Vector3(-2000, 0, 0);
            isFadeIn = true;
            isFadeOut = false;

            fadeInEnd = false;
            fadeInCount++;
        }
    }
}
