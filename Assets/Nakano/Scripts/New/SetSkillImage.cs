using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignPattern;
using CommonlyUsed;
using UnityEngine.UI;
using UnityEngine.Playables;

public class SetSkillImage : MonoBehaviour
{
    [SerializeField]
    Image[] skillImages;

    public Image[] SkillName
    {
        get { return skillImages; }
    }

    // Start is called before the first frame update
    void Start()
    {
        int index = 0;

        foreach (var img in skillImages)
        {
            string str = Singleton<SkillData>.Instance.SelectSkillData[index];
            if (str == null) str = "AlphaImage";

            ImageLoading.ImageLoadingAsync(img, str);
            index++;
        }
    }

}
