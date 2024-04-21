using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSample : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] PlayerDamage playerDamage;

    void Start()
    {
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy1" || other.gameObject.tag == "Enemy2" || other.gameObject.tag == "Boss")
        {
            int damage = playerDamage.Damage(other.gameObject.tag);
            Debug.Log(damage);
        }
    }
}
