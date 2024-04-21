using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class TLController : MonoBehaviour
{
    public TimelineAsset timelineAsset;
    public int maxPlaybackCount = 3; // 最大再生回数
    private int currentPlaybackCount = 0;
    private PlayableDirector director;

    private void Start()
    {
        director = gameObject.AddComponent<PlayableDirector>();
        director.playableAsset = timelineAsset;
        director.Stop();
        PlayTimeline();
    }


    private void PlayTimeline()
    {
        if (currentPlaybackCount < maxPlaybackCount)
        {
            director.stopped += OnTimelineStopped;
            director.Play();
            
            currentPlaybackCount++;
        }
        else
        {
            director.Stop();
        }
    }

    private void OnTimelineStopped(PlayableDirector director)
    {
        director.stopped -= OnTimelineStopped;
        currentPlaybackCount++;
        director.Play();
        //Debug.Log("通った");
        // ここで次の動作をトリガーする処理を記述
        TriggerNextAction();
    }

    private void TriggerNextAction()
    {
        // 次の動作をトリガーする処理を記述
    }
}
