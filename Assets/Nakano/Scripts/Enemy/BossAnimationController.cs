using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;

public class BossAnimationController : MonoBehaviour
{
    [SerializeField] BossController bossController;
    [SerializeField] Animator anim;

    void Start()
    {
    }

    void Update()
    {
    }

    public void Wave1()
    {
        if(bossController.BossWaveNum == 1)
        {
            anim.SetTrigger("Wave1");
        }
    }

    public void Wave2()
    {
        if (bossController.BossWaveNum == 2)
        {
            anim.SetTrigger("Wave2");
        }
    }

    public void Wave3()
    {
        if (bossController.BossWaveNum == 3)
        {
            anim.SetTrigger("Wave3");
        }
    }

    public void Wave4()
    {
        if (bossController.BossWaveNum == 4)
        {
            anim.SetTrigger("Wave4");
        }
    }
}
