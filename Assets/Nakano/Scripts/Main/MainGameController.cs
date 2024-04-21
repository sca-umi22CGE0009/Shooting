using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static PlayerManager;
using static TimeScoreCounter;

public class MainGameController : MonoBehaviour
{
    [SerializeField] int stageNum;
    public int StageNumber { get { return stageNum; } }

    string level;

    [SerializeField] HorizonFade fade;
    [SerializeField] WaveController waveController;
    [SerializeField, Header("�Q�[���J�n���o")] Animator startDirection;

    [SerializeField, Header("WAVE�ڍs���o")] Animator waveDirection;
    [SerializeField] Text waveDirectionText;
    [SerializeField, Header("WAVE�Ԃ̃N�[���^�C��")] float coolTime;
    bool waveDirectionEnd = false;

    [SerializeField, Header("�Q�[���I�[�o�[/�N���A���o")] Animator endDirection;
    [SerializeField] Text endDirectionText;
    
    [SerializeField, Header("�w�i")] BackGround bg1, bg2;

    [SerializeField, Header("�^�C��")] Text timer;
    [SerializeField, Header("�X�R�A")] Text score;

    [SerializeField] GameObject scoreWindow;
    [SerializeField] GameObject resultWindow;
    [SerializeField] GameObject gameoverWindow;
    [SerializeField] Result resultScript;
    [SerializeField] Result gameoverScript;

    [SerializeField] GameObject maingameUI;

    bool isGameover = false;
    bool gameoverDir = true;

    public bool WaveDirectionEnd
    {
        get { return waveDirectionEnd;}
    }

    void Start()
    {
        waveController.enabled = false;
        fade.FadeInStart();
        StartCoroutine(GameStart());

        resultScript.enabled = false;
        gameoverScript.enabled = false;

        if(Difficultylevel.difficulty == null) { level = "Normal"; }
        else level = Difficultylevel.difficulty;
    }

    IEnumerator GameStart()
    {
        timeCountState = TimeCountState.STOP;

        //�t�F�[�h���I�������
        yield return new WaitUntil(() => fade.FadeInEnd);

        //�Q�[���J�n�̃A�j���[�V�������Đ�
        startDirection.SetTrigger("Start");

        //�w�i�ړ�
        bg1.MoveTime = 3;
        bg1.MoveDistance = -351;
        bg2.MoveTime = 3;
        bg2.MoveDistance = -351;

        bg1.Move();
        bg2.Move();

        yield return new WaitUntil(() => startDirection.GetCurrentAnimatorStateInfo(0).IsName("StartDirection_End"));
        waveController.enabled = true;

        timeCountState = TimeCountState.COUNT;
    }

    void Update()
    {
        if (game_stat == GameStat.DETH && !isGameover)
        {
            isGameover = true;
        }

        if(isGameover && gameoverDir)
        {
            GameOverDirection();
            gameoverDir = false;
        }

        TimeDisp();
        ScoreDisp();
    }

    void TimeDisp()
    {
        int hours = Mathf.FloorToInt(GlobalVariables.AliveTime / 3600);
        int mitutes = Mathf.FloorToInt((GlobalVariables.AliveTime - hours * 3600) / 60);
        int seconds = Mathf.FloorToInt(GlobalVariables.AliveTime - hours * 3600 - mitutes * 60);

        timer.text = hours.ToString("d2") + ":" + mitutes.ToString("d2") + ":" + seconds.ToString("d2");
    }

    void ScoreDisp()
    {
        score.text =  $"{GlobalVariables.Score.ToString()} ��";
    }

    /// <summary>
    /// WAVE�ڍs���o�Đ� bgScroll -> �w�i�̈ړ������邩�@WaveNumber -> ���Ԗڂ�WAVE��
    /// </summary>
    /// <param name="bgScroll"></param>
    /// <param name="WaveNumber"></param>
    public void WaveDirection(bool bgScroll, int WaveNumber)
    {
        if(!isGameover)
        {
            timeCountState = TimeCountState.STOP;
            waveDirectionEnd = false;
            waveDirectionText.text = "WAVE" + (WaveNumber).ToString();

            //�uWAVE�v�̃e�L�X�g����ʓ���
            waveDirection.SetTrigger("In");

            StartCoroutine(WaveWait(bgScroll));

            //�v���C���[�̓������~�߂�
        }
    }

    IEnumerator WaveWait(bool bgScroll)
    {
        //�uWAVE�v�̃e�L�X�g�������ɂ���܂ő҂�
        yield return new WaitUntil(() => waveDirection.GetCurrentAnimatorStateInfo(0).IsName("WaveDirection_Center"));

        //�w�i�ړ�
        if (waveController.WaveNum != 1 && bgScroll)
        {
            bg1.MoveTime = 2;
            bg1.MoveDistance = -70;
            bg2.MoveTime = 2;
            bg2.MoveDistance = -70;

            bg1.Move();
            bg2.Move();
        }
        
        //�N�[���^�C��
        yield return new WaitForSeconds(coolTime);

        //�uWAVE�v�̃e�L�X�g����ʊO��
        waveDirection.SetTrigger("Out");
        yield return new WaitForSeconds(1);

        //�G�ړ��J�n
        waveDirectionEnd = true;

        //�v���C���[�����ĊJ
        timeCountState = TimeCountState.COUNT;
    }

    public void GameClearDirection()
    {
        timeCountState = TimeCountState.STOP;

        if(stageNum == 1) { endDirectionText.text = "Stage Clear"; }
        if(stageNum == 2) { endDirectionText.text = "Game Clear"; }
        
        endDirection.SetTrigger("End");

        StartCoroutine(ToNextScene(true));
    }

    void GameOverDirection()
    {
        timeCountState = TimeCountState.STOP;
        endDirectionText.text = "Game Over";
        endDirection.SetTrigger("End");
        UIManager.HP = 0;

        StartCoroutine(ToNextScene(false));
    }

    IEnumerator ToNextScene(bool isClear)
    {
        yield return new WaitForSeconds(3);

        fade.FadeOutStart();

        yield return new WaitUntil(() => fade.FadeOutEnd);

        yield return new WaitForSeconds(1);

        if (isClear)
        {
            switch (stageNum)
            {
                case 1:
                    switch (level)
                    {
                        case "Easy":
                            break;
                        case "Normal":
                            SceneManager.LoadScene("Stage2-Normal");
                            break;
                        case "Hard":
                            SceneManager.LoadScene("Stage2-Hard");
                            break;
                        case "Galaxy":
                            break;
                    }
                    break;
                case 2:
                    StartCoroutine(ResultWindow("clear"));
                    break;
            }
        }
        else { StartCoroutine(ResultWindow("gameover")); }
    }

    IEnumerator ResultWindow(string s)
    {
        scoreWindow.SetActive(true);
        maingameUI.SetActive(false);
        
        yield return new WaitForSeconds(1f);

        fade.FadeInStart();

        switch (s)
        {
            case "clear":
                resultWindow.SetActive(true);
                yield return new WaitUntil(() => fade.FadeInEnd);
                yield return new WaitForSeconds(1);
                resultScript.enabled = true;
            break;
            case "gameover":
                gameoverWindow.SetActive(true);
                yield return new WaitUntil(() => fade.FadeInEnd);
                yield return new WaitForSeconds(1);
                gameoverScript.enabled = true;
                break;
        }
        
    }
}
