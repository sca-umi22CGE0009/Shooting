using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragTest : MonoBehaviour, IDragHandler, IDropHandler
{
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnDrop(PointerEventData eventData)
    {
        var raycastResults = new List<RaycastResult>();
        PointerEventData eventDataCurrent = new PointerEventData(EventSystem.current);
        EventSystem.current.RaycastAll(eventDataCurrent, raycastResults);
        foreach (var hit in raycastResults)
        {
            if (hit.gameObject.CompareTag("NullUI"))
            {
                Destroy(gameObject);
                Debug.Log("set");
            }
        }
       
    }
}