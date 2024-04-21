using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHpController : MonoBehaviour
{
    [SerializeField] BossController bossController;

    string level;
    string selectChara;

    int damage;

    PlayerParam selectLevelParam;

    [SerializeField] PlayerParams playerParam;

    private void Start()
    {
        if(Difficultylevel.difficulty == null) { level = "Normal"; }
        else level = Difficultylevel.difficulty;

        //charaÇéÊìæ
        selectChara = "CharaA";

        foreach (var p in playerParam.character)
        {
            if(p.charaName == selectChara)
            {
                switch (level)
                {
                    case "Easy":
                        selectLevelParam = p.easy;
                        break;
                    case "Normal":
                        selectLevelParam = p.normal;
                        break;
                    case "Hard":
                        selectLevelParam = p.hard;
                        break;
                    case "Galaxy":
                        selectLevelParam = p.galaxy;
                        break;
                }
            }
        }
    }

    int Atk(string attackType)
    {
        int atk = 0;

        switch(attackType)
        {
            case "SingleAttack":
                atk = selectLevelParam.singleAttack;
                break;
            case "LightAttack":
                atk = selectLevelParam.lightAttack;
                break;
            case "HeavyAttack":
                atk = selectLevelParam.heavyAttack;
                break;
        }

        return atk;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!bossController.Invincible) //ñ≥ìGèÛë‘Ç≈Ç»Ç¢Ç∆Ç´
        {
            //ÉvÉåÉCÉÑÅ[ÇÃíeÇ…ìñÇΩÇ¡ÇΩÇÁ
            if (collision.gameObject.CompareTag("SingleAttack"))
            {
                damage = Atk("SingleAttack");
                bossController.BossHp -= damage;
                Destroy(collision.gameObject);
            }

            if (collision.gameObject.CompareTag("LightAttack"))
            {
                damage = Atk("LightAttack");
                bossController.BossHp -= damage;
                Destroy(collision.gameObject);
            }

            if (collision.gameObject.CompareTag("HeavyAttack"))
            {
                damage = Atk("HeavyAttack");
                bossController.BossHp -= damage;
                Destroy(collision.gameObject);
            }
        }
    }
}