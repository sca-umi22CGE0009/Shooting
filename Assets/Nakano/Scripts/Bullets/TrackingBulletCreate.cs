using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingBulletCreate : MonoBehaviour
{
    Canvas canvas;
    [SerializeField, Header("TrackingBullet")] GameObject prefabs;
    [SerializeField, Header("������")] int bulletNum;
    [SerializeField, Header("�N�[���^�C��")] float coolTime;
    [SerializeField, Header("�e��")] float speed;
    [SerializeField, Header("�ǔ�����")] float trackingTime;

    [SerializeField, Header("�c�񔭎˂��邩")] bool isColumn;
    [SerializeField, Header("�c��"), Tooltip("���ɉ����Ԃ�(�c�������邩) ���͂�1�ȏ�")] int way = 1;
    [SerializeField, Header("�c�񓯎m�̋���")] float distance;

    [SerializeField, Header("�S�e��������")] bool isAll = false;
    [SerializeField, Header("�S�e�����������̒e�Ԃ̋��� �N�[���^�C���̑���")] float dis;

    TrackingBullet trackingBullet;

    public bool isCreate = false;
    bool tmp = false;
    int count = 0;

    RectTransform rt;
    Vector3 pos;

    TransformChange tc;

    void Awake()
    {
        tc = gameObject.AddComponent<TransformChange>();
        canvas = GameObject.FindWithTag("Canvas").GetComponent<Canvas>();

        trackingBullet = prefabs.GetComponent<TrackingBullet>();
        trackingBullet.speed = speed;
        trackingBullet.trackingTime = trackingTime;

        rt = GetComponent<RectTransform>();

        if (way < 1) { way = 1; } //�w�肳�ꂽway��1�����̂Ƃ��A1�ɂ���
    }

    void Update()
    {
        pos = tc.PositionChange(rt, canvas);

        if (!isCreate) { count = 0; }
        if (isCreate)
        {
            isCreate = false;
            count++;
            if(count == 1)
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
        if(!isColumn)
        {
            for (int i = 0; i < bulletNum; i++)
            {
                Instantiate(prefabs, pos, Quaternion.identity);
                yield return new WaitForSeconds(coolTime);
            }
        }
        else if(isColumn)
        {
            float d = 0;
            for (int i = 0; i < bulletNum; i++)
            {
                if (way % 2 == 1)
                {
                    for (int j = 0 - ((int)way / 2); j <= (way - ((int)way / 2) - 1); j++)
                    {
                        Instantiate(prefabs, pos + new Vector3(j * distance, d, 0), Quaternion.identity);
                    }
                }
                else if (way % 2 == 0)
                {
                    for (int j = 0 - ((int)way / 2); j <= (way - ((int)way / 2)); j++)
                    {
                        if (j < 0)
                        {
                            Instantiate(prefabs, (pos + new Vector3((j + 0.5f) * distance, d, 0)), Quaternion.identity);
                        }
                        else if (j >= 0)
                        {
                            Instantiate(prefabs, (pos + new Vector3((j - 0.5f) * distance, d, 0)), Quaternion.identity);
                        }
                    }
                }

                if (!isAll)
                {
                    yield return new WaitForSeconds(coolTime);
                }
                else { d += dis; }
            }
        }
    }
}
