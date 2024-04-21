using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    public string stageName;
    
    public static Canvas canvas;
    public enum GameStat
    {
        START = 0,
        PLAY,
        DETH,
        //REPOP
    }
    [SerializeField]private GameObject player;
    public static GameStat game_stat;
    public static Vector3 startPos;
    public static bool isNewGame;
    public static bool isPlayerBroken;

    public GameObject Player
    {   
        get { return player;  }
        set { player = value; }
    }

    // Start is called before the first frame update
    void OnEnable()
    {
        canvas = GetComponent<Canvas>();
        Debug.Log(canvas.transform.position);
        isNewGame = true;

        if(player.name != "MagicSwordsMan")
        {
            startPos = new Vector3(0, -550, 0);
        }
        else
        {
            startPos = new Vector3(0, -400, 0);
        }
        game_stat = GameStat.START;
        isPlayerBroken = true;

        GameObject obj = Instantiate(player);
        obj.transform.SetParent(gameObject.transform);
//        GameObject obj = Instantiate(Player, /*startPos*/canvas.transform.position, Quaternion.identity, transform);
        RectTransform rect = obj.GetComponent<RectTransform>();
        rect.localScale = new Vector3(7f, 7f, 7f);
        rect.anchoredPosition3D = startPos;
       
    }

    // Update is called once per frame
    void Update()
    {

        if (game_stat==GameStat.START)
        {
            game_stat = GameStat.PLAY;
        }
        if (UIManager.HP<=0)
        {
            game_stat = GameStat.DETH;
        }
    }
}
