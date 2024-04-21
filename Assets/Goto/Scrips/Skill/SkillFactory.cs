using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class SkillFactory: MonoBehaviour
{
    private SkillFactory.SkillKind selectedSkillKind;
    // �X�L���ꗗ
    static readonly AbstractSkill[] skills = {
        new LightningSkill(),
        new HealSkill()
    };

    /// �X�L����enum
    public enum SkillKind
    {
        Lightning,
        Heal
    }

    // SkillKind�������ɁA����ɉ������X�L����Ԃ�
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