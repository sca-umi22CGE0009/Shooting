using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public  enum Skill
{
    _None,
    Stargazer,
    Pentagram,
    Energyfunnel,
    Mysticfield,
    Skill5,
    Skill6,
}
public class SkillDefine : MonoBehaviour
{
    
    public static Skill [] skillSets =new Skill[4];
    

 //  public static Skill skillSet1;
  
  //  public static Skill skillSet2;
   
  //  public static Skill skillSet3;
   
  //  public static Skill  skillSet4;

    public Skill [] SkillSets
    {
        get { return skillSets;}
        set { skillSets = value; }
    }
   
   

    public static Dictionary<Skill, string> dic_SkillName = new Dictionary<Skill, string>()
    {
        {Skill._None, "�X�L������"},
        {Skill. Stargazer, "�X�^�[�Q�C�U�["},
        {Skill.Pentagram, "�y���^�O����"},
        {Skill.Energyfunnel, "�G�l���M�[�t�@���l��"},
        {Skill.Mysticfield, "�~�X�e�B�b�N�t�B�[���h"},
        {Skill.Skill5, "�X�L��5"},
        {Skill.Skill6, "�X�L��6"},
    };
    // Start is called before the first frame update
    void Start()
    {
        Skill skill= Skill._None;
        Debug.Log(skill);

        for(int i = 0; i < skillSets.Length; i++)
        {
            skillSets[i] = Skill._None;
        }
    }

    void SkillChange()
    {

    }
    // Update is called once per frame
    void Update()
    {
      //  Debug.Log("slot1"+skillSets[0]);
       // Debug.Log("slot2"+skillSets[1]);
       // Debug.Log("slot3"+skillSets[2]);
       // Debug.Log("slot4"+skillSets[3]);

        SkillChange();
    }
 
}
