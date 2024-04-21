using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    [SerializeField]
    //�@�|�[�Y�������ɕ\������UI�̃v���n�u
    private GameObject pauseUIPrefab;
    //�@�|�[�YUI�̃C���X�^���X
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
            //�|�[�YUI�̃A�N�e�B�u�A��A�N�e�B�u��؂�ւ�
            pauseUI.SetActive(!pauseUI.activeSelf);

            //�|�[�YUI���\������Ă��鎞�͒�~
            if (pauseUI.activeSelf)
            {
                Time.timeScale = 0f;

            }//�|�[�YUI���\������ĂȂ���Βʏ�ʂ�i�s
            else
            {
                Time.timeScale = 1f;
            }
        }
    }
}
    ---------------------------------------------------*/
