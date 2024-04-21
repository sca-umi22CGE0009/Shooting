using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CommonlyUsed;
using DesignPattern;
using UnityEngine.UI;

public class CharacterDescription_SetIcon : MonoBehaviour
{
    [SerializeField]
    Image[] skillIconImages;


    private void OnEnable()
    {
        for (int i = 0; i < skillIconImages.Length; i++)
        {
            skillIconImages[i].enabled = true;
            try
            {
                ImageLoading.ImageLoadingAsync(skillIconImages[i], Singleton<SkillData>.Instance.SelectSkillData[i]);
            }
            catch (System.NullReferenceException)
            {
                skillIconImages[i].enabled = false;
            }
        }
    }
}