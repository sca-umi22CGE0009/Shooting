using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System.Collections;

public class Atteck : MonoBehaviour
{
    public GameObject[] Train;

    // Start is called before the first frame update
    void Start()
    {
        int number = Random.Range(0,Train.Length);
        Instantiate(Train[number],transform.position,transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
