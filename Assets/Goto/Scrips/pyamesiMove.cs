using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pyamesiMove : MonoBehaviour
{
 
    //===== 定義領域 =====
   
    [SerializeField] Animator MoveObject;
    private Animator anim;
    
    void Start()
    {
        //変数animに、Animatorコンポーネントを設定する
        
        MoveObject = gameObject.GetComponent<Animator>();
       // MoveObject.speed = 0;
    }

    //===== 主処理 =====
    void Update()
    {
        //もし、スペースキーが押されたらなら
        if (Input.GetKey(KeyCode.Space))
        {
            MoveObject.SetBool("Joy", true);
            Debug.Log("戻る");
            // Bool型のパラメーターであるblRotをTrueにする
            // anim.SetTrigger("Onoff");
        }
    }

    public void OnClick()
    {

        MoveObject.Play("New Animationzoro");
       // MoveObject.speed = 1;

        MoveObject.SetBool("Joy",true);
        Debug.Log("アニメーション再生");
       

    }
    public void OnClickMove() // ボタンをクリックした時に実行するメソッド
    {
       // MoveObject.speed = 1;
      //  MoveObject.Play("MoveMain1 "); //Animationの名前を変更している場合はその名前に変更
    }
}