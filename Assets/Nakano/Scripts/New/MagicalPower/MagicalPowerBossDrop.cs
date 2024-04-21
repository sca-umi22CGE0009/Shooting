using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicalPowerBossDrop : MonoBehaviour
{
    [SerializeField, Header("ステージ")]
    GameObject Stage1;

    [SerializeField]
    RectTransform rectTransform;

    [SerializeField]
    GameObject magicalPowerPrefab;

    [SerializeField] BossController bossController;

    int count = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //プレイヤーの弾に当たったら
        if (collision.gameObject.CompareTag("SingleAttack")|| collision.gameObject.CompareTag("LightAttack")|| collision.gameObject.CompareTag("HeavyAttack"))
        {
            count++;

            if(count > 5)
            {
                count = 0;

                GameObject obj = Instantiate(magicalPowerPrefab);
                obj.transform.SetParent(Stage1.transform);
                RectTransform rect = obj.GetComponent<RectTransform>();
                rect.anchoredPosition3D = new Vector3
                    (Random.Range(rectTransform.anchoredPosition3D.x - 50,rectTransform.anchoredPosition3D.x + 50),
                     Random.Range(rectTransform.anchoredPosition3D.y - 50,rectTransform.anchoredPosition3D.y + 50), 0f);
                rect.localScale = new Vector3(1f, 1f, 1f);
            }
        }
    }
}
