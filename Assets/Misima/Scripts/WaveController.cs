using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.UI;
using static PlayerManager;
using static TimeScoreCounter;

public class WaveController : MonoBehaviour
{
    //public TimelineAsset[] waveTimelines;
    public GameObject[] waveObject;

    private PlayableDirector playableDirector;
    private int currentWaveIndex = 0;
    bool waveCompleted = false;

    [SerializeField] MainGameController mainGameController;

    [SerializeField]
    OnStageMagicalPower onStageMagicalPower;

    public bool WaveCompleted { get { return waveCompleted; } }

    public int WaveNum { get { return currentWaveIndex + 1; } }

    // Start is called before the first frame update
    void Start()
    {
        //playableDirector = GetComponent<PlayableDirector>();
        playableDirector = waveObject[0].GetComponent<PlayableDirector>();
        if (playableDirector == null)
        {
            //Debug.LogError("No PlayableDirector component found on the WaveController game object.");
        }
        else 
        { 
            PlayNextWave();
        }
    }

    public void PlayNextWave()
    {
        if(currentWaveIndex < waveObject.Length)
        {
            mainGameController.WaveDirection(true, WaveNum); //WAVEà⁄çsââèoçƒê∂

            //playableDirector.playableAsset = waveTimelines[currentWaveIndex];

            if (game_stat != GameStat.DETH)
            {
                StartCoroutine(WaveChange());
            }
        }
        else
        {
            Debug.Log("All waves completed!");
            waveCompleted = true;
        }
    }

    IEnumerator WaveChange()
    {
        yield return new WaitUntil(() => mainGameController.WaveDirectionEnd);
        
        //éüÇÃWAVEÇ…à⁄çs
        playableDirector = waveObject[currentWaveIndex].GetComponent<PlayableDirector>();
        playableDirector.Play();
        currentWaveIndex++;
        onStageMagicalPower.waveCallBack?.Invoke();
    }
}
