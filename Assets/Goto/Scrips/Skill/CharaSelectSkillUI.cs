using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CharaSelectSkillUI : MonoBehaviour
{
    [SerializeField] private int[] setSkillsNumber = new int[4];
    [SerializeField] private Sprite[] skillSprite = new Sprite[6];
    [SerializeField] private Image[] setSkills = new Image[4];
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 4; ++i)
        {
            //setSkillsNumber[i]=(int)SkillDefine.skillSets[i];
            setSkills[i].sprite = skillSprite[(int)SkillDefine.skillSets[i]];
            //setSkills[i].sprite = skillSprite[setSkillsNumber[i]];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
