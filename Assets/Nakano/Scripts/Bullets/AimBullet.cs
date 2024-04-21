using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Playerを狙って発射　追尾はしない
/// </summary>
public class AimBullet : MonoBehaviour
{
    GameObject player;
    Vector3 playerPos;
    Vector3 bulletPos;

    Vector3 direction;

    [HideInInspector] public float speed;

    void Start()
    {
        //生成時に自身の位置とプレイヤーの位置を取得する
        player = GameObject.FindWithTag("Player");
        playerPos = player.transform.position;
        bulletPos = this.transform.position;

        direction = (playerPos - bulletPos).normalized;
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Mirror")
        {
            direction = Vector3.Reflect(direction, Vector3.right);
        }
    }
}
