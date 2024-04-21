using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherBulletCreate : MonoBehaviour
{
    [SerializeField, Header("NormalBullet")] GameObject prefabs;
    [SerializeField, Header("ˆê•Ó‚Ì¶¬”")] int bulletNum;
    [SerializeField, Header("¶¬ŠÔ")] float createTime;
    //[SerializeField, Header("¶¬‰ñ”")] int createNum;
    [SerializeField, Header("ƒN[ƒ‹ƒ^ƒCƒ€"), Tooltip("’Z‚¢‚Ù‚Ç–§“x‚ªã‚ª‚é")] float coolTime;
    [SerializeField, Header("’e‘¬")] float speed;

    [SerializeField, Header("“G‚Æ‚Ì‹——£‚ªdisˆÈ‰º‚Ì‚Æ‚«’e‚ğíœ")] float dis;

    NormalBullet normalBullet;
    BulletsDestroy bulletsDestroy;

    Vector3 direction;
    float range;

    public bool isCreate = false;

    float t = 0;
    bool isCount = false;
    bool tmp = false;
    int count = 0;

    Canvas canvas;
    RectTransform rt;
    Vector3 pos;
    TransformChange tc;

    void Awake()
    {
        tc = gameObject.AddComponent<TransformChange>();
        normalBullet = prefabs.GetComponent<NormalBullet>();
        normalBullet.speed = speed;
        normalBullet.isReflect = true;

        bulletsDestroy = prefabs.GetComponent<BulletsDestroy>();
        bulletsDestroy.isGather = true;
        bulletsDestroy.enemyPos = new Vector3(0, 38, 90);
        bulletsDestroy.dis = dis;

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

        if (isCount)
        {
            t += Time.deltaTime;
        }
    }

    IEnumerator Create()
    {
        while (t < createTime)
        {
            for (int j = 0; j < 4; j++)
            {
                for (int i = 0; i < bulletNum; i++)
                {
                    Vector3 createPos = new Vector3(0, 0, 90);
                    switch (j)
                    {
                        case 0:
                            createPos.x = Random.Range(-110, 110);
                            createPos.y = Random.Range(70, 80);
                            break;
                        case 1:
                            createPos.x = Random.Range(-110, 110);
                            createPos.y = Random.Range(-80, -70);
                            break;
                        case 2:
                            createPos.x = Random.Range(-110, -100);
                            createPos.y = Random.Range(-80, 80);
                            break;
                        case 3:
                            createPos.x = Random.Range(100, 110);
                            createPos.y = Random.Range(-80, 80);
                            break;
                        default:
                            break;
                    }

                    GameObject obj = Instantiate(prefabs, createPos, Quaternion.identity);
                    direction = (pos - obj.transform.position).normalized;
                    obj.GetComponent<NormalBullet>().angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                }
            }
            yield return new WaitForSeconds(coolTime);
        }
        isCount = false;
    }
}