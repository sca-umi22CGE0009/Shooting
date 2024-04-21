using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonMove : MonoBehaviour
{
    [SerializeField] private GameObject fadeObj;
    private HorizonFade horizonFade;
    // Start is called before the first frame update
    void Start()
    {
        horizonFade = fadeObj.GetComponent<HorizonFade>();
        fadeObj.SetActive(false);
    }
    public void OnClick()
    {
        fadeObj.SetActive(true);
        horizonFade.FadeOutStart();
        StartCoroutine(fade());
    }
    IEnumerator fade()
    {
        yield return new WaitUntil(() => horizonFade.FadeOutEnd);
        SceneManager.LoadScene("01_TitleScene");
    }
}
