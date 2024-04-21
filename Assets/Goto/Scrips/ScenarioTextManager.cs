using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class ScenarioTextManager : MonoBehaviour
{

    [SerializeField, Header("読み込むCSVファイル")]
    TextAsset loadCsvFile;

    [SerializeField,Header("イベントID")]
    string eventID;

    List<string> textMessage = new List<string>();
    List<string> skillName = new List<string>();

    public List<string> TextSkillMessage => textMessage;
    public List<string> SkillName => skillName;


    private void Start()
    {
        string[][] loadCsv = ConvertCSVtoArray(loadCsvFile.text);

        for(int i = 0; i < loadCsv.Length; i++)
        {
            skillName.Add(loadCsv[i][0]);
            textMessage.Add(loadCsv[i][1]);
        }
    }

    private string[][] ConvertCSVtoArray(string s)
    {
        StringReader reader = new StringReader(s);

        List<string[]> rows = new List<string[]>();
        while (reader.Peek() >= 0)
        {
            string line = reader.ReadLine();        
            string[] elements = line.Split(',');
            for (int i = 0; i < elements.Length; i++)
            {
                elements[i] = elements[i].TrimStart('"').TrimEnd('"');
            }
            rows.Add(elements);
        }
        return rows.ToArray();
    }
}