using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBulletThree : MonoBehaviour
{
    [SerializeField] private float expansionSpeed;
    private Vector3 scale;
    // Start is called before the first frame update
    void Start()
    {
        scale = new Vector3(1,1,1);
        StartCoroutine(BulletBreak());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, BulletManager.bs, 0) * Time.deltaTime);
        scale += new Vector3(expansionSpeed, expansionSpeed)*Time.deltaTime;
        transform.localScale = scale;
    }

    IEnumerator BulletBreak()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
