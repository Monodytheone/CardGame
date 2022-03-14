using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
    /// 生成卡的预制件(挂BallteCard)
    /// </summary>
    public GameObject cardPrefab;

    /// <summary>
    /// 游戏阶段更改事件
    /// </summary>
    public UnityEvent phaseChangeEvent = new UnityEvent();

    /// <summary>
    /// 可召唤的最大次数
    /// <para>index_0: player； index_1: enemy</para>
    /// </summary>
    public int[] SummonCountMax = new int[2];
    /// <summary>
    /// 当前可召唤次数
    /// <para>index_0: player;  index_1: enemy</para>
    /// </summary>
    private int[] SummonCounter = new int[2];

    /// <summary>
    /// 召唤时用来暂存等待召唤的怪兽卡
    /// </summary>
    private GameObject waitingMonster;
    /// <summary>
    /// 暂存发起召唤的玩家
    /// <para>0: player, 1: enemy</para>
    /// </summary>
    private int waitingPlayer;

    /// <summary>
    /// 箭头预制件
    /// </summary>
    public GameObject ArrowPrefab;
    /// <summary>
    /// 场景中可能存在的箭头
    /// </summary>
    private GameObject arrow;








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
    /// 游戏开始时执行的读取数据、洗牌、抽牌操作
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

        // 游戏开始时，将双方的可用召唤次数置为最大值
        for(int i = 0; i < SummonCounter.Length; i++)
        {
            SummonCounter[i] = SummonCountMax[i];
        }

        GamePhase = GamePhase.playerDraw;  // 进入玩家抽卡阶段
        phaseChangeEvent.Invoke();
    }

    /// <summary>
    /// 点击DrawCard时，player抽卡
    /// </summary>
    public void OnPlayerDraw()
    {
        if (GamePhase == GamePhase.playerDraw)
        {
            DrawCard(0, 1);
            //GamePhase = GamePhase.playerAction;
            //phaseChangeEvent.Invoke();
            NextPhase();
        }
        else if (GamePhase == GamePhase.enemyDraw)
        {
            DrawCard(1, 1);
            //GamePhase = GamePhase.enemyAction;
            //phaseChangeEvent.Invoke();
            NextPhase();
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
            newCard.GetComponent<BattleCard>().playerID = _player;  // 将卡牌所有者ID设为相应值
            newCard.GetComponent<BattleCard>().state = BattleCardState.inHand;  // 将卡牌状态设为：“在手牌区”
            targetDeckList.RemoveAt(0);
        }


    }


    /// <summary>
    /// 点击Turn End时
    /// </summary>
    public void OnClikcTurnEnd()  // 这样的写法是更规范的，有时候会希望TurnEnd()是private的
    {
        //TurnEnd();
        NextPhase();
    }

    /// <summary>
    /// 用于玩家或敌人回合结束时切换回合
    /// </summary>
    //public void TurnEnd()
    //{
    //    // 若玩家行动回合结束，则跳转到敌人抽卡
    //    if(GamePhase == GamePhase.playerAction)
    //    {
    //        GamePhase = GamePhase.enemyDraw;
    //        Debug.Log("TurnEnd()");
    //    }
    //    else if(GamePhase == GamePhase.enemyAction)  // 若敌人行动回合结束，则跳转到玩家抽卡阶段
    //    {
    //        GamePhase = GamePhase.playerDraw;
    //        Debug.Log("TurnEnd()");
    //    }
    //    else if(GamePhase == GamePhase.playerDraw)
    //    {
    //        GamePhase = GamePhase.playerAction;
    //    }
    //    else if(GamePhase == GamePhase.enemyDraw)
    //    {
    //        GamePhase = GamePhase.enemyAction;
    //    }
    //    phaseChangeEvent.Invoke();
    //}

    /// <summary>
    /// 用于进入下一阶段
    /// </summary>
    private void NextPhase()
    {
        Debug.Log("PhaseChange()");
        //if (GamePhase == GamePhase.enemyAction)
        if((int)GamePhase == System.Enum.GetNames(GamePhase.GetType()).Length - 1)
        {
            GamePhase = GamePhase.playerDraw;
        }
        else
            GamePhase++;  // 进入下一阶段   

        // 关闭所有格子的高亮
        foreach (var block in playerBlocks)
        {
            block.GetComponent<Block>().SummonBlock.SetActive(false);
        }
        foreach (var block in enemyBlocks)
        {
            block.GetComponent<Block>().SummonBlock.SetActive(false);
        }

        phaseChangeEvent.Invoke();
    }




    /// <summary>
    /// 召唤请求
    /// <para>用于卡牌被点击时，发起召唤请求，开始等待点击格子</para>
    /// </summary>
    /// <param name="_player">0表示玩家，1表示敌人</param>
    /// <param name="_monster">要召唤的怪兽卡</param>
    public void SummonRequest(int _player, GameObject _monster)  // 疑问：不需要返回一下召唤请求是否成功嘛？
    {
        GameObject[] blocks = new GameObject[1];
        bool hasEmptyBlock = false;  // 是否具有可召唤到的空格子
        if(_player == 0 && GamePhase == GamePhase.playerAction)
        {
            blocks = playerBlocks;
        }
        else if (_player == 1 && GamePhase == GamePhase.enemyAction)
        {
            blocks = enemyBlocks;
        }
        else
        {
            Debug.Log("不可召唤");
            return;
        }
        if (SummonCounter[_player] > 0)  // 还有剩余召唤次数时，才可召唤
        {
            foreach (var block in blocks)  // 还有空格子时，才可召唤
            {
                if (block.GetComponent<Block>().card == null)
                {
                    Debug.Log("空格子");
                    // 还要对所有等待召唤（空）的格子进行高亮显示
                    block.GetComponent<Block>().SummonBlock.SetActive(true);  // 高亮显示

                    hasEmptyBlock = true;
                }
            }
        }
        else
        {
            Debug.Log("可召唤次数为0");
        }
        if (hasEmptyBlock)
        {
            waitingMonster = _monster;
            waitingPlayer = _player;
            CreateArrow(_monster.transform, ArrowPrefab);
            Debug.Log("召唤请求通过");
        }
    }

    /// <summary>
    /// 召唤确认
    /// <para>用于格子被点击时，确认召唤，准备执行召唤</para>
    /// <para>为何不能点击格子后直接执行召唤呢？</para>
    /// <para>   答：若敌人为Ai控制，自然无法点击格子，需要让Ai直接能调用Summon进行召唤</para>
    /// </summary>
    /// <param name="_block">要召唤到的格子</param>
    public void SummonConfirm(Transform _block)
    {
        // 具体的召唤确认在Block里实现
        Summon(waitingPlayer, waitingMonster, _block);  // 执行召唤

        // 执行召唤之后要关闭格子的高亮，由于格子只有在格子高亮时才可召唤，故关闭高亮后格子就不可作为召唤目标位置了
        GameObject[] blocks = playerBlocks;
        if(waitingPlayer == 1)
        {
            blocks = enemyBlocks;
        }
        foreach (var block in blocks)
        {
            block.GetComponent<Block>().SummonBlock.SetActive(false);  // 关闭高亮
            // 由于格子只有在格子高亮时才可召唤，故关闭高亮后格子就不可作为召唤目标位置了
        }
    }

    /// <summary>
    /// 执行召唤(是直接更改坐标移过去了)
    /// </summary>
    /// <param name="_player">召唤的玩家</param>
    /// <param name="_monster">要召唤的怪兽</param>
    /// <param name="_block">要召唤到的格子</param>
    public void Summon(int _player, GameObject _monster, Transform _block)
    {
        _monster.transform.SetParent(_block);
        _monster.transform.localPosition = Vector3.zero;  // 这两步将_monster的坐标设为与_block一致

        _monster.GetComponent<BattleCard>().state = BattleCardState.inBlock;  // 卡牌状态设为在场上（格子里）
        _block.GetComponent<Block>().card = _monster;

        SummonCounter[_player]--;  // 当前玩家的召唤次数-1
    }

    /// <summary>
    /// 用于召唤请求时（未来可能魔法卡也用?）生成箭头
    /// </summary>
    /// <param name="_startPoint">箭头的起始点</param>
    /// <param name="_prefab">箭头预制件</param>
    public void CreateArrow(Transform _startPoint, GameObject _prefab)
    {
        DestroyArrow();
        arrow = GameObject.Instantiate(_prefab, _startPoint);  // 创建箭头
        // 赋予起始点的值
        arrow.GetComponent<Arrow>().SetStartPoint(new Vector2(_startPoint.position.x, _startPoint.position.y));
        

    }

    /// <summary>
    /// 销毁箭头
    /// </summary>
    public void DestroyArrow()
    {
        Destroy(arrow);
    }
}
