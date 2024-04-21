using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using DesignPattern;

public class DecisionButton : MonoBehaviour
{
    [SerializeField]
    PlayableDirector decisionCheckAnimation;

    [SerializeField]
    SetSkillIcon[] setSkillIcon;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        decisionCheckAnimation.Stop();
    }

    public void OnComplete()
    {
        int count = 0;

        for(int i = 0; i < setSkillIcon.Length; i++)
        {
            if (setSkillIcon[i].CollisionName == "")
            {
                count++;
            }
        }

        if(count == 0)
        {
            for(int i = 0; i < setSkillIcon.Length; i++)
            {
                Singleton<SkillData>.Instance.SelectSkillData[i] = setSkillIcon[i].CollisionName;
            }

            decisionCheckAnimation.Play();
        }
    }
}