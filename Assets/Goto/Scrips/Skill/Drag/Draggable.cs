using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Draggable : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    // ���̃I�u�W�F�N�g�̌��̈ʒu
    private Vector2 prePos;

    // ���̃I�u�W�F�N�g�̌��̐e
    private GameObject preParent;

    // �h���b�v�\�G���A
    public List<MonoBehaviour> dropArea;

    // �h���b�O�J�n���Ɏ��s����A�N�V����
    public Action beforeBeginDrag;

    // �h���b�v�������Ɏ��s����A�N�V����
    public Action<MonoBehaviour, Action> onDropSuccess;

    // �h���b�v�\�G���A�ȊO�Ƀh���b�v���ꂽ�Ƃ��̏���
    public Action<Action> onDropFail;

    // �h���b�O���A�I�u�W�F�N�g�̃R�s�[�����̏�Ɏc��
    public bool moveCopyObj = false;
    private GameObject copyObj = null;

    //private SkillDefine skillDefine;

    [SerializeField]
    string SkillName;



    public void OnBeginDrag(PointerEventData eventData)
    {
        // �h���b�O�J�n���Ɏ��s����A�N�V���������s
        if (beforeBeginDrag != null)
        {
            beforeBeginDrag.Invoke();
        }
        // ���̃I�u�W�F�N�g�̌��̈ʒu�Ɛe��\�ߕۑ�
        prePos = transform.position;
        preParent = this.transform.parent.gameObject;
        // �ŏ�ʂɈړ�
        this.transform.SetParent(transform.root.gameObject.transform, true);

        // �I�u�W�F�N�g�̃R�s�[�����̏�Ɏc���ꍇ�A�I�u�W�F�N�g���R�s�[����
        if (moveCopyObj)
        {
            GameObject target = eventData.pointerDrag;
            copyObj = copy(target);
            // �ړ�������I�u�W�F�N�g�͔������ɂ���
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
                // �h���b�v�\�G���A�ɂ��̃I�u�W�F�N�g���܂܂��ꍇ
                //onDropSuccess.Invoke(area, resetPos()); // ����1�F�h���b�v�����G���A�A����2�F�ʒu�����Ƃɖ߂��֐�
                isSuccess = true;
                if(SkillName== "Stargazer")
                {
                    SkillDefine skill = GameObject.Find("SkillManager").GetComponent<SkillDefine>();
                   

                    Debug.Log("a");
                }
                Debug.Log("d");
                
            }
        }

        // ���s������
        if (!isSuccess)
        {
            if (onDropFail == null)
            {
                // ���s���A�N�V���������ݒ�̏ꍇ�A�ʒu�����Ƃɖ߂�
                resetPos().Invoke();
            }
            else
            {
                // �A�N�V�����ݒ�ς݂Ȃ炻������s
                onDropFail.Invoke(resetPos());
            }
        }
    }

    private Action resetPos()
    {
        Action ret = () =>
        {
            // �ʒu�����Ƃɖ߂�
            transform.position = prePos;
            this.transform.SetParent(preParent.transform, true);
        };
        return ret;
    }

    // target��area�͈͓̔��ɂ��邩�ǂ����𔻒肷��
    // https://hacchi-man.hatenablog.com/entry/2020/05/09/220000
    // ���Q�l�ɍ쐬�����Ă��������܂���
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

    // �Q�[���I�u�W�F�N�g���R�s�[����
    private GameObject copy(GameObject source)
    {
        GameObject ret = UnityEngine.Object.Instantiate(source);
        // ���I�u�W�F�N�g�Ɠ����ʒu�Ɉړ�������
        ret.transform.SetParent(source.transform.parent, true);
        ret.transform.position = source.transform.position;
        // ���I�u�W�F�N�g�Ɠ����傫���ɂ���
        ret.transform.localScale = source.transform.localScale;
        return ret;
    }

    // �q�v�f�̓����x�����ׂĔ����ɂ���
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

    // �摜��C�ӂ̓����x�ɂ���
    private void setA(Image i, float a)
    {
        i.color = new Color(i.color.r, i.color.b, i.color.g, a);
    }
    private void setA(Text i, float a)
    {
        i.color = new Color(i.color.r, i.color.b, i.color.g, a);
    }
}