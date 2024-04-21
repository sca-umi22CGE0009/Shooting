using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Player��_���Ĕ��ˁ@�ǔ��͂��Ȃ�
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
        //�������Ɏ��g�̈ʒu�ƃv���C���[�̈ʒu���擾����
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
