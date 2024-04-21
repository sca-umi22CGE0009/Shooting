using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CommonlyUsed;
using UnityEngine.UI;

public class SetSkillIcon : MonoBehaviour
{
    [SerializeField]
    string skillSrotName;

    const string TAG_NAME = "SkillIcon";

    public string CollisionName { get; private set; } = "";

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag(TAG_NAME))
        {
            if(skillSrotName == gameObject.name)
            {
                skillSrotName = "";
                CollisionName = collision.gameObject.name;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(TAG_NAME))
        {
            CollisionName = "";
            skillSrotName = gameObject.name;
        }
    }
}