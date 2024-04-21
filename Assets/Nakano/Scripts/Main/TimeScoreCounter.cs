using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScoreCounter : MonoBehaviour
{
    [HideInInspector] public enum TimeCountState { COUNT = 0, STOP, }
    public static TimeCountState timeCountState;

    void Start()
    {
        timeCountState = TimeCountState.STOP;
    }

    void Update()
    {
        switch (timeCountState)
        {
            case TimeCountState.COUNT:
                GlobalVariables.AliveTime += Time.deltaTime;
                break;
            case TimeCountState.STOP:
                break;
        }
    }
}
