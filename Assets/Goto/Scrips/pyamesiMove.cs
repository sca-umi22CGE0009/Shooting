using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pyamesiMove : MonoBehaviour
{
 
    //===== ��`�̈� =====
   
    [SerializeField] Animator MoveObject;
    private Animator anim;
    
    void Start()
    {
        //�ϐ�anim�ɁAAnimator�R���|�[�l���g��ݒ肷��
        
        MoveObject = gameObject.GetComponent<Animator>();
       // MoveObject.speed = 0;
    }

    //===== �又�� =====
    void Update()
    {
        //�����A�X�y�[�X�L�[�������ꂽ��Ȃ�
        if (Input.GetKey(KeyCode.Space))
        {
            MoveObject.SetBool("Joy", true);
            Debug.Log("�߂�");
            // Bool�^�̃p�����[�^�[�ł���blRot��True�ɂ���
            // anim.SetTrigger("Onoff");
        }
    }

    public void OnClick()
    {

        MoveObject.Play("New Animationzoro");
       // MoveObject.speed = 1;

        MoveObject.SetBool("Joy",true);
        Debug.Log("�A�j���[�V�����Đ�");
       

    }
    public void OnClickMove() // �{�^�����N���b�N�������Ɏ��s���郁�\�b�h
    {
       // MoveObject.speed = 1;
      //  MoveObject.Play("MoveMain1 "); //Animation�̖��O��ύX���Ă���ꍇ�͂��̖��O�ɕύX
    }
}