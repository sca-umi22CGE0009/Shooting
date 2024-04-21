using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

/// <summary>
/// 座標をCSVファイルに書きこむ
/// </summary>
public class CSVController : MonoBehaviour
{
    [SerializeField, Header("trueのとき位置調整モードになり、座標データを書き出す")] private bool isEdit = false;
    [SerializeField, Header("CSVファイルを作るフォルダのパス")] string path = "/Nakano/PositionData/hikkaki.csv";

    GameObject[] posEdit;

    private void Awake()
    {
        if (isEdit)
        {
            if(File.Exists(Application.dataPath + path))
            {
                StreamWriter writer;

                string s2 = "";

                string fileName = Application.dataPath + path;
                writer = new StreamWriter(fileName, false);
                writer.WriteLine(s2);
                writer.Flush();
                writer.Close();
            }
            
            posEdit = GameObject.FindGameObjectsWithTag("PosEdit");
            foreach (GameObject obj in posEdit)
            {
                PositionEdit pos = obj.GetComponent<PositionEdit>();
                savePosition(path, pos.num, pos.gameObject.transform.position);
            }
        }
    }

    /// <summary>
    /// 座標をCSVファイルに入れる
    /// </summary>
    /// <param name="path"></param>
    /// <param name="posNum"></param>
    /// <param name="pos"></param>
    public void savePosition(string path, int posNum, Vector3 pos)
    {
        StreamWriter writer;

        string[] s = {posNum.ToString(), (pos.x).ToString(), (pos.y).ToString(), (pos.z).ToString()};
        string s2 = string.Join(",", s);

        string fileName = Application.dataPath + path;
        writer = new StreamWriter(fileName, true);
        writer.WriteLine(s2);
        writer.Flush();
        writer.Close();
    }
}
