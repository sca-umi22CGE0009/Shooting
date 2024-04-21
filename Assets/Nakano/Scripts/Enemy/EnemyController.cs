using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] ParticleSystem deathEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("SingleAttack") || collision.CompareTag("LightAttack") || collision.CompareTag("HeavyAttack"))
        {
            Instantiate(deathEffect, this.gameObject.transform.position, Quaternion.identity);
            GlobalVariables.Score++;
            Destroy(this.gameObject);
        }
    }
}