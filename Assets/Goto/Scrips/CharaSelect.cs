using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaSelect : MonoBehaviour
{
    public static string charaSelect;

    public void ButtonClick(string button)
    {
        switch (button)
        {
            case "Chara1":
                charaSelect = "Chara1";
                break;
            case "Chara2":
                charaSelect = "Chara2";
                break;
        }

  }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
