using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackChara : MonoBehaviour
{

    [SerializeField] GameObject Panel;
    [SerializeField] GameObject Panel2;
    // Start is called before the first frame update
    public void OnClick()
    {
        var isActive = Panel.activeInHierarchy; // Panel���A�N�e�B�u���擾
        Panel.SetActive(false);

        var isActivee = Panel2.activeInHierarchy; // Panel���A�N�e�B�u���擾
        Panel2.SetActive(true);

    }
}
