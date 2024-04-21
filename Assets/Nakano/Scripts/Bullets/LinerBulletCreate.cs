using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinerBulletCreate : MonoBehaviour
{
    [SerializeField, Header("NormalBullet")] GameObject prefabs;
    [SerializeField, Header("��������")] float createTime;
    //[SerializeField, Header("������")] int bulletNum;
    [SerializeField, Header("�N�[���^�C��")] float coolTime;
    [SerializeField, Header("������"), Tooltip("�������e�𐶐�����ꍇ�ɕ��������w�� ���͂�1�ȏ�")] int way = 1;
    [SerializeField, Header("�p�x")] float angle;
    [SerializeField, Header("�e��")] float speed;

    GameObject player;
    Vector3 playerPos;
    Vector3 direction;

    NormalBullet normalBullet;

    public bool isCreate = false;
    bool tmp = false;
    int count = 0;

    float t = 0;
    bool isCount = false;

    Canvas canvas;
    RectTransform rt;
    Vector3 pos;
    TransformChange tc;

    void Awake()
    {
        tc = gameObject.AddComponent<TransformChange>();
        player = GameObject.FindWithTag("Player");
        playerPos = player.transform.position;

        normalBullet = prefabs.GetComponent<NormalBullet>();
        normalBullet.speed = speed;

        direction = (playerPos - transform.position).normalized;

        canvas = GameObject.FindWithTag("Canvas").GetComponent<Canvas>();
        rt = GetComponent<RectTransform>();
    }

    void Update()
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
            isCount = true;
            t = 0;
            StartCoroutine(Create());
        }

        playerPos = player.transform.position;
        direction = (playerPos - transform.position).normalized;

        if (isCount)
        {
            t += Time.deltaTime;
        }
    }

    IEnumerator Create()
    {
        while (t < createTime)
        {
            for (int j = 0; j < way; j++)
            {
                GameObject obj = Instantiate(prefabs, pos, Quaternion.identity);

                float a = angle;
                if(way % 2 == 0)
                {
                    a = angle + ((angle / 2) * (j - 1));
                }
                if(way % 2 == 1)
                {
                    a = angle * (j - 1);
                }

                obj.GetComponent<NormalBullet>().angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + a;
            }
            yield return new WaitForSeconds(coolTime);
        }
        isCount = false;
    }
}
