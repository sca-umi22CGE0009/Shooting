using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    [SerializeField, Header("移動時間")] float time;
    [SerializeField, Header("移動距離"), Tooltip("移動速度は移動距離/移動時間から算出")] float distance;
    [SerializeField, Header("y座標がここまで行ったら->")] float minY;
    [SerializeField, Header("->この座標に飛ぶ")] float maxY;

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
        //速度計算
        speed = distance / time;

        //移動量
        moveDis = 0;

        //目指す座標
        nextY = transform.position.y + distance;

        isMove = true;
    }
}
