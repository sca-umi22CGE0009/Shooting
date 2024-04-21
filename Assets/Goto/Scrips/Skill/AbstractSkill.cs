using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class AbstractSkill
{
    
    // スキル種別の抽象プロパティ
    public abstract SkillFactory.SkillKind SkillKind { get; }

    // スキル実行の抽象メソッド
    public abstract void Play();
}