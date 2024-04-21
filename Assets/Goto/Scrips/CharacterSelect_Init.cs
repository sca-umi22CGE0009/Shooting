using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelect_Init : MonoBehaviour
{
    [SerializeField]
    CharaSelectData charaSelect;

    [SerializeField]
    Button button;

    private void OnEnable()
    {
        charaSelect.CharaSelect(button);
    }
}
