using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    [SerializeField]
    //　ポーズした時に表示するUIのプレハブ
    private GameObject pauseUIPrefab;
    //　ポーズUIのインスタンス
    private GameObject pauseUIInstance;
    
    bool justOnce = true;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            
            if (pauseUIInstance == null)
            {
                pauseUIInstance = GameObject.Instantiate(pauseUIPrefab) as GameObject;
                Time.timeScale = 0f;
            }
            else
            {
                Destroy(pauseUIInstance);
                Time.timeScale = 1f;
                
            }
        }
    }
}


    /*------------------------------------------
     

    [SerializeField]
    private GameObject pauseUI;

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Approximately(Time.timeScale, 0f))
        {
            return;
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            //ポーズUIのアクティブ、非アクティブを切り替え
            pauseUI.SetActive(!pauseUI.activeSelf);

            //ポーズUIが表示されている時は停止
            if (pauseUI.activeSelf)
            {
                Time.timeScale = 0f;

            }//ポーズUIが表示されてなければ通常通り進行
            else
            {
                Time.timeScale = 1f;
            }
        }
    }
}
    ---------------------------------------------------*/
