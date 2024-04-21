using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ©‹@‚ÉŒü‚©‚Á‚Ä”­Ë‚³‚ê‚é’e‚ğ¶¬
/// </summary>
public class AimBulletCreate : MonoBehaviour
{
    [SerializeField, Header("AimBullet")] GameObject prefabs;
    [SerializeField, Header("¶¬”")] int bulletNum;
    [SerializeField, Header("ƒN[ƒ‹ƒ^ƒCƒ€")] float coolTime;
    [SerializeField, Header("’e‘¬")] float speed;

    [SerializeField, Header("c—ñ”­Ë‚·‚é‚©")] bool isColumn;
    [SerializeField, Header("c—ñ”"), Tooltip("‰¡‚É‰½ŒÂ•À‚Ô‚©(c—ñ‚ğ‰½ŒÂì‚é‚©) “ü—Í‚Í1ˆÈã")] int way = 1;
    [SerializeField, Header("c—ñ“¯m‚Ì‹——£")] float distance;

    [SerializeField, Header("‘S’e“¯¶¬")] bool isAll = false;
    [SerializeField, Header("‘S’e“¯¶¬‚Ì’eŠÔ‚Ì‹——£ ƒN[ƒ‹ƒ^ƒCƒ€‚Ì‘ã‚í‚è")] float dis;

    AimBullet aimBullet;

    public bool isCreate = false;
    bool tmp = false;
    int count = 0;

    Canvas canvas;
    RectTransform rt;
    Vector3 pos;
    TransformChange tc;

    void Awake()
    {
        tc = gameObject.AddComponent<TransformChange>();
        aimBullet = prefabs.GetComponent<AimBullet>();
        aimBullet.speed = speed;

        canvas = GameObject.FindWithTag("Canvas").GetComponent<Canvas>();
        rt = GetComponent<RectTransform>();

        if (way < 1) { way = 1; } //w’è‚³‚ê‚½way‚ª1–¢–‚Ì‚Æ‚«A1‚É‚·‚é
    }

    void Update()
    {
        pos = tc.PositionChange(rt, canvas);

        if(!isCreate) { count = 0;}
        if (isCreate)
        {
            count++;
            if(count == 1)
            {
                tmp = true;
            }
        }

        if(tmp)
        {
            tmp = false;
            StartCoroutine(Create());
        }
    }

    IEnumerator Create()
    {
        if (!isColumn)
        {
            for (int i = 0; i < bulletNum; i++)
            {
                Instantiate(prefabs, pos, Quaternion.identity);
                yield return new WaitForSeconds(coolTime);
            }
        }
        else if (isColumn)
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
