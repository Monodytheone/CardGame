using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    /// <summary>
    /// 卡组显示区域
    /// <para>挂载"deck grid"</para>>
    /// <para>Transform类型比GameObject更详细一点，代表这个物体的坐标、缩放、旋转之类的信息</para>
    /// </summary>
    public Transform deckPanel;

    /// <summary>
    /// 显示玩家卡牌仓库的区域 
    /// <para>挂载"library grid"</para>
    /// <para>Transform类型比GameObject更详细一点，代表这个物体的坐标、缩放、旋转之类的信息</para>
    /// </summary>
    public Transform libraryPanel;

    /// <summary>
    /// 挂"DeckCard"
    /// </summary>
    public GameObject deckPrefab;

    /// <summary>
    /// 挂"LibraryCard"
    /// </summary>
    public GameObject cardPrefab;

    public GameObject DataManager;  // 得找下规律为啥up刻意把一些变量开头大写，是因为开头大写的都是脚本吗？（比如下面两个）
                                    // 虽然Up说的是脚本，不过其实好像是Scene里有的物体就要大写

    // 下面两个变量是在代码里获取的，不需要是public的
    private PlayerData PlayerData;
    private CardStore CardStore;








    // Start is called before the first frame update
    void Start()
    {
        // 获取这两个脚本
        PlayerData = DataManager.GetComponent<PlayerData>();
        CardStore = DataManager.GetComponent<CardStore>();

        UpdateLibrary();
        UpdateDeck();
    }

    // Update is called once per frame
    void Update()
    {
        
    }







    /// <summary>
    /// 更新卡牌仓库中的显示
    /// </summary>
    public void UpdateLibrary()
    {
        for (int i = 0; i < PlayerData.playerCards.Length; i++)
        {
            // 如果一种卡的数量不是0，就在libraryPanel中生成一个
            if (PlayerData.playerCards[i] > 0)
            {
                GameObject newCard = Instantiate(cardPrefab, libraryPanel);
                newCard.GetComponent<CardCounter>().counter.text = PlayerData.playerCards[i].ToString();
                newCard.GetComponent<CardDisplay>().card = CardStore.cardList[i];
            }

        }
    }

    /// <summary>
    /// 更新卡组中的显示
    /// </summary>
    public void UpdateDeck()
    {
        for (int i = 0; i < PlayerData.playerDeck.Length; i++)
        {
            if (PlayerData.playerDeck[i] > 0)
            {
                GameObject newCard = Instantiate(deckPrefab, deckPanel);
                newCard.GetComponent<CardCounter>().counter.text = PlayerData.playerDeck[i].ToString();
                newCard.GetComponent<CardDisplay>().card = CardStore.cardList[i];  
            }
        }
    }
}
