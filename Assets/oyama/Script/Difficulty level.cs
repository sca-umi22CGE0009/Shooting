using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Difficultylevel : MonoBehaviour
{

     // ��Փx�I����UI���܂Ƃ߂��L�����o�X�O���[�v
     [SerializeField] CanvasGroup groupDifficult; 
    // �I�����ꂽ��Փx
    public static string difficulty;

    public void ButtonClick(string button)
    {
         switch (button)
        {
            case "Hard":
                difficulty = "Hard"; 
                // �A�Ŗh�~�̂��ߖ�����
               groupDifficult.interactable = false; 
                // UI���t�F�[�h�A�E�g�����鏈��
                FadeOutSelect();
                break;
            case "Normal":
                difficulty = "Normal";
                groupDifficult.interactable = false;
                break;
        }
 void FadeOutSelect()
    {
        throw new NotImplementedException();
    }
}

}
