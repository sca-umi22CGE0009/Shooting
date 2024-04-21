using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Draggable : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    // このオブジェクトの元の位置
    private Vector2 prePos;

    // このオブジェクトの元の親
    private GameObject preParent;

    // ドロップ可能エリア
    public List<MonoBehaviour> dropArea;

    // ドラッグ開始時に実行するアクション
    public Action beforeBeginDrag;

    // ドロップ完了時に実行するアクション
    public Action<MonoBehaviour, Action> onDropSuccess;

    // ドロップ可能エリア以外にドロップされたときの処理
    public Action<Action> onDropFail;

    // ドラッグ中、オブジェクトのコピーをその場に残す
    public bool moveCopyObj = false;
    private GameObject copyObj = null;

    //private SkillDefine skillDefine;

    [SerializeField]
    string SkillName;



    public void OnBeginDrag(PointerEventData eventData)
    {
        // ドラッグ開始時に実行するアクションを実行
        if (beforeBeginDrag != null)
        {
            beforeBeginDrag.Invoke();
        }
        // このオブジェクトの元の位置と親を予め保存
        prePos = transform.position;
        preParent = this.transform.parent.gameObject;
        // 最上位に移動
        this.transform.SetParent(transform.root.gameObject.transform, true);

        // オブジェクトのコピーをその場に残す場合、オブジェクトをコピーする
        if (moveCopyObj)
        {
            GameObject target = eventData.pointerDrag;
            copyObj = copy(target);
            // 移動させるオブジェクトは半透明にする
            childHalfA(target);
        }

    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {

        bool isSuccess = false;
        foreach (MonoBehaviour area in dropArea)
        {
            if (contains(area.GetComponent<RectTransform>(), eventData))
            {
                transform.position = area.transform.position;
                // ドロップ可能エリアにこのオブジェクトが含まれる場合
                //onDropSuccess.Invoke(area, resetPos()); // 引数1：ドロップしたエリア、引数2：位置をもとに戻す関数
                isSuccess = true;
                if(SkillName== "Stargazer")
                {
                    SkillDefine skill = GameObject.Find("SkillManager").GetComponent<SkillDefine>();
                   

                    Debug.Log("a");
                }
                Debug.Log("d");
                
            }
        }

        // 失敗時処理
        if (!isSuccess)
        {
            if (onDropFail == null)
            {
                // 失敗時アクションが未設定の場合、位置をもとに戻す
                resetPos().Invoke();
            }
            else
            {
                // アクション設定済みならそれを実行
                onDropFail.Invoke(resetPos());
            }
        }
    }

    private Action resetPos()
    {
        Action ret = () =>
        {
            // 位置をもとに戻す
            transform.position = prePos;
            this.transform.SetParent(preParent.transform, true);
        };
        return ret;
    }

    // targetがareaの範囲内にいるかどうかを判定する
    // https://hacchi-man.hatenablog.com/entry/2020/05/09/220000
    // を参考に作成させていただきました
    private bool contains(RectTransform area, PointerEventData target)
    {
        var selfBounds = GetBounds(area);
        var worldPos = Vector3.zero;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(
            area,
            target.position,
            target.pressEventCamera,
            out worldPos);
        worldPos.z = 0f;
        return selfBounds.Contains(worldPos);
    }

    private Bounds GetBounds(RectTransform target)
    {
        Vector3[] s_Corners = new Vector3[4];
        var min = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
        var max = new Vector3(float.MinValue, float.MinValue, float.MinValue);
        target.GetWorldCorners(s_Corners);
        for (var index2 = 0; index2 < 4; ++index2)
        {
            min = Vector3.Min(s_Corners[index2], min);
            max = Vector3.Max(s_Corners[index2], max);
        }

        max.z = 0f;
        min.z = 0f;

        Bounds bounds = new Bounds(min, Vector3.zero);
        bounds.Encapsulate(max);
        return bounds;
    }

    // ゲームオブジェクトをコピーする
    private GameObject copy(GameObject source)
    {
        GameObject ret = UnityEngine.Object.Instantiate(source);
        // 元オブジェクトと同じ位置に移動させる
        ret.transform.SetParent(source.transform.parent, true);
        ret.transform.position = source.transform.position;
        // 元オブジェクトと同じ大きさにする
        ret.transform.localScale = source.transform.localScale;
        return ret;
    }

    // 子要素の透明度をすべて半分にする
    private void childHalfA(GameObject target)
    {
        Transform children = target.GetComponentInChildren<Transform>();
        if (children != null)
        {
            foreach (Transform child in children)
            {
                if (child.GetComponent<Image>() != null)
                {
                    setA(child.GetComponent<Image>(), child.GetComponent<Image>().color.a / 2);
                }
                if (child.GetComponent<Text>() != null)
                {
                    setA(child.GetComponent<Text>(), child.GetComponent<Text>().color.a / 2);
                }
                childHalfA(child.gameObject);
            }
        }
    }

    // 画像を任意の透明度にする
    private void setA(Image i, float a)
    {
        i.color = new Color(i.color.r, i.color.b, i.color.g, a);
    }
    private void setA(Text i, float a)
    {
        i.color = new Color(i.color.r, i.color.b, i.color.g, a);
    }
}