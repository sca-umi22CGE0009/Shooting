using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Result : MonoBehaviour
{
    [SerializeField, Header("生存タイム")] private Text timeText;

    [SerializeField, Header("生存タイムのpos")] private RectTransform rectTimeText;
    //TimeMoveText
    [SerializeField] private float xRectPos = 3f;
    [SerializeField] private float publicTextSpeed = 0.5f;
    float textSpeed;
    Vector2 rPos;
    bool resetPos = false;

    [SerializeField, Header("タイムボーナス")] private Text TimeBonusText;
    [SerializeField,Header("スコア")] private Text scoreText;
    private int score;
    [SerializeField, Header("HP")] private Text hpText;
    private int hp;
    [SerializeField, Header("トータススコア")] private Text TotalScoreText;

    private float bonus;
    private float totalScore;
    float hours;
    float minutes;
    float seconds;
    //float comma = 0f;
    float t;

    //MoveText
    [SerializeField, Header("テキストオブジェクト表示の順番")] private GameObject[] totalObjects;
    [SerializeField, Header("表示時間")] private float time = 1.0f;
    float resetTime;

    int pushNum = 0;
    int textNum = 0;
    bool pushNumCheck = false;

    //BadgesImage
    [SerializeField, Header("バッジ表示")] private GameObject[] imaBadges;

    void Start()
    {
        t = 0.0f;
        resetTime = time;
        textSpeed = publicTextSpeed;
        rPos = new Vector2(rectTimeText.anchoredPosition.x,rectTimeText.anchoredPosition.y);
    }

    private void Update()
    {
        scoreText.text = "" + GlobalVariables.Score;
        hpText.text = "" + UIManager.HP;
        TotalScore();
        BadgesImage();

        if (UIManager.HP >= 0)
        {
            BonusScore();
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            pushNum++;
            pushNumCheck = true;
        }

        totalObjects[0].gameObject.SetActive(true);
        if (t <= GlobalVariables.AliveTime)
        {
            TimeMoveText();
            Timer();
            t++;
            if (pushNum == 1)
            {
                t = GlobalVariables.AliveTime;
                Timer();
            }
            //表示
            //timeText.text = ($"{minutes.ToString("00")}:{seconds.ToString("00")}:{comma.ToString("00")}");
            timeText.text = ($"{hours.ToString("00")}:{minutes.ToString("00")}:{seconds.ToString("00")}");
        }
        if (t >= GlobalVariables.AliveTime)
        {
            MoveText();
            if (!pushNumCheck)
            {
                pushNum = 1;
            }

            if (pushNum >= 2)
            {
                for (int i = 0; i < totalObjects.Length; i++)
                {
                    totalObjects[i].SetActive(true);
                }
            }
            resetPos = true;
        }
    }
    //切り捨て
    void Timer()
    {
        //comma = GlobalVariables.AliveTime;
        //comma = (comma - Mathf.Floor(comma)) * 100;
        //comma = Mathf.Floor(comma);
        seconds = t;
        seconds = Mathf.Floor(seconds);

        minutes = seconds / 60;
        minutes = Mathf.Floor(minutes);

        hours = minutes / 60;
        hours = Mathf.Floor(hours);

        seconds -= minutes * 60;
        minutes -= hours * 60;
    }
    //ボーナススコア計算
    void BonusScore()
    {
        float s;
        s = GlobalVariables.AliveTime;
        s = Mathf.Floor(s);

        float m;
        m = s / 60;
        m = Mathf.Floor(m);

        float b =m;

        if (b >= 30)
        {
            bonus = 1.0f;
        }
        if (b < 30 && b >= 20)
        {
            bonus = 1.1f;
        }
        if (b < 20 && b >= 15)
        {
            bonus = 1.2f;
        }
        if (b < 15 && b >= 10)
        {
            bonus = 1.3f;
        }
        if (b < 10 && b >= 5)
        {
            bonus = 1.4f;
        }
        if (b < 5 && b >= 0)
        {
            bonus = 1.5f;
        }
        TimeBonusText.text = "×" + bonus.ToString("f1");
    }
    //トータルスコア計算
    void TotalScore()
    {
        float s;
        s = GlobalVariables.AliveTime;
        s = Mathf.Floor(s);

        int clearScore = 360000;
        //totalScore = ((minutes * 60 + seconds) + comma / 100)* 100 * bonus + GlobalVariables.Score + UIManager.HP;
        if (UIManager.HP <= 0)
        {
            bonus = 1.0f;
        }
        totalScore = (clearScore - (s * 5)) * bonus + (GlobalVariables.Score*1000 + UIManager.HP * 50);

        if (totalScore < 0)
        {
            totalScore = 0;
        }
        TotalScoreText.text = "" + (int)totalScore + " pt";
    }
    //タイム横揺れのアニメーション(仮案-> 座標を範囲内でランダム表示)
    void TimeMoveText()
    {
        Vector2 pos = new Vector2(rPos.x, 0) * (-textSpeed / 2) * Time.deltaTime;
        rectTimeText.Translate(pos);

        if (rectTimeText.anchoredPosition.x <= rPos.x - xRectPos)
        {
            textSpeed = publicTextSpeed;
        }
        if (rectTimeText.anchoredPosition.x >= rPos.x + xRectPos)
        {
            textSpeed = -publicTextSpeed;
        }

        if (resetPos)
        {
            rectTimeText.anchoredPosition = rPos;
        }
    }
    //テキストを順番に表示
    void MoveText()
    {
        resetTime -= Time.deltaTime;

        if (resetTime <= 0)
        {
            if (textNum != totalObjects.Length - 1)
            {
                textNum++;
                totalObjects[textNum].SetActive(true);
                resetTime = time;
            }
            else
            {
                textNum = totalObjects.Length - 1;
            }
        }
    }
    //総合評価
    void BadgesImage()
    {
        if (totalScore < 200000)
        {
            imaBadges[0].gameObject.SetActive(true);
        }
        else if (totalScore < 300000 && totalScore >= 200000)
        {
            imaBadges[1].gameObject.SetActive(true);
        }
        else if (totalScore < 500000 && totalScore >= 300000)
        {
            imaBadges[2].gameObject.SetActive(true);
        }
        else if (totalScore < 750000 && totalScore >= 500000)
        {
            imaBadges[3].gameObject.SetActive(true);
        }
        else if (totalScore < 1000000 && totalScore >= 750000)
        {
            imaBadges[4].gameObject.SetActive(true);
        }
        else if (totalScore >= 1000000)
        {
            imaBadges[5].gameObject.SetActive(true);
        }
    }
}
