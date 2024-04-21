using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignPattern;


public class CharaData : Singleton<CharaData>
{
    public override bool DestroyTragetGameObject => true;

    public string CharaName { get; set; } = "";

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);    
    }
}
