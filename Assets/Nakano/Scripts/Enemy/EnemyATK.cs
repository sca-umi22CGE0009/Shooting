using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Create EnemyParams")]
public class EnemyParams : ScriptableObject
{
    public List<Level> stage = new();
}

[System.Serializable]
public class Level
{
    public int stageNum;
    public Enemy easy;
    public Enemy normal;
    public Enemy hard;
    public Enemy galaxy;

    public BossParamList boss = new();
}

[System.Serializable]
public class Enemy
{
    public List<EnemyParam> enemy = new();
}

[System.Serializable]
public class EnemyParam
{
    public string enemyID;
    public int atk;
}

[System.Serializable]
public class BossParamList
{
    public Boss easyBoss;
    public Boss normalBoss;
    public Boss hardBoss;
    public Boss galaxyBoss;
}

[System.Serializable]
public class Boss
{
    public int bossHp;
    public BossParam wave1;
    public BossParam wave2;
    public BossParam wave3;
    public BossParam wave4;
}

[System.Serializable]
public class BossParam
{
    public int atk;
}