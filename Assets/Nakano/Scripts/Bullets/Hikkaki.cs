using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class Hikkaki : MonoBehaviour
{
    [SerializeField, Header("NormalBullet")] GameObject prefabs;

    [SerializeField, Header("上段　生成数")] int upNum;
    [SerializeField, Header("中段　生成数")] int middleNum;
    [SerializeField, Header("下段　生成数")] int downNum;

    [SerializeField, Header("弾速")] float speed;

    NormalBullet normalBullet;

    public bool isCreate = false;
    bool tmp = false;
    int count = 0;

    List<Vector3> upPosList = new();
    List<Vector3> middlePosList = new();
    List<Vector3> downPosList = new();

    [SerializeField, Header("座標が入っているcsvを指定")] private AssetReference csvData;
    TextAsset text = null;
    bool isInput = false;

    void Awake()
    {
        normalBullet = prefabs.GetComponent<NormalBullet>();
        normalBullet.speed = speed;

        AsyncOperationHandle handle = csvData.LoadAssetAsync<TextAsset>();
        handle.Completed += OnCompletedHandler;
    }

    private void Start()
    {
    }

    void Update()
    {
        if(isInput)
        {
            if (isCreate)
            {
                isCreate = false;
                count++;
                if (count == 1)
                {
                    tmp = true;
                }
            }
            else { count = 0; }

            if (tmp)
            {
                tmp = false;
                StartCoroutine(Create());
            }
        }
    }

    void DataLoad()
    {
        var split = new List<string>();
        var s = text;
        var lineSplit = s.text.Split("\n"); //行ごとに分割
        for(var i = 0; i < lineSplit.Length; i++)
        {
            var line = lineSplit[i].Split(",");

            int n = 0;
            if (int.TryParse(line[0], out n))
            {
                switch (int.Parse(line[0]))
                {
                    case 1:
                        upPosList.Add(new Vector3(float.Parse(line[1]), float.Parse(line[2]), float.Parse(line[3])));
                        break;
                    case 2:
                        middlePosList.Add(new Vector3(float.Parse(line[1]), float.Parse(line[2]), float.Parse(line[3])));
                        break;
                    case 3:
                        downPosList.Add(new Vector3(float.Parse(line[1]), float.Parse(line[2]), float.Parse(line[3])));
                        break;
                }
            }
        }

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
        for(int i = 0; i < upNum; i++)
        {
            Vector3 pos = upPosList[Random.Range(0, upPosList.Count)];
            GameObject obj = Instantiate(prefabs, pos, Quaternion.identity);
            obj.GetComponent<NormalBullet>().angle = Random.Range(0, 361);
        }

        for (int i = 0; i < middleNum; i++)
        {
            Vector3 pos = middlePosList[Random.Range(0, middlePosList.Count)];
            GameObject obj = Instantiate(prefabs, pos, Quaternion.identity);
            obj.GetComponent<NormalBullet>().angle = Random.Range(0, 361);
        }

        for (int i = 0; i < downNum; i++)
        {
            Vector3 pos = downPosList[Random.Range(0, downPosList.Count)];
            GameObject obj = Instantiate(prefabs, pos, Quaternion.identity);
            obj.GetComponent<NormalBullet>().angle = Random.Range(0, 361);
        }

        yield return new WaitForEndOfFrame();
    }
}
