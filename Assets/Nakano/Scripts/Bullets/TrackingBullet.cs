using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ž©‹@‚ð’Ç”ö‚·‚é
/// </summary>
public class TrackingBullet : MonoBehaviour
{
    GameObject player;
    Vector3 playerPos;
    Vector3 bulletPos;
    Vector3 direction;

    [Header("’Ç”öŽžŠÔ")] public float trackingTime;
    float time;

    [HideInInspector] public float speed;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        time = 0;
    }

    void Update()
    {
        playerPos = player.transform.position;
        bulletPos = this.transform.position;

        time += Time.deltaTime;
        if(time <= trackingTime)
        {
            direction = (playerPos - bulletPos).normalized;
        }

        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Mirror" && time > trackingTime)
        {
            direction = Vector3.Reflect(direction, Vector3.right);
        }
    }
}
