using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Create PlayerParams")]
public class PlayerParams : ScriptableObject
{
    public List<LevelParam> character = new();
}

[System.Serializable]
public class LevelParam
{
    public string charaName;
    public PlayerParam easy;
    public PlayerParam normal;
    public PlayerParam hard;
    public PlayerParam galaxy;
}

[System.Serializable]
public class PlayerParam
{
    public int singleAttack;
    public int lightAttack;
    public int heavyAttack;
}