using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public enum SkillState
{
    Stargazer,
    Pentagram,
    EnergyFunnnel,
    MysticField,
    LightingFlush,
    BlueThunder,
}


public class SkillDragAndDrop : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    [SerializeField]
    RectTransform rectTransform;

    [SerializeField]
    SkillReset skillReset;

    [SerializeField]
    SkillState skillState;

    bool downCheck = false;

    bool collsionCheck = false;

    Vector3 startPos = new Vector3(0f,0f,0f);

    
    Queue<Vector3> skillPos = new Queue<Vector3>();

    public SkillState SkillState => skillState;

    // Start is called before the first frame update
    void Start()
    {
        startPos = rectTransform.anchoredPosition3D;
        skillReset.skillRelease += ResetPos;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = Input.mousePosition;

        if(downCheck)
        {
            rectTransform.anchoredPosition3D = new Vector3(mousePos.x,- Screen.height + mousePos.y); 
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        downCheck = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        downCheck = false;
        
        if(collsionCheck)
        {
            rectTransform.anchoredPosition3D = skillPos.Dequeue();
        }
        else
        {
            rectTransform.anchoredPosition3D = startPos;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("SkillSrot"))
        {
            skillPos.Clear();
            skillPos.Enqueue(other.gameObject.GetComponent<RectTransform>().anchoredPosition3D);


            if (other.gameObject.transform.GetChild(1).GetComponent<Collider2D>().enabled)
            {
                collsionCheck = true;

                other.gameObject.transform.GetChild(1).GetComponent<Collider2D>().enabled = false;
            }  
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("SkillSrot"))
        {
            collsionCheck = false;
            StartCoroutine(WaitTime(collision));
        }
    }

    IEnumerator WaitTime(Collider2D collision)
    {
        yield return new WaitForSeconds(0.1f);

        if (collision.gameObject.GetComponent<SetSkillIcon>().CollisionName == "")
            collision.gameObject.transform.GetChild(1).GetComponent<Collider2D>().enabled = true;
    }

    private void ResetPos()
    {
        rectTransform.anchoredPosition3D = startPos;
    }
}