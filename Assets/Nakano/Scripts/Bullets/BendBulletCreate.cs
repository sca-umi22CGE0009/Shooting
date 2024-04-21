using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BendBulletCreate : MonoBehaviour
{
    [SerializeField, Header("BendBullet")] GameObject prefabs;
    [SerializeField, Header("��������")] float createTime;

    [SerializeField, Header("�������ŏ����������ꍇ��true�ɂ���")] bool isNum;
    [SerializeField, Header("������")] int bulletNum;

    [SerializeField, Header("�N�[���^�C��")] float coolTime;
    [SerializeField, Header("�e���@�y���Ӂz���ƈقȂ�l�������قǒᑬ��")] float speed;

    [SerializeField, Header("�p�x"), Tooltip("BendBullets�Ŏw�肵���p�x���D�悳���")] float angle = 0;
    [SerializeField, Header("������"), Tooltip("�������e�𐶐�����ꍇ�ɕ��������w�� �p�x�͐ݒ肵�Ă����Ӗ��ɂȂ� ���͂�1�ȏ�")] int way = 1;
    [SerializeField, Header("�p�x����"), Tooltip("�������e�̊p�x�𒲐�����")] float adjustmentAngle = 0;

    [SerializeField, Header("�p�x����"), Tooltip("�x�W�F�Ȑ��̍����𒲐�")] Vector2 relayAjust = new Vector2(100, 300);
    [SerializeField, Header("���B�n�_����"), Tooltip("�x�W�F�Ȑ��̍ŏI�ʒu�𒲐�")] Vector2 targetAjust = new Vector2(200, -900);

    BendBullet bendBullet;

    public bool isCreate = false;
    [Header("�t��]�ɂ��邩�ǂ���")] public bool isReverse = false;

    float t = 0;
    bool isCount = false;
    bool tmp = false;
    int count = 0;

    Canvas canvas;
    RectTransform rt;
    Vector3 pos;
    TransformChange tc;

    private void Awake()
    {
        tc = gameObject.AddComponent<TransformChange>();
        bendBullet = prefabs.GetComponent<BendBullet>();
        bendBullet.speed = speed;

        if (!isReverse)
        {
            bendBullet.relayAjust = relayAjust;
            bendBullet.targetAjust = targetAjust;
        }
        if(isReverse)
        {
            bendBullet.relayAjust = new Vector2(relayAjust.x * -1, relayAjust.y);
            bendBullet.targetAjust = new Vector2(targetAjust.x * -1, targetAjust.y);
        }

        if (way < 1) { way = 1; } //�w�肳�ꂽway��1�����̂Ƃ��A1�ɂ���

        //�������e�̂Ƃ��A�e���m�̊Ԃ̊p�x���Z�o
        if (way > 1) { angle = 360 / way; }
        else { adjustmentAngle = 0; }

        canvas = GameObject.FindWithTag("Canvas").GetComponent<Canvas>();
        rt = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (isReverse)
        {
            bendBullet.relayAjust = new Vector2(relayAjust.x * -1, relayAjust.y);
            bendBullet.targetAjust = new Vector2(targetAjust.x * -1, targetAjust.y);
            isReverse = false;
        }
        else
        {
            bendBullet.relayAjust = new Vector2(relayAjust.x, relayAjust.y);
            bendBullet.targetAjust = new Vector2(targetAjust.x, targetAjust.y);
        }

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

        if (isCount)
        {
            t += Time.deltaTime;
        }
    }

    IEnumerator Create()
    {
        if(!isNum)
        {
            while (t < createTime)
            {
                for (int j = 1; j <= way; j++)
                {
                    GameObject obj = Instantiate(prefabs, pos, Quaternion.identity);
                    obj.GetComponent<BendBullet>().angle = angle * j + adjustmentAngle;
                }
                yield return new WaitForSeconds(coolTime);
            }
        }
        
        if(isNum)
        {
            for(int i = 0; i < bulletNum; i++)
            {
                for (int j = 1; j <= way; j++)
                {
                    GameObject obj = Instantiate(prefabs, pos, Quaternion.identity);
                    obj.GetComponent<BendBullet>().angle = angle * j + adjustmentAngle;
                }
                yield return new WaitForSeconds(coolTime);
            }
        }

        isCount = false;
    }
}
