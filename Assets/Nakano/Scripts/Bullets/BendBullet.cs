using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BendBullet : MonoBehaviour
{
    float time;
    Vector2 startPos;
    Vector2 relayPos;
    Vector2 targetPos;

    [HideInInspector] public Vector2 relayAjust;
    [HideInInspector] public Vector2 targetAjust;

    [Header("���˕���")] public float angle;
    [HideInInspector] public float speed;

    Vector2 vec = new Vector2(0, 0);

    void Start()
    {
        startPos = this.transform.position;
        relayPos = Quaternion.Euler(0, 0, angle) * new Vector2(startPos.x + relayAjust.x, startPos.y + relayAjust.y);
        targetPos = Quaternion.Euler(0, 0, angle) * new Vector2(startPos.x + targetAjust.x, startPos.y + targetAjust.y);
    }

    void Update()
    {
        //�e�̐i�s��iLerp�̑�O�����ɓ����j
        time += Time.deltaTime;
        //�񎟃x�W�F�Ȑ����g��
        //�X�^�[�g���璆�p�n�_���Ȃ��x�N�g����𑖂�_�̈ʒu
        var firstVec = Vector2.Lerp(startPos, relayPos, time / speed);
        //���p�n�_����^�[�Q�b�g�܂ł��Ȃ��x�N�g����𑖂�_�̈ʒu
        var SecondVec = Vector2.Lerp(relayPos, targetPos, time / speed);
        //��̓�_���Ȃ��x�N�g����𑖂�_�i�e�j�̈ʒu
        vec = Vector2.Lerp(firstVec, SecondVec, time / speed);
        //�e�̈ʒu��������
        this.transform.position = new Vector3(vec.x, vec.y, 90);

        if(Vector3.Distance(this.transform.position, new Vector3(targetAjust.x, targetAjust.y, 0)) <= 20)
        {
            Destroy(this);
        }
    }
}
