using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mahouzinnkaitenn : MonoBehaviour
{
    // Start is called before the first frame update
    RectTransform RectTransform_get;
    void Start()
    {
        RectTransform_get = gameObject.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        
        // y�������ɂ���5�x�Ax�������ɂ���5�x�A��]������Quaternion���쐬�i�ϐ���rot�Ƃ���j
        Quaternion rot = Quaternion.Euler(0, 0, 1);
        // ���݂̎��M�̉�]�̏����擾����B
        Quaternion q = this.transform.rotation;
        // �������āA���g�ɐݒ�
        this.transform.rotation = q * rot;
    }
}