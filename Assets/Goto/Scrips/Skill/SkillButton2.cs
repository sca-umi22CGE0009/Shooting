using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillButton2 : MonoBehaviour
{

    private SkillFactory.SkillKind selectedSkillKind;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnClick()
    {
        SkillFactory sk2 = GetComponent<SkillFactory>();
        sk2.Skill2Set();
    }
}
