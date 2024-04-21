using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class TipsData
{
    public int ID;
    public string text;
}

public class Tips : MonoBehaviour
{
    //データロード
    [SerializeField] private AssetReference csvData;
    TextAsset text = null;
    bool isInput = false;
    List<TipsData> tipsList = new();

    //表示
    Text tips;
    Animator changeAnim;
    bool textIn = true;

    [SerializeField, Header("テキストを変える間隔")] float changeTime;
    float time = 0;

    void Awake()
    {
        AsyncOperationHandle handle = csvData.LoadAssetAsync<TextAsset>();
        handle.Completed += OnCompletedHandler;

        tips = GetComponent<Text>();
        changeAnim = GetComponent<Animator>();

        changeAnim.SetBool("Start", false);
    }

    void Update()
    {
        if(isInput)
        {
            if(textIn)
            {
                TextChange();
                changeAnim.SetBool("Start", true);
                textIn = false;
            }

            time += Time.deltaTime;
            if(time >= changeTime)
            {
                TextOut();
                time = 0;
            }
        }
    }

    void TextOut()
    {
        changeAnim.SetTrigger("Change");
    }

    void TextChange()
    {
        string t = tipsList[Random.Range(0, tipsList.Count)].text;

        if (t.Contains("<NewLine>"))
        {
            t = t.Replace("<NewLine>", "\n");
        }

        tips.text = t;
    }

    void DataLoad()
    {
        var s = text;
        var lineSplit = s.text.Split("\n"); //行ごとに分割
        for (var i = 0; i < lineSplit.Length; i++)
        {
            var line = lineSplit[i].Split(",");

            int n = 0;
            if (int.TryParse(line[0], out n))
            {
                TipsData data = new();

                data.ID = int.Parse(line[0]);
                data.text = line[1];
                tipsList.Add(data);
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
}
