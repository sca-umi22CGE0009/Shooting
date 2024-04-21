using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInSample : MonoBehaviour
{
    [SerializeField] HorizonFade fade;

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        if(!fade.FadeInEnd)
        {
            fade.FadeInStart();
        }

        if(fade.FadeInEnd)
        {
        }
    }
}
