using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharaPanel1 : MonoBehaviour
{
    [SerializeField] GameObject Panel;
    [SerializeField] GameObject Panel2;



    public void OnClick()
    {
        // Panel���A�N�e�B�u���擾
        Panel.SetActive(false);
        Panel2.SetActive(true);
    }
}   