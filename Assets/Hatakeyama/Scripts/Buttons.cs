using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttons : MonoBehaviour
{
    public void TitleButtonClick()
    {
        Debug.Log("�^�C�g���ֈړ�");
    }

    public void CharacterSelectButtonClick()
    {
        Debug.Log("�L�����N�^�[�I����ʂֈړ�");
    }

    public void PuseExitButtonClick()
    {
        Pose.poseInputCount++;
    }

    public void ButtonEnter()
    {
        transform.localScale *= 1.5f;
    }
    public void ButtonExit()
    {
        transform.localScale *= (float)2 / 3;
    }
}
