using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 本脚本挂载在CardStore上
/// <para>在Start中调用本脚本里写的LoadCardData方法，用于从CardData.csv文件载入卡牌信息</para>
/// <para>CardStore还提供了一个RandowCard方法，随机返回一张卡，供OpenPackage调用</para>
/// </summary>


// 如果实例化一个该类的对象，cardList会自动由Unity中的挂载来绑定到.csv文件
// cardList则会通过  在游戏一开始就会调用的Start()方法中调用LoadCardData()方法，来把卡牌信息读到cardList里
// 这么一来，CardStore类的实例一被定义出来就拥有了完整的卡组信息
public class CardStore : MonoBehaviour
{
    /// <summary>
    /// 挂CardData.csv
    /// </summary>
    public TextAsset cardData;

    /// <summary>
    /// 存储卡组的列表
    /// </summary>
    public List<Card> cardList = new List<Card>();  // List的类型是父类"Card"，但里面可以放子类对象







    /// Start is called before the first frame update
    void Start()
    {
        //LoadCardData();  // 这个的调用放到PlayerData.cs里了

        //TestLoad();  // 游戏开始时，卡牌信息载入后进行测试
    }

    // Update is called once per frame
    void Update()
    {
        
    }





     
    public void LoadCardData()
    {
        Debug.Log("CardStore.LoadCardData()");
        string[] dataRow = cardData.text.Split('\n');  // 按照换行符来分割，分隔成每一行，存到dataRow中
        foreach (var row in dataRow)
        {
            string[] rowArray = row.Split(',');  // csv中，如果检测到开头是'#'，就会忽略这一项
            if (rowArray[0] == "#")
            {
                continue;  // 忽略以'#'开头的一整行
            }
            else if (rowArray[0] == "monster")
            {
                // 新建怪兽卡

                int id = int.Parse(rowArray[1]);
                string name = rowArray[2];
                int atk = int.Parse(rowArray[3]);
                int health = int.Parse(rowArray[4]);
                MonsterCard monsterCard = new MonsterCard(id, name, atk, health);  // 新建怪兽卡一张，用上面从.csv里读入的属性值初始化
                cardList.Add(monsterCard);  // 把新建的这张卡加入CardList

                //Debug.Log("读取到怪兽卡: " + monsterCard.cardName);  // 测试
            }
            else if(rowArray[0] == "spell")
            {
                // 新建魔法卡

                int id = int.Parse (rowArray[1]);
                string name = rowArray[2];
                string effect = rowArray[3];
                SpellCard spellCard = new SpellCard(id, name, effect);
                cardList.Add(spellCard);  // 把新建的这张魔法卡加入CardList
            }
        }
    }

    public void TestLoad()
    {
        foreach (Card card in cardList)
        {
            Debug.Log("类型: " + card.GetType().ToString() + "   编号: " + card.id.ToString() + "   卡名: " + card.cardName);
        }
    }


    /// <summary>
    /// 返回一张随机的牌（以Card类对象的形式返回信息）
    /// </summary>
    /// <returns>一张随机的牌</returns>
    public Card RandomCard()  // 
    {
        Card card = cardList[Random.Range(0, cardList.Count)];
        return card;
    }
}
