using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSerect_Init : MonoBehaviour
{
    [SerializeField]
    CharaSelectData charaSelectData;

    // Start is called before the first frame update
    void Start()
    {
        charaSelectData.SkillSelect();    
    }

}
