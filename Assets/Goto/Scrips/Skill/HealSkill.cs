using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealSkill : AbstractSkill
{
    // �X�L�����
    public override SkillFactory.SkillKind SkillKind
    {
        get { return SkillFactory.SkillKind.Heal; }
    }

    /// �X�L���u�q�[���v�̎��s
    public override void Play()
    {
        Debug.Log("Heal!");
    }
}