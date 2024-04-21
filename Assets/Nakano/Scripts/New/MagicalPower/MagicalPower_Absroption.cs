using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicalPower_Absroption : MonoBehaviour
{
    [SerializeField]
    RectTransform rectTransform;

    [SerializeField,Header("取得するスキルポイント")]
    int skillPoint;

    [SerializeField,Header("吸収範囲")]
    float absorptionDistance;

    [SerializeField, Header("吸収速度")]
    float absorptionSpeed;
 
    GameObject player;
    RectTransform playerRect;

    Vector3 dist;
    //PlayerのPivot補正
    Vector3 playerPosCorrection = new Vector3(0f,150f,0f);

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (player.name == "MagicSwordsMan(Clone)") playerPosCorrection = new Vector3(0f, 0f, 0f);

        playerRect = player.GetComponent<RectTransform>();
        dist = playerRect.anchoredPosition3D - rectTransform.anchoredPosition3D;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = playerRect.anchoredPosition3D + playerPosCorrection;

        dist = playerPos - rectTransform.anchoredPosition3D;


        if (Vector3.Magnitude(dist) < absorptionDistance)
        {
            rectTransform.anchoredPosition3D = Vector3.MoveTowards(rectTransform.anchoredPosition3D,playerPos,absorptionSpeed);
        }
    }

    public int GetSkillPoint()
    {
        return skillPoint;  
    }
}
