using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CommonlyUsed;
using DesignPattern;

public class CharaSelectData : MonoBehaviour
{
    [SerializeField]
    GameObject MagicalGirlImage;

    [SerializeField]
    GameObject MagicSwordsMan;

    [SerializeField]
    Loading loadingA,loadingB;

    [SerializeField,Header("CharacterTextBox")]
    Image characterInfoTextBox;

    [SerializeField, Header("スキル動画フレーム")]
    Image skillPreviewImage;
    [SerializeField,Header("スキル情報フレーム")]
    Image skillInfoImage;

    [SerializeField]
    GameObject magicalGirlText;

    [SerializeField]
    GameObject magicSwordsManText;

    [SerializeField]
    AudioClip charaSelect,skillSelect;

    public string CharaName { get; set; } = "";

    AudioSource audioSource;

    private void Start()
    {
        audioSource = GameObject.Find("BGM").GetComponent<AudioSource>();
        audioSource.clip = charaSelect;
        audioSource.Play();
    }

    public void CharaSelect(Button button)
    {
        Singleton<CharaData>.Instance.CharaName = CharaName;

        audioSource.clip = charaSelect;
        audioSource.Play();

        if (CharaName == "MagicalGirl")
        {
            magicalGirlText.SetActive(true);
            magicSwordsManText.SetActive(false);

            MagicalGirlImage.SetActive(true);
            MagicSwordsMan.SetActive(false);
            button.onClick.AddListener(loadingA.LoadNextScene);
            ImageLoading.ImageLoadingAsync(characterInfoTextBox,"CharacterTextBox_A");
        }

        //スプライトがSwordではなくSowrd
        if (CharaName == "MagicSowrdsMan")
        {
            magicalGirlText.SetActive(false);
            magicSwordsManText.SetActive(true);

            MagicalGirlImage.SetActive(false);
            MagicSwordsMan.SetActive(true);
            button.onClick.AddListener(loadingB.LoadNextScene);
            ImageLoading.ImageLoadingAsync(characterInfoTextBox, "CharacterTextBox_B");
        }
    }

    public void SkillSelect()
    {
        audioSource.clip = skillSelect;
        audioSource.Play();

        if (CharaName == "MagicalGirl")
        {
            ImageLoading.ImageLoadingAsync(skillInfoImage, "SkillInfo_Pink");
            ImageLoading.ImageLoadingAsync(skillPreviewImage, "SkillPreview_Pink");
        }

        //スプライトがSwordではなくSowrd
        if (CharaName == "MagicSowrdsMan")
        {
            ImageLoading.ImageLoadingAsync(skillInfoImage, "SkillInfo_Blue");
            ImageLoading.ImageLoadingAsync(skillPreviewImage, "SkillPreview_Blue");
        }
    }
}