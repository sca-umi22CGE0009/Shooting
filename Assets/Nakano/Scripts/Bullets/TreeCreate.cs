using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class Data
{
    public int num;
    public Vector3 pos;
}

public class TreeCreate : MonoBehaviour
{
    [SerializeField, Header("NormalBullet")] GameObject prefabs;
    [SerializeField, Header("������"), Tooltip("createNum��ڂ̐������͐������]�����ɕ���")] int createNum;
    int count = 0;
    [SerializeField, Header("�N�[���^�C��")] float coolTime;
    [SerializeField, Header("������"), Tooltip("������������������ ���͂�1�ȏ�")] int way = 1;

    [SerializeField, Header("����ɂ����鎞�ԁi�b�j")] float rotateTime;

    [SerializeField, Header("���󂵂Ă����Ƃ��̃N�[���^�C��")] float crumbleCoolTime;
    [SerializeField, Header("��������m��")] int fallProbability;
    bool isFall;
    [SerializeField, Header("����Ȃ��痎�����Ă����ꍇ�̗����X�s�[�h")] float fallSpeed;


    [SerializeField, Header("�t��]���邩�ǂ���")] bool isReverse;

    NormalBullet normalBullet;

    List<Data> posList = new();
    List<GameObject> bullets = new();

    public bool isCreate = false;
    bool tmp = false;
    int tmpCount = 0;

    bool isRotate = false;
    bool isCrumble = false;

    GameObject center;

    //�f�[�^�֌W
    [SerializeField, Header("���W�������Ă���csv���w��")] private AssetReference csvData;
    TextAsset text = null;
    bool isInput = false;

    Canvas canvas;
    RectTransform rt;
    Vector3 pos;
    TransformChange tc;

    private void Awake()
    {
        tc = gameObject.AddComponent<TransformChange>();
        normalBullet = prefabs.GetComponent<NormalBullet>();
        normalBullet.speed = 0;

        if (way < 1) { way = 1; } //�w�肳�ꂽway��1�����̂Ƃ��A1�ɂ���

        AsyncOperationHandle handle = csvData.LoadAssetAsync<TextAsset>();
        handle.Completed += OnCompletedHandler;

        canvas = GameObject.FindWithTag("Canvas").GetComponent<Canvas>();
        rt = GetComponent<RectTransform>();
    }

    void Update()
    {
        pos = tc.PositionChange(rt, canvas);

        if(!isCreate) { tmpCount = 0; }
        if (isCreate && isInput)
        {
            tmpCount++;
            if (tmpCount == 1)
            {
                tmp = true;
            }
        }

        if (tmp)
        {
            count++;
            tmp = false;
            StartCoroutine(Create());
        }

        if (isRotate && center)
        {
            if(!isReverse)
            {
                center.transform.Rotate(0, 0, (360f / rotateTime) * Time.deltaTime * -1);
            }
            else
            {
                center.transform.Rotate(0, 0, (360f / rotateTime) * Time.deltaTime * 1);
            }
        }

        if (isCrumble)
        {
            isCrumble = false;
            StartCoroutine(Crumble());
        }
    }

    void DataLoad()
    {
        var s = text;
        var lineSplit = s.text.Split("\n"); //�s���Ƃɕ���
        for (var i = 0; i < lineSplit.Length; i++)
        {
            var line = lineSplit[i].Split(",");

            int n = 0;
            if(int.TryParse(line[0] , out n))
            {
                Data data = new();

                data.num = int.Parse(line[0]);
                data.pos = new Vector3(float.Parse(line[1]), float.Parse(line[2]), float.Parse(line[3]));
                posList.Add(data);
            }
        }

        posList.Sort((a, b) => a.num - b.num);

        isInput = true;
    }

    private void OnCompletedHandler(AsyncOperationHandle obj)
    {
        if (obj.Status == AsyncOperationStatus.Succeeded)
        {
            TextAsset loadedCsv = csvData.Asset as TextAsset;
            if (loadedCsv != null)
            {
                text = loadedCsv;
                DataLoad();
            }
        }
        else
        {
            Debug.LogError($"AssetReference {csvData.RuntimeKey} failed to load.");
        }
    }

    IEnumerator Create()
    {
        center = Instantiate(prefabs, pos, Quaternion.identity);
        for (int i = 1; i <= posList[posList.Count - 1].num; i++)
        {
            foreach(var data in posList)
            {
                if(data.num == i)
                {
                    Vector3 p = data.pos * 10;
                    for(int w = 1; w <= way; w++)
                    {
                        Vector3 dir = new Vector3(1, 1, 0);

                        switch (w)
                        {
                            case 1:
                                if (isReverse) { dir = new Vector3(1, -1, 0); }
                                else
                                    { dir = new Vector3(1, 1, 0); }
                                break;
                            case 2:
                                if(isReverse) { dir = new Vector3(-1, 1, 0); }
                                else
                                { dir = new Vector3(-1, -1, 0); }
                                break;
                            case 3:
                                if(isReverse) { dir = new Vector3(1, -1, 0); }
                                else
                                { dir = new Vector3(-1, 1, 0); }
                                break;
                            case 4:
                                if(isReverse) { dir = new Vector3(-1, 1, 0); }
                                else { dir = new Vector3(1, -1, 0); }
                                break;
                        }

                        Vector3 position = p;
                        Vector3 position2 = new Vector3(position.x * dir.x , position.y * dir.y, 0);

                        GameObject obj = Instantiate(prefabs, position2 + pos, Quaternion.identity, center.transform);
                        obj.transform.localScale = new Vector3(1, 1, 1);
                        obj.GetComponent<NormalBullet>().num = data.num;
                        bullets.Add(obj);
                    }
                }
            }

            yield return new WaitForSeconds(coolTime);
        }
 

        if (count < createNum)
        {
            isRotate = true;
            isCrumble = true;
        }
        else if (count >= createNum)
        {
            isRotate = false;
            fallProbability = 100;
            isCrumble = true;
        }
    }

    IEnumerator Crumble()
    {
        for (int i = posList[posList.Count - 1].num; i > 0; i--)
        {
            if(bullets != null)
            {
                foreach (var o in bullets)
                {
                    if(o)
                    {
                        if (o.GetComponent<NormalBullet>().num == i)
                        {
                            var ran = Random.Range(0, 100);
                            if(ran < fallProbability) { isFall = true; }
                            else { isFall = false; }

                            if (isFall)
                            {
                                o.gameObject.transform.parent = null;
                                Vector3 rotate = o.GetComponent<Transform>().localEulerAngles;
                                rotate = new Vector3(0, 0, 0);
                                o.GetComponent<Transform>().localEulerAngles = rotate;
                                o.GetComponent<NormalBullet>().angle = -90;
                                o.GetComponent<NormalBullet>().speed = fallSpeed;
                            }
                            else
                            {
                                Destroy(o);
                            }
                        }
                    }
                }
            }
            
            if(i <= 1) { Destroy(center); }
            yield return new WaitForSeconds(crumbleCoolTime);
        }

        isRotate = false;
        yield return null;
    }
}
