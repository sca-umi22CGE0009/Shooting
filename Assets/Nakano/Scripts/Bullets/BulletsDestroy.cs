using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��ʊO�ɏo���e���폜����X�N���v�g�ł�
/// </summary>
public class BulletsDestroy : MonoBehaviour
{
    public bool isGather = false; //�G�̈ʒu�ɒe���W�߂邩
    public Vector3 enemyPos = new Vector3(0, 0, 0); //�G�̈ʒu
    public float dis = 0; //�G�Ƃ̋���


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
