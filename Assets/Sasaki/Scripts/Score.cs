using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField,Header("���U���g")] private GameObject result;
    [SerializeField, Header("�Q�[���I�[�o�[")] private GameObject gameOver;
    // Start is called before the first frame update
    void Start()
    {
        result.gameObject.SetActive(false);
        gameOver.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

//���L
public static class GlobalVariables
{
    public static float AliveTime = 0f; //��������
    public static int Score = 0; //�X�R�A
}
