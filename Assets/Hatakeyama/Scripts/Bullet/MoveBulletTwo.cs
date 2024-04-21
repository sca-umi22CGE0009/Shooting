using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBulletTwo : MonoBehaviour
{
    [SerializeField]private float rotSpeed;
    private Vector3 pos;
    private Vector3 rot;
    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
        StartCoroutine(BulletBreak());
    }

    // Update is called once per frame
    void Update()
    {
        pos.y+=BulletManager.bs*Time.deltaTime;
        transform.position= pos;
        rot.z+=rotSpeed*Time.deltaTime;
        transform.rotation=Quaternion.Euler(rot);
    }
    IEnumerator BulletBreak()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
