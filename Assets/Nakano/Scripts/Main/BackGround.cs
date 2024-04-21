using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    [SerializeField, Header("�ړ�����")] float time;
    [SerializeField, Header("�ړ�����"), Tooltip("�ړ����x�͈ړ�����/�ړ����Ԃ���Z�o")] float distance;
    [SerializeField, Header("y���W�������܂ōs������->")] float minY;
    [SerializeField, Header("->���̍��W�ɔ��")] float maxY;

    float speed = 0;
    float moveDis = 0;
    float nextY;

    bool isMove = false;

    public float MoveTime
    {
        get { return time; }
        set { time = value; }
    }

    public float MoveDistance
    {
        get { return distance; }
        set { distance = value; }
    }

    void Update()
    {
        if(isMove)
        {
            if (distance < moveDis)
            {
                transform.Translate(Vector3.up * speed * Time.deltaTime);
                moveDis += speed * Time.deltaTime;
            }
            else
            {
                transform.position = new Vector3(0, nextY, 100);
                isMove = false;
            }
        }

        if(transform.position.y <= minY)
        {
            transform.position = new Vector3(0, maxY, 100);
        }
    }

    public void Move()
    {
        //���x�v�Z
        speed = distance / time;

        //�ړ���
        moveDis = 0;

        //�ڎw�����W
        nextY = transform.position.y + distance;

        isMove = true;
    }
}
