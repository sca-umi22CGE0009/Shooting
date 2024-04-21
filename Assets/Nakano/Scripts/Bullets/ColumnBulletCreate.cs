using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnBulletCreate : MonoBehaviour
{
    [SerializeField, Header("NormalBullet")] GameObject prefabs;
    [SerializeField, Header("��x�̐�����")] int bulletNum;
    [SerializeField, Header("�N�[���^�C��")] float coolTime;
    [SerializeField, Header("�e��")] float speed;

    [SerializeField, Header("�p�x"), Tooltip("NormalBullets�Ŏw�肵���p�x���D�悳���")] float angle = 0;
    [SerializeField, Header("�c��"), Tooltip("���ɉ����Ԃ�(�c�������邩) ���͂�1�ȏ�")] int way = 1;
    [SerializeField, Header("�c�񓯎m�̋���")] float distance;

    [SerializeField, Header("�S�e��������")] bool isAll = false;
    [SerializeField, Header("�S�e�����������̒e�Ԃ̋��� �N�[���^�C���̑���")] float dis;

    NormalBullet normalBullet;

    public bool isCreate = false;
    bool tmp = false;
    int count = 0;

    Canvas canvas;
    RectTransform rt;
    Vector3 pos;
    TransformChange tc;

    void Start()
    {
        tc = gameObject.AddComponent<TransformChange>();
        normalBullet = prefabs.GetComponent<NormalBullet>();
        normalBullet.speed = speed;

        if (way < 1) { way = 1; } //�w�肳�ꂽway��1�����̂Ƃ��A1�ɂ���

        canvas = GameObject.FindWithTag("Canvas").GetComponent<Canvas>();
        rt = GetComponent<RectTransform>();
    }

    void FixedUpdate()
    {
        pos = tc.PositionChange(rt, canvas);

        if (!isCreate) { count = 0; }
        if (isCreate)
        {
            count++;
            if (count == 1)
            {
                tmp = true;
            }
        }

        if (tmp)
        {
            tmp = false;
            StartCoroutine(Create());
        }
    }

    IEnumerator Create()
    {
        float d = 0;
        for (int i = 0; i < bulletNum; i++)
        {
            if (way % 2 == 1)
            {
                for (int j = 0 - ((int)way / 2); j <= (way - ((int)way / 2) - 1); j++)
                {
                    GameObject obj = Instantiate(prefabs, pos + Quaternion.Euler(0, 0, angle - 90) * new Vector3(j * distance, d, 0), Quaternion.identity);
                    obj.GetComponent<NormalBullet>().angle = angle;
                }
            }
            else if (way % 2 == 0)
            {
                for (int j = 0 - ((int)way / 2); j <= (way - ((int)way / 2)); j++)
                {
                    if (j < 0)
                    {
                        GameObject obj = Instantiate(prefabs, (pos + Quaternion.Euler(0, 0, angle - 90) * new Vector3((j + 0.5f) * distance, d, 0)), Quaternion.identity);
                        obj.GetComponent<NormalBullet>().angle = angle;
                    }
                    else if (j >= 0)
                    {
                        GameObject obj = Instantiate(prefabs, (pos + Quaternion.Euler(0, 0, angle - 90) * new Vector3((j - 0.5f) * distance, d, 0)), Quaternion.identity);
                        obj.GetComponent<NormalBullet>().angle = angle;
                    }
                }
            }

            if(!isAll)
            {
                yield return new WaitForSeconds(coolTime);
            }
            else { d += dis; }
        }
    }
}
