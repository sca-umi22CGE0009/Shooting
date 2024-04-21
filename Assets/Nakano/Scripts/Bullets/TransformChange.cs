using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformChange : MonoBehaviour
{
    public Vector3 PositionChange(RectTransform rect, Canvas canvas)
    {
        //UI座標からスクリーン座標に変換
        Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(canvas.worldCamera, rect.position);

        //ワールド座標
        Vector3 result = Vector3.zero;

        //スクリーン座標→ワールド座標に変換
        RectTransformUtility.ScreenPointToWorldPointInRectangle(rect, screenPos, canvas.worldCamera, out result);

        return result;
    }
}
