using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillReset : MonoBehaviour
{
    public delegate void SkillRelease();

    public SkillRelease skillRelease;

    public void OnClick()
    {
        skillRelease?.Invoke();
    }    
}
