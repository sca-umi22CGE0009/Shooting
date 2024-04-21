using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �w�肵�������ɒ����I�ɔ���
/// </summary>
public class NormalBullet : MonoBehaviour
{
    [Header("���˕���"), Tooltip("�p�x0�̂Ƃ��E�ցA�p�x180�̂Ƃ�����\n�p�x�͎O�p�֐��̃C���[�W��")] public float angle;
    [HideInInspector] public float speed;
    Vector3 direction;

    public bool isReflect = false;
    bool reflect = false;
    bool mirrorReflect = false;

    [HideInInspector] public int num = 0;

    GameObject player;
    Vector3 playerPos;
    Vector3 bulletPos;

    void Start()
    {
        //�p�x��P�ʃx�N�g���ɕς���
        direction = AngleToVector3(angle);
    }

    void Update()
    {
        if(!reflect && !mirrorReflect)
        {
            //�p�x��P�ʃx�N�g���ɕς���
            direction = AngleToVector3(angle);
        }
        
        transform.Translate(direction * speed * Time.deltaTime);
    }

    /// <summary>
    /// �p�x����P�ʃx�N�g�����擾
    /// </summary>
    Vector2 AngleToVector3(float angle)
    {
        var radian = angle * (Mathf.PI / 180);
        return new Vector3(Mathf.Cos(radian), Mathf.Sin(radian), 0).normalized;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Wolf3_BigNormal(Clone)" && isReflect)
        {
            reflect = true;
            isReflect = false;
            direction *= -1;
        }

        if(collision.gameObject.tag == "Mirror")
        {
            mirrorReflect = true;
            direction = Vector3.Reflect(direction, Vector3.right);
        }
    }
}
