﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;  // 文件读写库

/// <summary>
/// 挂载在名为"PlayerData"的空对象(Create Empty)上
/// </summary>

public class PlayerData : MonoBehaviour
{
    /// <summary>
    /// 视频中是"CardStore"，我写成"cardStore"了，防止误解
    /// </summary>
    public CardStore cardStore;

    /// <summary>
    /// 玩家拥有金币的数量
    /// </summary>
    [HideInInspector]  // 在unity组件面板中隐藏个变量
    public int playerCoins;

    /// <summary>
    /// 玩家拥有的每种卡牌的数量
    /// <para>下标与卡牌编号对应，数组项存的值表示玩家拥有《该下标对应的卡牌》的数量</para>
    /// </summary>
    public int[] playerCards;

    /// <summary>
    /// 从PlayerData.csv读入的东西
    /// </summary>
    public TextAsset playerData;















    // Start is called before the first frame update
    void Start()
    {
        cardStore.LoadCardData();  // 卡组信息要比玩家信息先一步载入
        LoadPlayerData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }










    /// <summary>
    /// 加在玩家数据，在Start中调用 
    /// </summary>
    public void LoadPlayerData()
    {
        playerCards = new int[cardStore.cardList.Count];  // 确定数组长度为卡牌的种数
        string[] dataRow = playerData.text.Split('\n');   // 数组的每一项是PlayerData.csv中的一行
        foreach (var row in dataRow)
        {
            string[] rowArray = row.Split(',');  // 把每一行按','分隔
            if (rowArray[0] == "#")  // 忽略以"#"开头的行
                continue;
            else if(rowArray[0] == "coins")  // 载入硬币数量
            {
                playerCoins = int.Parse(rowArray[1]);
            }
            else if (rowArray[0] == "card")  // 载入玩家拥有卡牌的数量
            {
                int id = int.Parse(rowArray[1]);
                int num = int.Parse(rowArray[2]);
                playerCards[id] = num;
            }
        }
        Debug.Log("玩家信息已载入");
        Debug.Log(playerCoins.ToString());
    }

    /// <summary>
    /// 保存玩家数据到PlayerData.csv
    /// </summary>
    public void SavePlayerData()
    {
        string path = Application.dataPath + "/Datas/PlayerData.csv";  // 文件存储路径（视频里写成"playerdata.csv"了）

        List<string> datas = new List<string>();  // 要保存到PlayerData.csv的内容
        datas.Add("coins," + playerCoins.ToString());  // 把玩家硬币数量信息保存为PlayerData.csv的第一行
        for(int i = 0; i < playerCards.Length; i++)  // 把每种卡都循环一遍，把每一张牌（拥有的数量不是0的）的信息都加入到PlayerData.csv中
        {
            if (playerCards[i] != 0)
                datas.Add("card," + i.ToString() + "," + playerCards[i].ToString());
        }

        // 保存卡组（下一期视频（P5）再进行）

        File.WriteAllLines(path, datas);  // 保存数据到PlayerData.csv
    }
}
