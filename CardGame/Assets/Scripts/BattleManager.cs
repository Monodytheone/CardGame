using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 表示游戏阶段的枚举
/// </summary>
public enum GamePhase
{
    /// <summary>
    /// 游戏开始阶段
    /// </summary>
    gameStart,

    /// <summary>
    /// 玩家抽卡阶段
    /// </summary>
    playerDraw,

    /// <summary>
    /// 玩家行动阶段
    /// </summary>
    playerAction,

    /// <summary>
    /// 敌人抽卡阶段
    /// </summary>
    enemyDraw,

    /// <summary>
    /// 敌人行动阶段
    /// </summary>
    enemyAction,

}

/// <summary>
/// 直接继承单例模板，
/// 这样BattleManager就是一个继承了MonoBehaviour的单例类
/// </summary>
public class BattleManager : MonoSingleton<BattleManager>  // 直接继承单例模板，这样BattleManager就是一个继承了MonoBehaviour的单例类
{
    //public static BattleManager instance;

    public PlayerData playerData;
    public PlayerData enemyData;  // 数据

    public List<Card> playerDeckList = new List<Card> ();
    public List<Card> enemyDeckList = new List<Card>();  // 卡组

    /// <summary>
    /// 玩家手牌区
    /// </summary>
    public Transform playerHand;
    /// <summary>
    /// 敌人手牌区
    /// </summary>
    public Transform enemyHand;

    /// <summary>
    /// 玩家的三个格子
    /// </summary>
    public GameObject[] playerBlocks;
    /// <summary>
    /// 敌人的三个格子
    /// </summary>
    public GameObject[] enemyBlocks;

    /// <summary>
    /// 玩家头像
    /// </summary>
    public GameObject playerIcon;
    /// <summary>
    /// 
    /// </summary>
    public GameObject enemyIcon;

    /// <summary>
    /// 游戏阶段(初始默认为gameStart)
    /// </summary>
    public GamePhase GamePhase = GamePhase.gameStart;  // 该值现在是可以在Inspector里选择的，以后需要看看是否需要把此成员隐藏起来


    /// <summary>
    /// 生成卡的预制件(暂时先挂Card1，若还需要新的功能就制作新的卡牌预制件)
    /// </summary>
    public GameObject cardPrefab;









    private void Awake()
    {
        //Instance = this;

    }

    // Start is called before the first frame update
    void Start()
    {
        GameStart();
    }

    // Update is called once per frame
    void Update()
    {
        
    }








    /// 游戏流程：
    /// 开始游戏：加载数据、卡组洗牌、初始手牌
    /// 回合结束、游戏阶段
    //
    
    /// <summary>
    /// 
    /// </summary>
    public void GameStart()
    {
        /// 1. 读取数据
        /// 2. 卡组洗牌
        /// 3. 玩家抽卡5，敌人抽卡5 (从卡组里抽)
        /// 
        /// 一件我现在明白但害怕以后不明白的事：
        ///     开卡包的时候不就已经是随机了的吗？为什么还要洗牌呢？
        ///     答：
        ///         开卡包是随机往卡库(library)里添五张牌，而卡组(deck)是由玩家自己从卡库里选择的，故需要再洗
        ///         

        ReadDeck();  // 载入卡组

        ShuffletDeck(0);
        ShuffletDeck(1);  // 洗牌

        DrawCard(0, 3);
        DrawCard(1, 3);  // 玩家和敌人从各自的卡组抽5张牌，生成到各自的手牌区


        GamePhase = GamePhase.playerDraw;  // 进入玩家抽卡阶段
    }

    /// <summary>
    /// 点击DrawCard时，player抽卡
    /// </summary>
    public void OnPlayerDraw()
    {
        if (GamePhase == GamePhase.playerDraw)
        {
            DrawCard(0, 1);
            GamePhase = GamePhase.playerAction;
        }
        else if (GamePhase == GamePhase.enemyDraw)
        {
            DrawCard(1, 1);
            GamePhase = GamePhase.enemyAction;
        }
        else
        {
            Debug.Log("现在不能抽卡");
        }
    }

    //public void OnPlayerDraw()
    //{
    //    if (GamePhase == GamePhase.playerDraw)
    //    {
    //        DrawCard(0, 1);  // 目前参数只抽了一张
    //        GamePhase = GamePhase.playerAction;  // 进入玩家行动阶段
    //    }
    //}

    ///// <summary>
    ///// 敌人抽卡
    ///// </summary>
    //public void EnemyDraw()
    //{
    //    if (GamePhase == GamePhase.enemyDraw)
    //    {
    //        DrawCard(1, 1);
    //        GamePhase = GamePhase.enemyAction;
    //    }
    //}

   
    /// <summary>
    /// 读取卡组
    /// </summary>
    public void ReadDeck()
    {
        // 加载玩家卡组
        for(int i = 0; i < playerData.playerDeck.Length; i++)
        {
            if(playerData.playerDeck[i] != 0)
            {
                int count = playerData.playerDeck[i];
                for (int j = 0; j < count; j++)
                {
                    playerDeckList.Add(playerData.cardStore.CopyCard(i));  // new一张该id对应的卡，加到playerDeckList中
                }
            }
        }

        // 加载敌人卡组
        for (int i = 0; i < enemyData.playerDeck.Length; i++)
        {
            if(enemyData.playerDeck[i] != 0)
            {
                int count = enemyData.playerDeck[i];
                for(int j = 0; j < count; j++)
                {
                    enemyDeckList.Add(enemyData.cardStore.CopyCard(i));
                }
            }
        }

    }

    /// <summary>
    /// 卡组洗牌
    /// </summary>
    /// <param name="_player">0为玩家，1为敌人</param>
    public void ShuffletDeck(int _player)
    {
        List<Card> shuffletDeck = new List<Card>();
        if(_player == 0)
        {
            shuffletDeck = playerDeckList;
        }
        else if(_player == 1)
        {
            shuffletDeck = enemyDeckList;
        }
        else
        {
            Debug.Log("洗牌错误");
        }

        // 遍历列表中的每一张卡，每张卡都与随机的另一张卡交换位置，
        // 如此实现洗牌的简单算法
        for(int i = 0;i < shuffletDeck.Count;i++)
        {
            int rad = Random.Range(0, shuffletDeck.Count);
            Card temp = shuffletDeck[i];
            shuffletDeck[i] = shuffletDeck[rad];
            shuffletDeck[rad] = temp;
        }
    }

    /// <summary>
    /// 从卡组抽卡(不随机，抽最上面的）
    /// </summary>
    /// <param name="_player">0表示玩家，1表示敌人</param>
    /// <param name="_count">抽卡的张数</param>
    public void DrawCard(int _player, int _count)
    {
        Transform targetHand = playerHand;  // 抽到的区域
        List<Card> targetDeckList = playerDeckList;  // 从哪个卡组抽卡
        if (_player == 1)
        {
            targetHand = enemyHand;
            targetDeckList = enemyDeckList;
        }

        for(int i = 0; i < _count; i++)
        {
            //int rad = Random.Range(0, targetDeckList.Count);
            //CreateHandCard(rad, _player);
            //targetDeckList.RemoveAt(rad);
            if(targetDeckList.Count == 0)
            {
                Debug.Log("卡组空了");
                break;
            }
            GameObject newCard = Instantiate(cardPrefab, targetHand);
            newCard.GetComponent<CardDisplay>().card = targetDeckList[0];
            targetDeckList.RemoveAt(0);
        }


    }


    /// <summary>
    /// 点击Turn End时
    /// </summary>
    public void OnClikcTurnEnd()  // 这样的写法是更规范的，有时候会希望TurnEnd()是private的
    {
        TurnEnd();
    }

    /// <summary>
    /// 用于玩家或敌人回合结束时切换回合
    /// </summary>
    public void TurnEnd()
    {
        // 若玩家行动回合结束，则跳转到敌人抽卡
        if(GamePhase == GamePhase.playerAction)
        {
            GamePhase = GamePhase.enemyDraw;
            Debug.Log("TurnEnd()");
        }
        else if(GamePhase == GamePhase.enemyAction)  // 若敌人行动回合结束，则跳转到玩家抽卡阶段
        {
            GamePhase = GamePhase.playerDraw;
            Debug.Log("TurnEnd()");
        }
        else if(GamePhase == GamePhase.playerDraw)
        {
            GamePhase = GamePhase.playerAction;
        }
        else if(GamePhase == GamePhase.enemyDraw)
        {
            GamePhase = GamePhase.enemyAction;
        }
    }
}
