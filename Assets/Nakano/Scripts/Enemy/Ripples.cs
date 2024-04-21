using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ripples : MonoBehaviour
{
    [SerializeField] ParticleSystem ripplesEffect;
    [SerializeField] Vector3 createPos;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Mirror")
        {
            var pos = this.transform.position + createPos;
            Instantiate(ripplesEffect, pos, Quaternion.identity);
        }
    }
}
