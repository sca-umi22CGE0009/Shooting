using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class No1animetorStop : MonoBehaviour
{
    [SerializeField] GameObject Panel;
    [SerializeField] GameObject Panel2;

    [SerializeField]
    private GameObject statusWindow;

    [SerializeField]
    CharaSelectData charaSelect;

   // [SerializeField] Animator anim;

    //Animator��anim�Ƃ����ϐ��Œ�`����

    private void Start()
    {
        //�ϐ�anim�ɁAAnimator�R���|�[�l���g��ݒ肷��
        //anim = gameObject.GetComponent<Animator>();
    }

    IEnumerator Stop()
    {
        GetComponent<Button>().interactable = false;

        yield return new WaitForSeconds(1);
       // anim.SetBool("blRot", false);

        var isActive = Panel.activeInHierarchy; // Panel���A�N�e�B�u���擾
        Panel.SetActive(false);
        
        var isActivee = Panel2.activeInHierarchy; // Panel���A�N�e�B�u���擾
        Panel2.SetActive(true);
        //GetComponent<Button>().interactable = false;
        //GetComponent<Button>().interactable = true;
        Moveanim();


    }

    public void OnClick()
    {
  
        StartCoroutine(Stop());

        charaSelect.CharaName = GetComponent<Image>().sprite.name;

       // GetComponent<Button>().interactable = true;
        //anim.Play("idle");

    }

    private void Update()
    {
        
    }

    private void Moveanim()
    {
        if (statusWindow.activeSelf)
        {

        }
        else
        {
            GetComponent<Button>().interactable = true;
           
        }
    }
}