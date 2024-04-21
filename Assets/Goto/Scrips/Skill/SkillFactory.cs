using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class SkillFactory: MonoBehaviour
{
    private SkillFactory.SkillKind selectedSkillKind;
    // スキル一覧
    static readonly AbstractSkill[] skills = {
        new LightningSkill(),
        new HealSkill()
    };

    /// スキルのenum
    public enum SkillKind
    {
        Lightning,
        Heal
    }

    // SkillKindを引数に、それに応じたスキルを返す
    public AbstractSkill Create(SkillKind skillKind)
    {
        return skills.SingleOrDefault(skill => skill.SkillKind == skillKind);
    }

    public void Skill1Set()
    {
        Debug.Log("Skill [Lightning] Selected!");
        this.selectedSkillKind = SkillKind.Lightning;
    }

    public void Skill2Set()
    {
        Debug.Log("Skill [Heal] Selected!");
        this.selectedSkillKind =SkillKind.Heal;
    }

    public void SkillSetOK()
    {
        Create(selectedSkillKind).Play();
        
    }
}