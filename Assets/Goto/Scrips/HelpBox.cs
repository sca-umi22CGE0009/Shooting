using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Video;
using System.Collections;

public class HelpBox : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private string _helpMessage;
    [SerializeField] private Text _text;

    [SerializeField,TextArea(3,10)] private string helpMessage;
   [SerializeField] private Text text;

    [SerializeField] VideoPlayer videoPlayer;
    [SerializeField] GameObject Panel;


    public void OnPointerEnter(PointerEventData eventData)
    {

        text.text = helpMessage;
        text.transform.gameObject.SetActive(true); //�X�L�����\��

        //_text.text = _helpMessage;
        _text.transform.gameObject.SetActive(true); //�X�L������

        // videoPlayer.Play();
        Panel.SetActive(true); //videoPlayer�̃p�l���\��
        videoPlayer.Play();
        // StartCoroutine(Starts());

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _text.transform.gameObject.SetActive(false); //�e�L�X�g����
        text.transform.gameObject.SetActive(false);�@//�e�L�X�g����

        videoPlayer.Stop();
        Panel.SetActive(false); //videoplayer�̃p�l����\��

        // StartCoroutine(Stop());

    }
}