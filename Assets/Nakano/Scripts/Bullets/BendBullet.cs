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

    [Header("発射方向")] public float angle;
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
        //弾の進行具合（Lerpの第三引数に入れる）
        time += Time.deltaTime;
        //二次ベジェ曲線を使う
        //スタートから中継地点をつなぐベクトル上を走る点の位置
        var firstVec = Vector2.Lerp(startPos, relayPos, time / speed);
        //中継地点からターゲットまでをつなぐベクトル上を走る点の位置
        var SecondVec = Vector2.Lerp(relayPos, targetPos, time / speed);
        //上の二点をつなぐベクトル上を走る点（弾）の位置
        vec = Vector2.Lerp(firstVec, SecondVec, time / speed);
        //弾の位置を代入する
        this.transform.position = new Vector3(vec.x, vec.y, 90);

        if(Vector3.Distance(this.transform.position, new Vector3(targetAjust.x, targetAjust.y, 0)) <= 20)
        {
            Destroy(this);
        }
    }
}
