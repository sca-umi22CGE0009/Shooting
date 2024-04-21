using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using static PlayerManager;

public class AnimeController : MonoBehaviour
{
    public PlayableDirector PlayableDirector;
    public int maxPlayCount = 3;

    private int playCount = 0;

    public WaveController waveController;

    float coolTime = 1;

    // Start is called before the first frame update
    void Start()
    {
        PlayableDirector.stopped += OnTimelineStopped;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTimelineStopped(PlayableDirector director)
    {
        playCount++;
        if(playCount < maxPlayCount)
        {
            if (this.gameObject.activeSelf)
            {
                StartCoroutine(CoolTime());
            }
        }
        else if(playCount >= maxPlayCount && !waveController.WaveCompleted)
        {
            waveController.PlayNextWave(); //ˆê’è‰ñ”Ä¶‚µ‚½‚çŸ‚ÌWAVE‚ÉˆÚs‚·‚é
        }
    }

    IEnumerator CoolTime()
    {
        yield return new WaitForSeconds(coolTime);

        if (game_stat != GameStat.DETH)
        {
            PlayableDirector.Play();
        }
    }
}
