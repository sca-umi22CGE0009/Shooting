using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackCharaMain : MonoBehaviour
{ 
    [SerializeField] GameObject Panel;
    [SerializeField] GameObject Panel2;



    public void OnClick()
    {


        var isActive = Panel.activeInHierarchy; // Panel���A�N�e�B�u���擾
        Panel.SetActive(false);

        var isActivee = Panel2.activeInHierarchy; // Panel���A�N�e�B�u���擾
        Panel2.SetActive(true);

        SceneManager.LoadScene("02_CharacterSelect");

    }
}