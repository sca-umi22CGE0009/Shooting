using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignPattern;
using UnityEngine.UI;
using CommonlyUsed;

public class InitSettings : MonoBehaviour
{
    [SerializeField]
    GameObject player1;

    [SerializeField]
    GameObject player2;

    [SerializeField]
    GameObject[] bullets1;

    [SerializeField]
    GameObject[] bullets2;

    [SerializeField]
    BulletManager bulletManager;

    [SerializeField]
    PlayerManager playerManager;

    [SerializeField]
    Image resultCharaImage;

    [SerializeField]
    Image gameOverCharaImage;

    [SerializeField]
    Image resultBGImage;

    [SerializeField]
    Image gameOverBGImage;

    [SerializeField]
    Image charaIconImage;

    [SerializeField]
    AudioClip audioClip;

    AudioSource audioSource;
    // Start is called before the first frame update
    void Awake()
    {
        string charaName = Singleton<CharaData>.Instance.CharaName;

        if (charaName == "MagicalGirl")
        {
            bulletManager.Bullets = bullets1;
            playerManager.Player = player1;

            ImageLoading.ImageLoadingAsync(charaIconImage, "MagicalGirl_Icon");

            ImageLoading.ImageLoadingAsync(resultCharaImage, "MagicalGirl_Joy");
            ImageLoading.ImageLoadingAsync(gameOverCharaImage, "MagicalGirl_Damege");
        }

        if (charaName == "MagicSowrdsMan")
        {
            bulletManager.Bullets = bullets2;
            playerManager.Player = player2;

            ImageLoading.ImageLoadingAsync(charaIconImage, "MagicSwordsMan_Icon");

            ImageLoading.ImageLoadingAsync(resultCharaImage, "MagicSwordsMan_Joy");
            ImageLoading.ImageLoadingAsync(gameOverCharaImage, "MagicSwordsMan_Damege");
        }

        if (playerManager.stageName == "Stage1")
        {
            ImageLoading.ImageLoadingAsync(resultBGImage, "Stage1");
            ImageLoading.ImageLoadingAsync(gameOverBGImage, "Stage1");
        }

        if (playerManager.stageName == "Stage2")
        {
            ImageLoading.ImageLoadingAsync(resultBGImage, "Stage2");
            ImageLoading.ImageLoadingAsync(gameOverBGImage, "Stage2");
        }
    }

    private void Start()
    {
        audioSource = GameObject.Find("BGM").GetComponent<AudioSource>();

        audioSource.clip = audioClip;
        audioSource.Play();
    }
}
