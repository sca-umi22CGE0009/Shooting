using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class testDrag : MonoBehaviour
{ // ドロップ可能オブジェクト
    public Draggable dropObj;

    void Start()
    {
       // dropObj.beforeBeginDrag = () =>
        {
            Debug.Log("ドラッグ前に呼び出される処理");
        };
       // dropObj.onDropSuccess = (MonoBehaviour area, Action resetAction) =>
        {
            Debug.Log("ドラッグ成功時に呼び出される処理");
       //     resetAction.Invoke();
       //     resetAction.Invoke();
        };
      //  dropObj.onDropFail = (Action resetAction) =>
        {
            Debug.Log("ドラッグ失敗時に呼び出される処理");
        //    resetAction.Invoke();
        };
    }
}