using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBulletOne : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(BulletBreak());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0,BulletManager.bs,0)*Time.deltaTime);
    }
    IEnumerator BulletBreak()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
