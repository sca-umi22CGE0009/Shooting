using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningSkill: AbstractSkill
{
    // �X�L�����
    public override SkillFactory.SkillKind SkillKind
    {
        get { return SkillFactory.SkillKind.Lightning; }
    }

    // �X�L���u���C�g�j���O�v�̎��s
    public override void Play()
    {
        Debug.Log("Lightning!");
    }
}