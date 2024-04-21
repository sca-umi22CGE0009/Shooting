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

    [SerializeField, Header("HP�o�[�̒��g")] Image hpBar;
    [SerializeField, Header("Boss��UI")] GameObject bossUI;
    [SerializeField, Header("Boss��UI")] Animator bossUIAnim;
    [SerializeField, Header("HP�������̌��������܂ł̎���"), Tooltip("(decreaseTime)�b��HP���������I���")] float decreaseTime = 0.2f;

    [SerializeField] Sprite[] hpBars;

    public float BossHp
    {
        get { return hp; }
        set { hp = value; }
    }

    bool screenInitialize = false; //�e�S�����t���O

    [SerializeField] WaveController waveController;
    [SerializeField] PlayableDirector director;
    int currentTrackIndex = 1;

    bool wave2 = false, wave3 = false, wave4 = false, end = false;

    [SerializeField, Header("�{�X�o�ꂩ��{�X��WAVE�P�J�n�܂ł̎���")] float entryTime;

    [SerializeField, Header("�����ʒu�ɖ߂�X�s�[�h")] float moveSpeed;
    [SerializeField, Header("�eWAVE�����ʒu")] GameObject[] pos;
    bool isPosIni = false;

    [SerializeField, Header("�eWAVE�ڍs����Ƃ��̎cHP����"), Tooltip("Element0��0.75�̏ꍇ�A�cHP��75���ȉ���WAVE2�Ɉڍs����")] float[] hpLimit; 

    [SerializeField] GameObject[] wave;

    [SerializeField, Header("�{�X �^�C�����C����")] int timelineNum = 5;

    //�cHP�ȊO��WAVE�ڍs�����p �����B������true�ɂ���Wave�ڍs����
    bool toWave2 = false, toWave3 = false, toWave4 = false;

    //���G���
    bool isInvincible = true;
    public bool Invincible { get { return isInvincible; } }

    //Stage1�p
    [SerializeField, Header("�{�X�̕��g")] GameObject[] bossClone;
    [SerializeField, Header("��")] Animator mirror;
    float wave2Time = 0; //Wave2 ���Ԍv��
    int wave3Count = 0; //Wave3 �U���񐔌v��
    public int BubbleCount { get { return wave3Count; } set { wave3Count= value; } }

    [SerializeField, Tooltip("�{�X��HP���S�̂�(cloneDestroyHP)%���������Ƃ����g����̏�����")] float cloneDestroyHP;
    float wave4NowHP = 0;
    int wave4CloneDestroyCount = 0;

    //Stage2�p
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
        //Stage1��HP�ȊO��WAVE�ڍs��������p ���X�e�[�W�ł�HP�ȊO��WAVE�ڍs������������ꍇ�Aswitch���œZ�߂�̂��ǂ�����

        //Debug
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    if (!isInvincible) hp -= 200;
        //}

        //HP�����ȉ��ɂȂ����珉���ʒu�ɖ߂�
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
                    mainGameController.WaveDirection(false, currentTrackIndex); //WAVE�ڍs���o�Đ�
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

    //�{�X��ڍs
    IEnumerator BossReach()
    {
        isInvincible = true;

        //WAVE���S�ďI�������
        yield return new WaitUntil(() => waveController.WaveCompleted);

        timeCountState = TimeCountState.STOP;

        yield return new WaitForSeconds(3f);

        //�o��
        TimelineAsset timelineAsset = director.playableAsset as TimelineAsset;
        director.SetGenericBinding(timelineAsset.GetOutputTrack(currentTrackIndex), bossObj);
        director.Stop();
        director.Play();

        yield return new WaitForSeconds(1f);

        //�{�X��UI�o��
        bossUIAnim.SetBool("Entry", true);

        //������ւ�ɃX�g�[���[�ǉ��H

        yield return new  WaitForSeconds(entryTime);

        yield return new WaitUntil(() => mainGameController.WaveDirectionEnd);

        //SetBossBinding();
        WaveChange();
    }

    //HP�`�� HP�ɉ�����WAVE�ڍs
    void HpDirection()
    {
        //HP�����`��
        hpRatio = hp / defaultHp;
        if(hpBar.fillAmount > hpRatio) { hpBar.fillAmount -= (hpBar.fillAmount - hpRatio) / decreaseTime * Time.deltaTime; }
        else { hpBar.fillAmount = hpRatio; }

        //HP�ɉ����ĉ摜�ύX
        if(hpRatio >= 2.0f / 3.0f) { hpBar.sprite = hpBars[0]; }
        else if (hpRatio >= 1.0f / 3.0f && hpRatio < 2.0f / 3.0f) { hpBar.sprite = hpBars[1]; }
        else if(hpRatio < 1.0f / 3.0f) { hpBar.sprite = hpBars[2]; }

        //�c��Hp�ɉ�����WAVE�ύX wave2��wave3�Ȃǂ̃t���O��WaveChange()����x�����Ăяo�����߂̂���
        if (hpRatio <= hpLimit[0] && !wave2)
        {
            hpRatio = hpLimit[0];
            isInvincible = true; //Wave�ڍs������B�����Ă��Ȃ��A����HP�����ʂ܂ŉ������Ă���ꍇ�͂���ȏ�HP�̌��������Ȃ�
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

    //���݂�WAVE���I��������J��Ԃ� Signal�ŌĂяo��
    public void AnimationReplay()
    {
        if(currentTrackIndex <= timelineNum)
        {
            director.Stop();
            director.Play();
        }

        //Stage1 Boss Wave4 �{�X�̕��g���ړ����������炻��ȍ~�����Ȃ��Ȃ�(�e�͔��˂���)
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

    //WAVE�ڍs
    void WaveChange()
    {
        isPosIni = true;
        screenInitialize = true;
        timeCountState = TimeCountState.STOP;
        isInvincible = true;
    }

    //��ʂ���e��S�폜 
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

    //Timeline�ҏW
    void SetBossBinding()
    {
        TimelineAsset timelineAsset = director.playableAsset as TimelineAsset;

        // ���݃J�������ݒ肳��Ă���Track��Binding�����Z�b�g
        director.ClearGenericBinding(timelineAsset.GetOutputTrack(currentTrackIndex));

        if(currentTrackIndex < timelineNum)
        {
            currentTrackIndex++;
        }
        
        // �V����Track��Binding�ɃJ������ݒ�
        director.SetGenericBinding(timelineAsset.GetOutputTrack(currentTrackIndex), bossObj);
        // CinemachineTrack�̏�Ԃ����Z�b�g
        director.Stop();
        director.Play();
    }

    //WAVE�ڍs���o���I����Ă���G������
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
        //���Ԍv����~
        timeCountState = TimeCountState.STOP;

        GlobalVariables.Score++;
        //���j�G�t�F�N�g�\���@Particle
        Instantiate(explodeEffect, bossObj.transform.position, Quaternion.identity);

        yield return new WaitForSeconds(0.1f);

        //�{�X��\��
        bossObj.SetActive(false);
        
        foreach(var b in bossClone)
        {
            b.SetActive(false);
        }

        yield return new WaitForSeconds(1);

        //HP�Q�[�W��\��
        bossUI.SetActive(false);

        yield return new WaitForSeconds(1);

        mainGameController.GameClearDirection();
    }

    //Stage���ɏ������������ WAVE�ڍs�����̏�����
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

    //Stage1 Boss Wave4 �{�X���g
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

    //�Ђ��������̃G�t�F�N�g�\�� Signal
    public void ScarDirection()
    {
        if(currentTrackIndex == 3)
        {
            scarDirection.SetTrigger("Scar");
        }
    }

    //Stage2 Boss Wave4 HP���� Signal�ŌĂяo��
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
