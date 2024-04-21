using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicalPower : MonoBehaviour
{
    private GameObject player;
    private Image image;
    private bool isPower1;
    private float distance;
    private Vector3 playerHight=new Vector3(0,150.0f,0);
    private const float vacuumR = 200.0f;
    private const float speed = 600.0f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        image = GetComponent<Image>();
        if (image.sprite.name=="マジカルパワー_1") isPower1 = true;
        else isPower1 = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(player!=null) distance = Mathf.Abs(Vector3.Magnitude(transform.position - player.transform.position-playerHight));
        if (distance <= vacuumR)
        {
            transform.Translate(Vector3.Normalize(player.transform.position - (transform.position-playerHight)) * speed * Time.deltaTime);
        }
        if (distance < 30)
        {
            if (isPower1) UIManager.skillGaugePoint += 10;
            else UIManager.skillGaugePoint += 20;
                Destroy(gameObject);
        }
    }
}
