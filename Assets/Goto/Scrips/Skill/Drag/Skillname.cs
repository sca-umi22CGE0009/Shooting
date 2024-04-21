using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Skillname : MonoBehaviour
{
    [SerializeField]
    string skillset;
    string  skillname;

    [SerializeField]
    Skill skill1;
    [SerializeField]
    Skill skill2;
    [SerializeField]
    Skill skill3;
    [SerializeField]
    Skill skill4;
    // [SerializeField] private GameObject TargetSprite;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /*private void OnTriggerStay2D(Collider2D collision)
    {

        GameObject obj = collision.gameObject;
        Image mg = obj.GetComponent<Image>();
        skillname = mg.sprite.name;
      
        SkillDefine skill = GameObject.Find("SkillManager").GetComponent<SkillDefine>();
        if(skillset=="1")
        {
            Debug.Log(skillname);
            skill.SkillSets[0] = (Skill)int.Parse(skillname);
            skill1= (Skill)int.Parse(skillname);
        }
        else
        {
            skill.SkillSets[0] = Skill._None;
            skill1 = Skill._None;
        }
        if (skillset == "2")
        {
            skill.SkillSets[1] = (Skill)int.Parse(skillname);
            skill2 = (Skill)int.Parse(skillname);
        }
        else
        {
            skill.SkillSets[1] = Skill._None;
            skill2 = Skill._None;
        }


        if (skillset == "3") 
            
        {
            skill.SkillSets[2] = (Skill)int.Parse(skillname);
            skill3 = (Skill)int.Parse(skillname);
        }
        else
        {
            skill.SkillSets[2] = Skill._None;
            skill3 = Skill._None;
        }

        if (skillset == "4")
        {
            skill.SkillSets[3] = (Skill)int.Parse(skillname);
            skill4 = (Skill)int.Parse(skillname);
        }
        else
        {
            skill.SkillSets[3] = Skill._None;
            skill4 = Skill._None;
        }


    }*/

}
