using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Video;
using System.Collections;

public class SkillInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    Text textMessage;

    [SerializeField]
    Text skillName;

    [SerializeField]
    ScenarioTextManager scenarioTextManager;

    [SerializeField,Header("Preview“®‰æ")] 
    VideoPlayer videoPlayer;

    [SerializeField]
    VideoClip[] clip;

    public void OnPointerEnter(PointerEventData eventData)
    { 
        int number = (int)eventData.pointerEnter.GetComponent<SkillDragAndDrop>().SkillState;

        skillName.text = scenarioTextManager.SkillName[number];  
        textMessage.text = scenarioTextManager.TextSkillMessage[number];
        videoPlayer.clip = clip[number];
        videoPlayer.Play();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        int number = (int)eventData.pointerEnter.GetComponent<SkillDragAndDrop>().SkillState;

        skillName.text = "";
        textMessage.text = "";
        videoPlayer.Stop();
    }

}