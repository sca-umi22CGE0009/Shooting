using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillOk : MonoBehaviour
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
        var skillFactory = new SkillFactory();
        var skill = skillFactory.Create(this.selectedSkillKind);
        skill.Play();
    }

}
