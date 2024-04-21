using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    MainGameController mainGameController;
    BossController bossController;
    PlayerController playerController;

    [SerializeField] EnemyParams enemyParams;

    [SerializeField]
    GameObject MysticField;

    int stageNum;
    string levelString;
    int bossWaveNum;

    int damageCount = 0;

    Enemy enemy;
    Boss boss;

    void Awake()
    {
        mainGameController = GameObject.FindObjectOfType<MainGameController>();
        bossController = GameObject.FindObjectOfType<BossController>();
        playerController = GameObject.FindObjectOfType<PlayerController>();
        stageNum = mainGameController.StageNumber;
        bossWaveNum = bossController.BossWaveNum;

        if (Difficultylevel.difficulty == null) { levelString = "Normal"; }
        else levelString = Difficultylevel.difficulty;

        foreach (var e in enemyParams.stage)
        {
            if (e.stageNum == stageNum)
            {
                switch (levelString)
                {
                    case "Easy":
                        enemy = e.easy;
                        boss = e.boss.easyBoss;
                        break;
                    case "Normal":
                        enemy = e.normal;
                        boss = e.boss.normalBoss;
                        break;
                    case "Hard":
                        enemy = e.hard;
                        boss = e.boss.hardBoss;
                        break;
                    case "Galaxy":
                        enemy = e.galaxy;
                        boss = e.boss.galaxyBoss;
                        break;
                }
            }
        }
    }

    private void Update()
    {
        bossWaveNum = bossController.BossWaveNum;
    }

    int EnemyAtk(string EnemyID)
    {
        int atk = 0;

        foreach (var v in enemy.enemy)
        {
            if (v.enemyID == EnemyID) { atk = v.atk; }
        }

        return atk;
    }

    int BossAtk(int BossWaveNum)
    {
        int atk = 0;

        switch (BossWaveNum)
        {
            case 1:
                atk = boss.wave1.atk;
                break;
            case 2:
                atk = boss.wave2.atk;
                break;
            case 3:
                atk = boss.wave3.atk;
                break;
            case 4:
                atk = boss.wave4.atk;
                break;
        }

        return atk;
    }

    /// <summary>
    /// 引数に当たったオブジェクトのタグを入れる
    /// 例 => Damage(collision.gameObject.tag)
    /// </summary>
    /// <param name="objTag"></param>
    /// <returns></returns>
    public int Damage(string objTag)
    {
        int damage = 0;

        if(objTag == "Enemy1" || objTag == "Enemy2")
        {
            damage = EnemyAtk(objTag);
        }
        else if(objTag == "Boss")
        {
            damage = BossAtk(bossWaveNum);
        }
        else
        {
            Debug.Log("想定外の値が入っています。");
            damage = 0;
        }

        return damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!PlayerController.damageFlag)
        {
            if(!UIManager.isMysticField)
            {
                if (collision.gameObject.tag == "Enemy1" || collision.gameObject.tag == "Enemy2" || collision.gameObject.tag == "Boss")
                {
                    int damage = Damage(collision.gameObject.tag);
                    UIManager.HP -= damage;
                    playerController.IsDamage = true;
                }
            }
            else
            {
                MysticField.SetActive(true);
                damageCount++;

                if(damageCount >= 10)
                {
                    UIManager.isMysticField = false;
                    MysticField.SetActive(false);
                    damageCount = 0;
                }
            }
        }
    }
}
