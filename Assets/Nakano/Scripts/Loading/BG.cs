using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG : MonoBehaviour
{
    [SerializeField] float speed;

    private float length, height;
    private Vector3 startPos;

    bool isMove = true;

    public bool IsMove
    {
        get { return isMove; }
        set { isMove = value; }
    }

    void Start()
    {
        startPos = transform.position;
        length = GetComponent<RectTransform>().sizeDelta.x;
        height = GetComponent<RectTransform>().sizeDelta.y;
    }

    void FixedUpdate()
    {
        if(isMove)
        {
            transform.Translate(speed * Time.deltaTime, 0, 0);
        }

        if (transform.position.x <= -length / 2)
            transform.position = new Vector3(length * 1.5f, height / 2, 0);
    }
}
