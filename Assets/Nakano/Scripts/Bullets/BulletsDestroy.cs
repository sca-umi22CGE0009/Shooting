using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 画面外に出た弾を削除するスクリプトです
/// </summary>
public class BulletsDestroy : MonoBehaviour
{
    public bool isGather = false; //敵の位置に弾を集めるか
    public Vector3 enemyPos = new Vector3(0, 0, 0); //敵の位置
    public float dis = 0; //敵との距離


    void Update()
    {
        if(UIManager.isPentagram)
        {
            Destroy(gameObject);
        }

        var pos = this.transform.position;
        if (pos.x < -150 || pos.x > 150 || pos.y < -150 || pos.y > 150)
        {
            Destroy(this.gameObject);
        }

        if (isGather && Vector3.Distance(this.transform.position, enemyPos) <= dis)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }
}
