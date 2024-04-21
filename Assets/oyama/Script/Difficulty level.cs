using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Difficultylevel : MonoBehaviour
{

     // 難易度選択のUIをまとめたキャンバスグループ
     [SerializeField] CanvasGroup groupDifficult; 
    // 選択された難易度
    public static string difficulty;

    public void ButtonClick(string button)
    {
         switch (button)
        {
            case "Hard":
                difficulty = "Hard"; 
                // 連打防止のため無効化
               groupDifficult.interactable = false; 
                // UIをフェードアウトさせる処理
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
