using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using static TimeScoreCounter;

public class BossController : MonoBehaviour
{
    [SerializeField] MainGameController mainGameController;
    [SerializeField] EnemyParams enemyParams;

    [SerializeField] int stageNum;
    string level;

    [SerializeField] GameObject bossObj;

    [SerializeField] ParticleSystem explodeEffect;

    float defaultHp;

    [SerializeField,Header("BossHp")]
    float hp;
    
    float hpRatio = 1;

    [SerializeField, Header("HPバーの中身")] Image hpBar;
    [SerializeField, Header("BossのUI")] GameObject bossUI;
    [SerializeField, Header("BossのUI")] Animator bossUIAnim;
    [SerializeField, Header("HP減少時の減少完了までの時間"), Tooltip("(decreaseTime)秒でHPが減少し終わる")] float decreaseTime = 0.2f;

    [SerializeField] Sprite[] hpBars;

    public float BossHp
    {
        get { return hp; }
        set { hp = value; }
    }

    bool screenInitialize = false; //弾全消去フラグ

    [SerializeField] WaveController waveController;
    [SerializeField] PlayableDirector director;
    int currentTrackIndex = 1;

    bool wave2 = false, wave3 = false, wave4 = false, end = false;

    [SerializeField, Header("ボス登場からボス戦WAVE１開始までの時間")] float entryTime;

    [SerializeField, Header("初期位置に戻るスピード")] float moveSpeed;
    [SerializeField, Header("各WAVE初期位置")] GameObject[] pos;
    bool isPosIni = false;

    [SerializeField, Header("各WAVE移行するときの残HP割合"), Tooltip("Element0が0.75の場合、残HPが75％以下でWAVE2に移行する")] float[] hpLimit; 

    [SerializeField] GameObject[] wave;

    [SerializeField, Header("ボス タイムライン数")] int timelineNum = 5;

    //残HP以外のWAVE移行条件用 条件達成時にtrueにしてWave移行する
    bool toWave2 = false, toWave3 = false, toWave4 = false;

    //無敵状態
    bool isInvincible = true;
    public bool Invincible { get { return isInvincible; } }

    //Stage1用
    [SerializeField, Header("ボスの分身")] GameObject[] bossClone;
    [SerializeField, Header("鏡")] Animator mirror;
    float wave2Time = 0; //Wave2 時間計測
    int wave3Count = 0; //Wave3 攻撃回数計測
    public int BubbleCount { get { return wave3Count; } set { wave3Count= value; } }

    [SerializeField, Tooltip("ボスのHPが全体の(cloneDestroyHP)%減少したとき分身が一体消える")] float cloneDestroyHP;
    float wave4NowHP = 0;
    int wave4CloneDestroyCount = 0;

    //Stage2用
    int wave4AttackCount = 1;
    [SerializeField] Animator scarDirection;

    public int BossWaveNum { get { return currentTrackIndex - 1; } }

    public float BossHP
    {
        get { return hp; }
        set { hp = value; }
    }

    void Start()
    {
        if (Difficultylevel.difficulty == null) { level = "Normal"; }
        else level = Difficultylevel.difficulty;

        DefaultHpSet();

        hpBar.sprite = hpBars[0];
        hp = defaultHp;
        hpBar.fillAmount = 1;

        StartCoroutine(BossReach());

        wave[0].SetActive(true);
        wave[1].SetActive(false);
        wave[2].SetActive(false);
        wave[3].SetActive(false);

        BossCondition();
    }

    void Update()
    {
        HpDirection();
        ScreenInitialize();

        Stage1WaveChange();
        //Stage1のHP以外のWAVE移行条件判定用 他ステージでもHP以外のWAVE移行条件を加える場合、switch文で纏めるのが良いかも

        //Debug
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    if (!isInvincible) hp -= 200;
        //}

        //HPが一定以下になったら初期位置に戻る
        if (isPosIni)
        {
            if (currentTrackIndex > 1) wave[currentTrackIndex - 2].SetActive(false);

            director.Stop();
            TimelineAsset timelineAsset = director.playableAsset as TimelineAsset;
            director.ClearGenericBinding(timelineAsset.GetOutputTrack(currentTrackIndex));

            GameObject p;
            switch (currentTrackIndex)
            {
                case 1:
                    p = pos[0];
                    break;
                case 2:
                    p = pos[1];
                    break;
                case 3:
                    p = pos[2];
                    break;
                case 4:
                    p = pos[3];
                    break;
                default:
                    p = pos[0];
                    break;
            }

            if(Vector3.Distance(bossObj.transform.localPosition, p.transform.localPosition) > 10)
            {
                if (stageNum == 1 && currentTrackIndex == 1 && mirror != null)
                {
                    if (mirror.GetCurrentAnimatorStateInfo(0).IsName("Mirror_Defalut"))
                    {
                        mirror.SetTrigger("In");
                    }
                }
                if (stageNum == 1 && currentTrackIndex == 2 && mirror != null)
                {
                    if (mirror.GetCurrentAnimatorStateInfo(0).IsName("Mirror_In"))
                    {
                        mirror.SetTrigger("Out");
                    }
                }

                Vector3 dir = (p.transform.localPosition - bossObj.transform.localPosition).normalized;
                dir.z = 0;

                bossObj.transform.Translate(dir * moveSpeed * Time.deltaTime);
            }

            else 
            {
                bossObj.transform.localPosition = p.transform.localPosition;

                if(currentTrackIndex <= 4)
                {
                    mainGameController.WaveDirection(false, currentTrackIndex); //WAVE移行演出再生
                    StartCoroutine(WaveChangeDirection(currentTrackIndex));
                }

                isPosIni = false;
            }
        }
    }

    void DefaultHpSet()
    {
        foreach (var i in enemyParams.stage)
        {
            if (i.stageNum == stageNum)
            {
                switch (level)
                {
                    case "Easy":
                        defaultHp = i.boss.easyBoss.bossHp;
                        break;
                    case "Normal":
                        defaultHp = i.boss.normalBoss.bossHp;
                        break;
                    case "Hard":
                        defaultHp = i.boss.hardBoss.bossHp;
                        break;
                    case "Galaxy":
                        defaultHp = i.boss.galaxyBoss.bossHp;
                        break;
                }
            }
        }
    }

    //ボス戦移行
    IEnumerator BossReach()
    {
        isInvincible = true;

        //WAVEが全て終わったら
        yield return new WaitUntil(() => waveController.WaveCompleted);

        timeCountState = TimeCountState.STOP;

        yield return new WaitForSeconds(3f);

        //登場
        TimelineAsset timelineAsset = director.playableAsset as TimelineAsset;
        director.SetGenericBinding(timelineAsset.GetOutputTrack(currentTrackIndex), bossObj);
        director.Stop();
        director.Play();

        yield return new WaitForSeconds(1f);

        //ボスのUI登場
        bossUIAnim.SetBool("Entry", true);

        //ここらへんにストーリー追加？

        yield return new  WaitForSeconds(entryTime);

        yield return new WaitUntil(() => mainGameController.WaveDirectionEnd);

        //SetBossBinding();
        WaveChange();
    }

    //HP描画 HPに応じたWAVE移行
    void HpDirection()
    {
        //HP減少描画
        hpRatio = hp / defaultHp;
        if(hpBar.fillAmount > hpRatio) { hpBar.fillAmount -= (hpBar.fillAmount - hpRatio) / decreaseTime * Time.deltaTime; }
        else { hpBar.fillAmount = hpRatio; }

        //HPに応じて画像変更
        if(hpRatio >= 2.0f / 3.0f) { hpBar.sprite = hpBars[0]; }
        else if (hpRatio >= 1.0f / 3.0f && hpRatio < 2.0f / 3.0f) { hpBar.sprite = hpBars[1]; }
        else if(hpRatio < 1.0f / 3.0f) { hpBar.sprite = hpBars[2]; }

        //残りHpに応じてWAVE変更 wave2やwave3などのフラグはWaveChange()を一度だけ呼び出すためのもの
        if (hpRatio <= hpLimit[0] && !wave2)
        {
            hpRatio = hpLimit[0];
            isInvincible = true; //Wave移行条件を達成していない、かつHPが一定量まで下がっている場合はそれ以上HPの減少をしない
            if(toWave2)
            {
                wave2 = true;
                WaveChange();
            }
        }
        else if (hpRatio <= hpLimit[1] && !wave3) 
        {
            hpRatio = hpLimit[1];
            isInvincible = true;
            if (toWave3)
            {
                wave3 = true;
                WaveChange();
            }
        }
        else if (hpRatio <= hpLimit[2] && !wave4) 
        {
            hpRatio = hpLimit[2];
            isInvincible = true;
            if (toWave4)
            {
                wave4 = true;
                WaveChange();

                if (stageNum == 1) { wave4NowHP = hp; }
            }
        }
        else if (hp <= 0 && !end) 
        { 
            hp = 0;
            end = true;
            WaveChange();
            StartCoroutine(ClearDirection());
        }
    }

    //現在のWAVEが終了したら繰り返す Signalで呼び出し
    public void AnimationReplay()
    {
        if(currentTrackIndex <= timelineNum)
        {
            director.Stop();
            director.Play();
        }

        //Stage1 Boss Wave4 ボスの分身が移動完了したらそれ以降動かなくなる(弾は発射する)
        if(stageNum == 1 && currentTrackIndex == 5)
        {
            TimelineAsset timelineAsset = director.playableAsset as TimelineAsset;

            for (int i = 0; i < bossClone.Length; i++)
            {
                director.ClearGenericBinding(timelineAsset.GetOutputTrack(i + 6));
                director.SetGenericBinding(timelineAsset.GetOutputTrack(i + 6 + bossClone.Length), bossClone[i]);
            }

            director.Stop();
            director.Play();
        }
    }

    //WAVE移行
    void WaveChange()
    {
        isPosIni = true;
        screenInitialize = true;
        timeCountState = TimeCountState.STOP;
        isInvincible = true;
    }

    //画面から弾を全削除 
    void ScreenInitialize()
    {
        if (screenInitialize)
        {
            GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullets");
            foreach (var b in bullets)
            {
                Destroy(b);
            }

            GameObject[] bigBullets = GameObject.FindGameObjectsWithTag("BigBullets");
            foreach (var b in bigBullets)
            {
                Destroy(b);
            }
            screenInitialize = false;
        }
    }

    //Timeline編集
    void SetBossBinding()
    {
        TimelineAsset timelineAsset = director.playableAsset as TimelineAsset;

        // 現在カメラが設定されているTrackのBindingをリセット
        director.ClearGenericBinding(timelineAsset.GetOutputTrack(currentTrackIndex));

        if(currentTrackIndex < timelineNum)
        {
            currentTrackIndex++;
        }
        
        // 新しいTrackのBindingにカメラを設定
        director.SetGenericBinding(timelineAsset.GetOutputTrack(currentTrackIndex), bossObj);
        // CinemachineTrackの状態をリセット
        director.Stop();
        director.Play();
    }

    //WAVE移行演出が終わってから敵が動く
    IEnumerator WaveChangeDirection(int waveObjNum)
    {
        if (waveObjNum > 1) wave[waveObjNum - 2].SetActive(false);

        yield return new WaitUntil(() => mainGameController.WaveDirectionEnd);

        timeCountState = TimeCountState.COUNT;
        isInvincible = false;

        //Stage1 Boss Wave4
        if (stageNum == 1 && currentTrackIndex == 4 && hp > 0)
        {
            BossClone();
        }

        wave[waveObjNum - 1].SetActive(true);

        SetBossBinding();
    }

    IEnumerator ClearDirection()
    {
        //時間計測停止
        timeCountState = TimeCountState.STOP;

        GlobalVariables.Score++;
        //爆破エフェクト表示　Particle
        Instantiate(explodeEffect, bossObj.transform.position, Quaternion.identity);

        yield return new WaitForSeconds(0.1f);

        //ボス非表示
        bossObj.SetActive(false);
        
        foreach(var b in bossClone)
        {
            b.SetActive(false);
        }

        yield return new WaitForSeconds(1);

        //HPゲージ非表示
        bossUI.SetActive(false);

        yield return new WaitForSeconds(1);

        mainGameController.GameClearDirection();
    }

    //Stage毎に初期化するもの WAVE移行条件の初期化
    void BossCondition()
    {
        if (stageNum == 1)
        {
            foreach (var b in bossClone)
            {
                b.SetActive(false);
            }

            toWave2 = true;
            toWave3 = false;
            toWave4 = false;
        }

        if (stageNum == 2)
        {
            toWave2 = true;
            toWave3 = true;
            toWave4 = true;
        }
    }

    //Stage1 Boss Wave4 ボス分身
    void BossClone()
    {
        foreach (var b in bossClone)
        {
            b.SetActive(true);
        }

        TimelineAsset timelineAsset = director.playableAsset as TimelineAsset;

        for (int i = 0; i < bossClone.Length; i++)
        {
            director.SetGenericBinding(timelineAsset.GetOutputTrack(i + 6), bossClone[i]);
        }

        director.Stop();
        director.Play();
    }

    void Stage1WaveChange()
    {
        if(stageNum == 1)
        {
            if (currentTrackIndex == 3)
            {
                wave2Time += Time.deltaTime;

                if(wave2Time >= 20) { toWave3 = true; }
            }

            if(currentTrackIndex == 4)
            {
                if(wave3Count >= 4)
                {
                    toWave4 = true;
                }
            }

            if(currentTrackIndex == 5)
            {
                if(wave4NowHP - hp >= cloneDestroyHP / 100 * defaultHp)
                {
                    var pos =  bossClone[bossClone.Length - wave4CloneDestroyCount - 1].transform.position;
                    Instantiate(explodeEffect, pos, Quaternion.identity);

                    if (bossClone.Length - wave4CloneDestroyCount - 1 >= 0) bossClone[bossClone.Length - wave4CloneDestroyCount - 1].SetActive(false);
                    wave4CloneDestroyCount++;
                    wave4NowHP = hp;
                }
                if(hp <= 0)
                {
                    foreach (var b in bossClone)
                    {
                        b.SetActive(false);
                    }
                }
            }
        }
    }

    //ひっかき傷のエフェクト表示 Signal
    public void ScarDirection()
    {
        if(currentTrackIndex == 3)
        {
            scarDirection.SetTrigger("Scar");
        }
    }

    //Stage2 Boss Wave4 HP減少 Signalで呼び出し
    public void Stage2_Wave4()
    {
        if(stageNum == 2 && currentTrackIndex == 5)
        {
            //if(wave4AttackCount == 5)
            //{
            //    hp = (defaultHp * 0.04f);
            //}
            //else { hp -= (defaultHp * 0.05f); }

            hp -= (defaultHp * 0.05f);

            wave4AttackCount++;
        }
    }
}
