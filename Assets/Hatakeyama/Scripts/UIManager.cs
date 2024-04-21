using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private Image HPGauge;
    [SerializeField] private Image SPGauge;
    [SerializeField] private Text SPText;
    [SerializeField] private int maxHP;
    public static int HP;
    private float tmpHP;
    private int SP;

    public static int skillGaugePoint;

    [SerializeField] private int[] setSkillsNumber = new int[4];
    [SerializeField] private Sprite[] skillSprite = new Sprite[6];
    [SerializeField] private Image[] setSkills = new Image[4];

    [SerializeField]
    PlayableDirector[] skillEffectAnimation;

    [SerializeField]
    SetSkillImage setSkillImage;

    private UI gauge;


    int backSkillGaugePoint = 0;
    bool spAnimationEnd = true;

    public static bool isPentagram = false;
    public static bool isEnergyFunnel = false;
    public static bool isMysticField = false;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < skillEffectAnimation.Length; i++)
        {
            skillEffectAnimation[i].Stop();
        }

        HP = maxHP;
        tmpHP=HP;
        SP = 0;
        SPGauge.fillAmount = 0;
        skillGaugePoint = 0;
        backSkillGaugePoint = 0;

        for (int i = 0; i < 4; ++i)
        {
            //setSkillsNumber[i]=(int)SkillDefine.skillSets[i];
            setSkills[i].sprite= skillSprite[(int)SkillDefine.skillSets[i]];
            //setSkills[i].sprite = skillSprite[setSkillsNumber[i]];
        }

        gauge = GameObject.Find("Skills").GetComponent<UI>();

        //onStageMagicalPower.waveCallBack += ;
    }

    // Update is called once per frame
    void Update()
    {
        HPGauge.fillAmount = tmpHP / maxHP;
        SPText.text = SP + "/10";


        if (SP < 10)
        {
            if (skillGaugePoint != backSkillGaugePoint && spAnimationEnd)
            {
                StartCoroutine(SPMoveAnimation(1.0f));
            }
            backSkillGaugePoint = skillGaugePoint;
        }
        else
        {
            SPGauge.fillAmount = 1;
        }



        if (tmpHP > HP)
        {
            tmpHP -= maxHP / 10 * Time.deltaTime * 2.0f;
        }
        else
        {
            tmpHP = HP;
        }

        if (SP >= 4)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                skillOne();

            else if (Input.GetKeyDown(KeyCode.Alpha2))
                skillTow();

            else if (Input.GetKeyDown(KeyCode.Alpha3))
                skillThree();

            else if (Input.GetKeyDown(KeyCode.Alpha4))
                skillFour();

            for (int i = 0; i < setSkillsNumber.Length; ++i)
            {
                setSkills[setSkillsNumber[i]].color = new Color(1, 1, 1, gauge.alpha);
            }
        }
        else
        {
            for (int i = 0; i < setSkillsNumber.Length; ++i)
            {
                setSkills[i].color = new Color(0.5f, 0.5f, 0.5f, gauge.alpha);
            }
        }
    }

    IEnumerator SPMoveAnimation(float time)
    {
        spAnimationEnd = false;

        float deltaTime = 0;
        float currentMax = (float)skillGaugePoint / 100.0f;

        while (SPGauge.fillAmount < currentMax)
        {
            yield return null;

            deltaTime += Time.deltaTime;
            SPGauge.fillAmount = Mathf.Lerp(SPGauge.fillAmount, currentMax, deltaTime/time);
            if (SPGauge.fillAmount >= 1) break;
        }

        SPGauge.fillAmount = currentMax;

        if (SPGauge.fillAmount >= 1f)
        {
            skillGaugePoint = 0;
            SPGauge.fillAmount = 0;
            SP += 1;
        }
        spAnimationEnd = true;
    }

    void skillOne()
    {
        SP-=4;
        CheckSkill(0);
    }

    void skillTow()
    {
        SP-=4;
        CheckSkill(1);
    }

    void skillThree()
    {
        SP-=4;
        CheckSkill(2);
    }

    void skillFour()
    {
        SP-=4;
        CheckSkill(3);
    }

    void CheckSkill(int i)
    {
        if (setSkillImage.SkillName[i].sprite.name == "1(Clone)") 
            StartCoroutine(Stargazer());


        if (setSkillImage.SkillName[i].sprite.name == "2(Clone)")
            StartCoroutine(Pentagram());

        if (setSkillImage.SkillName[i].sprite.name == "3(Clone)")
            StartCoroutine(EnergyFunnel());

        if (setSkillImage.SkillName[i].sprite.name == "4(Clone)")
            MysticField();

/*
        if (setSkillImage.SkillName[i].sprite.name == "5(Clone)")
        {

        }

        if (setSkillImage.SkillName[i].sprite.name == "6(Clone)")
        {

        }*/
    }

    IEnumerator Stargazer()
    {
        skillEffectAnimation[0].Play();
        yield return new WaitForSeconds(2.0f);
        skillEffectAnimation[0].Stop();
    }

    IEnumerator Pentagram()
    {
        skillEffectAnimation[1].Play();
        yield return new WaitForSecondsRealtime(1.0f);
        isPentagram = true;

        //èàóù
        yield return new WaitForSecondsRealtime(1.0f);
        isPentagram = false;
    }

    IEnumerator EnergyFunnel()
    {
        isEnergyFunnel = true;
        yield return new WaitForSeconds(10.0f);
        isEnergyFunnel = false;
    }

    void MysticField()
    {
        isMysticField = true;
    }
}
