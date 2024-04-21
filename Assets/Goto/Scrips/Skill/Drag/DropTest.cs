using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropTest : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        Destroy(gameObject);
    }
}