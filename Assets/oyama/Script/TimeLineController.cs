using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;


/// <summary>
/// Author ç≤ì°èv
/// </summary>
public class TimeLineController : MonoBehaviour
{
    [SerializeField]
    PlayableDirector titleAnimation;

    WaitForSeconds waitTime = new WaitForSeconds(5.0f);

    // Start is called before the first frame update
    void Start()
    {
        titleAnimation.Stop();
        StartCoroutine(TitleAnimation());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator TitleAnimation()
    {
        while(true)
        {
            yield return waitTime;

            titleAnimation.Play();
        }
    }
}
