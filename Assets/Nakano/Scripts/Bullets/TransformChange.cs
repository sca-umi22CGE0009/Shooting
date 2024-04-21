using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformChange : MonoBehaviour
{
    public Vector3 PositionChange(RectTransform rect, Canvas canvas)
    {
        //UI���W����X�N���[�����W�ɕϊ�
        Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(canvas.worldCamera, rect.position);

        //���[���h���W
        Vector3 result = Vector3.zero;

        //�X�N���[�����W�����[���h���W�ɕϊ�
        RectTransformUtility.ScreenPointToWorldPointInRectangle(rect, screenPos, canvas.worldCamera, out result);

        return result;
    }
}
