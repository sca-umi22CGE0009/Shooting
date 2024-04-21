using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningSkill: AbstractSkill
{
    // スキル種別
    public override SkillFactory.SkillKind SkillKind
    {
        get { return SkillFactory.SkillKind.Lightning; }
    }

    // スキル「ライトニング」の実行
    public override void Play()
    {
        Debug.Log("Lightning!");
    }
}