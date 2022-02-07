using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 此脚本挂载在CardStore上，用于实现点击按钮时发牌的功能
/// 点击Open Package按钮时，生成五张卡片放在屏幕中
/// 这五张卡牌会从之前定义好的预制件（Prefabs）中调用
/// </summary>
public class OpenPackage : MonoBehaviour
{
    public GameObject cardPrefab;  // 在Unity中，需要把Card预制件挂载给这个cadPrefab
    public GameObject cardPool;  // 卡池，Scene里也有个cardPool
                                 // 在unity中，需要把CardPool挂载给此处这个cardPool
                                 // 这就实现了物体之间的通信（是不是要专门总结记忆一下这点）

    CardStore CardStore;  // 不需要是public 
    // 这个对象一被定义出来就拥有了完整的卡组信息,但，也不是在这里就拥有的，这里只是个引用变量罢了，要铭记C#的特性
    // 详细说明见CardStore.cs

    List<GameObject> cards = new List<GameObject>();  // 用于清空卡池的临时链表

    /// <summary>
    /// 视频中写成"PlayerData"
    /// </summary>
    public PlayerData playerData;








    // Start is called before the first frame update
    void Start()
    {
        CardStore = GetComponent<CardStore>();   // 由于CardStore和OpenPackage是在同一个物体下，所以直接获取就可以直接拿到CardStore
        // 想想这里得GetComponent干了什么？
        // 大概是用于物体间通信的？
        // 把Cardstore类的实例读到这里的OpenPackage（的CardStore成员）里？
    }

    // Update is called once per frame
    void Update()
    {
        
    }











    public void OnClickOpen()
    {
        if (playerData.playerCoins < 2)  // 如果玩家的金币数不够，就不能开卡包
        {
            Debug.Log("金币不够，不能开卡包");
            return;
        }
        else
        {
            playerData.playerCoins -= 2;
        }
        ClearPool();  // 每次点击按钮前都先清空卡池
        for (int i = 0; i < 5; i++)
        {
            GameObject newCard = GameObject.Instantiate(cardPrefab, cardPool.transform);  // 这样cardPrefab都会生成到cardPool.transform
            // cardPrefab是卡牌的预制件

            // 调用CardStore类中的RandowCard()方法：从CardStore中的cardList中随机获取一张牌
            newCard.GetComponent<CardDisplay>().card = CardStore.RandomCard();  // 这里还有一层：获取一张随机排然后赋给newCard的卡牌信息
                                                                                // 虽然这句话很复杂，但概括下就干了这么件事情


           // 这步是为了实现清空卡池而添加的
           cards.Add(newCard);  // 新生成的卡牌添加到链表这个为了清空卡池而设置的临时链表中
        }
        SaveCardData();  // 把抽到的卡保存到playerData
        playerData.SavePlayerData();  // 保存玩家数据到PlayerData.csv
        Debug.Log("剩余金币：" + playerData.playerCoins + " 枚");
    }

    /// <summary>
    /// 清空卡池
    /// </summary>
    public void ClearPool()
    {
        foreach (var card in cards)  // 销毁卡池中的每一张卡
        {
            Destroy(card);
        }
        cards.Clear();
    }

    /// <summary>
    /// 把抽到的卡保存到playerData
    /// </summary>
    public void SaveCardData()
    {
        foreach (var card in cards)
        {
            int id = card.GetComponent<CardDisplay>().card.id;  // 获取这张卡的id
            playerData.playerCards[id] += 1;  // 玩家拥有该卡的数量增加1
        }
    }
}
