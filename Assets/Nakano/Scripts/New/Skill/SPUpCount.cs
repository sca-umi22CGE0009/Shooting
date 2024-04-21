using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPUpCount : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("MagicalPower"))
        {
            UIManager.skillGaugePoint += collision.GetComponent<MagicalPower_Absroption>().GetSkillPoint();
            Destroy(collision.gameObject);
        }

    }
}
