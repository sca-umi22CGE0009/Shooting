using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BendBulletCreate : MonoBehaviour
{
    [SerializeField, Header("BendBullet")] GameObject prefabs;
    [SerializeField, Header("¶¬ŠÔ")] float createTime;

    [SerializeField, Header("¶¬”‚Åˆ—‚µ‚½‚¢ê‡‚Ítrue‚É‚·‚é")] bool isNum;
    [SerializeField, Header("¶¬”")] int bulletNum;

    [SerializeField, Header("ƒN[ƒ‹ƒ^ƒCƒ€")] float coolTime;
    [SerializeField, Header("’e‘¬@y’ˆÓz‘¼‚ÆˆÙ‚È‚è’l‚ª‚‚¢‚Ù‚Ç’á‘¬‰»")] float speed;

    [SerializeField, Header("Šp“x"), Tooltip("BendBullets‚Åw’è‚µ‚½Šp“x‚æ‚è—Dæ‚³‚ê‚é")] float angle = 0;
    [SerializeField, Header("•ûŒü”"), Tooltip("‘½•ûŒü’e‚ğ¶¬‚·‚éê‡‚É•ûŒü”‚ğw’è Šp“x‚Íİ’è‚µ‚Ä‚à–³ˆÓ–¡‚É‚È‚é “ü—Í‚Í1ˆÈã")] int way = 1;
    [SerializeField, Header("Šp“x’²®"), Tooltip("‘½•ûŒü’e‚ÌŠp“x‚ğ’²®‚·‚é")] float adjustmentAngle = 0;

    [SerializeField, Header("Šp“x’²®"), Tooltip("ƒxƒWƒF‹Èü‚Ì‚‚³‚ğ’²®")] Vector2 relayAjust = new Vector2(100, 300);
    [SerializeField, Header("“’B’n“_’²®"), Tooltip("ƒxƒWƒF‹Èü‚ÌÅIˆÊ’u‚ğ’²®")] Vector2 targetAjust = new Vector2(200, -900);

    BendBullet bendBullet;

    public bool isCreate = false;
    [Header("‹t‰ñ“]‚É‚·‚é‚©‚Ç‚¤‚©")] public bool isReverse = false;

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

        if (way < 1) { way = 1; } //w’è‚³‚ê‚½way‚ª1–¢–‚Ì‚Æ‚«A1‚É‚·‚é

        //‘½•ûŒü’e‚Ì‚Æ‚«A’e“¯m‚ÌŠÔ‚ÌŠp“x‚ğZo
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
