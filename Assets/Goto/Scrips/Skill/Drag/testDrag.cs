using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class testDrag : MonoBehaviour
{ // �h���b�v�\�I�u�W�F�N�g
    public Draggable dropObj;

    void Start()
    {
       // dropObj.beforeBeginDrag = () =>
        {
            Debug.Log("�h���b�O�O�ɌĂяo����鏈��");
        };
       // dropObj.onDropSuccess = (MonoBehaviour area, Action resetAction) =>
        {
            Debug.Log("�h���b�O�������ɌĂяo����鏈��");
       //     resetAction.Invoke();
       //     resetAction.Invoke();
        };
      //  dropObj.onDropFail = (Action resetAction) =>
        {
            Debug.Log("�h���b�O���s���ɌĂяo����鏈��");
        //    resetAction.Invoke();
        };
    }
}