using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircleFade : MonoBehaviour
{
    [SerializeField] Image unMask;
    RectTransform rt;
    [SerializeField, Header("フェード速度")] float speed;
    [SerializeField, Header("マスクしない範囲の最大サイズ")] float size;
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
        get { return fadeOutEnd; }
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
        this.gameObject.SetActive(false);
    }

    void Update()
    {
        if(isFadeOut)
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
        if(rt.localScale.x > 0)
        {
            Vector3 v = new Vector3(1, 1, 1) * speed;
            rt.localScale -= v * Time.deltaTime;
        }
        else if(rt.localScale.x <= 0)
        {
            rt.localScale = new Vector3(0, 0, 0);
            isFadeOut = false;

            fadeOutEnd = true;
        }
    }

    void FadeIn()
    {
        if (rt.localScale.x < size)
        {
            Vector3 v = new Vector3(1, 1, 1) * speed;
            rt.localScale += v * Time.deltaTime;
        }
        else if (rt.localScale.x >= size)
        {
            rt.localScale = new Vector3(size, size, size);
            isFadeIn = false;
            this.gameObject.SetActive(false);

            fadeInEnd = true;
        }
    }

    public void FadeOutStart()
    {
        this.gameObject.SetActive(true);
        
        if(fadeOutCount == 0)
        {
            rt.localScale = new Vector3(size, size, size);
            isFadeOut = true;
            isFadeIn = false;

            fadeOutEnd = false;
            fadeOutCount++;
        }
    }

    public void FadeInStart()
    {
        this.gameObject.SetActive(true);
        
        if(fadeInCount == 0)
        {
            rt.localScale = new Vector3(0, 0, 0);
            isFadeIn = true;
            isFadeOut = false;

            fadeInEnd = false;
        }
    }
}
