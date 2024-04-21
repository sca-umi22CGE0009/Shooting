using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pose : MonoBehaviour
{
    private GameObject poseBG;
    public static int poseInputCount;

    [SerializeField]
    private GameObject[] Buttons;
    // Start is called before the first frame update
    void Start()
    {
        poseBG = GameObject.Find("PoseBackGround");
        poseInputCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            poseInputCount++;
        }
        if (poseInputCount % 2 == 0)
        {
            poseBG.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            poseBG.SetActive(true);
            Time.timeScale = 0;
        }
    }

}
