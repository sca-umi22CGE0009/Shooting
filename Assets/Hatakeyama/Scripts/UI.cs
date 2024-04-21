using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField]
    private Image[] ui;
    private float _alpha;

    public float alpha
    {
        get {return _alpha;}
    }

    // Start is called before the first frame update
    void Start()
    {
        _alpha=1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < ui.Length; ++i)
        {
            ui[i].color = new Color(ui[i].color.r, ui[i].color.g, ui[i].color.b, alpha);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            _alpha = 0.5f;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            _alpha = 1.0f;
        }
    }
}
