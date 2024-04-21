using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerSkillManager : MonoBehaviour
{
    private SkillFactory.SkillKind selectedSkillKind;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("Skill [Lightning] Selected!");
            this.selectedSkillKind = SkillFactory.SkillKind.Lightning;
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            Debug.Log("Skill [Heal] Selected!");
            this.selectedSkillKind = SkillFactory.SkillKind.Heal;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            var skillFactory = new SkillFactory();
            var skill = skillFactory.Create(this.selectedSkillKind);
            skill.Play();
        }
    }

    public void Skill1Set()
    {
        Debug.Log("Skill [Lightning] Selected!");
        this.selectedSkillKind = SkillFactory.SkillKind.Lightning;
    }

    public void Skill2Set()
    {
        Debug.Log("Skill [Heal] Selected!");
        this.selectedSkillKind = SkillFactory.SkillKind.Heal;
    }

    public void SkillSetOK()
    {
        var skillFactory = new SkillFactory();
        var skill = skillFactory.Create(this.selectedSkillKind);
        skill.Play();
    }
}