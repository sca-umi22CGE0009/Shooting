using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class OnStageMagicalPower : MonoBehaviour
{
    [SerializeField,Header("マジカルパワーの数")]
    int magicalPowerNumber;

    [SerializeField]
    GameObject magicalPowerPrefab;

    public delegate void WaveCallBack();

    public WaveCallBack waveCallBack = default;

    // Start is called before the first frame update
    void Start()
    {
        waveCallBack += OnWaveAppearanceMagicalPower;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnWaveAppearanceMagicalPower()
    {
        for(int i = 0; i < magicalPowerNumber; i++)
        {            
            GameObject obj = Instantiate(magicalPowerPrefab);
            obj.transform.SetParent(transform);  
            RectTransform rect = obj.GetComponent<RectTransform>();
            rect.anchoredPosition3D = new Vector3(Random.Range(-Screen.width/2,Screen.width / 2),Random.Range(-Screen.height/2,Screen.height/2),0f);
            rect.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}
