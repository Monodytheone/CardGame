using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 卡库(Library)和卡组(Deck)的显示
/// </summary>
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


    /// <summary>
    /// 卡库里,id与场景中创建的卡牌的对应关系
    /// </summary>
    private Dictionary<int, GameObject> libraryDic = new Dictionary<int, GameObject>();

    /// <summary>
    /// 卡组里,id与场景中创建的卡牌的对应关系
    /// </summary>
    private Dictionary<int, GameObject> deckDic = new Dictionary<int, GameObject>();








    // Start is called before the first frame update
    void Start()
    {
        ///经过debug，看起来原因应该是：
        ///PlayerData.Start()后于DeckManager.Start()执行，
        ///导致UpdateLibrary()被调用时，playerData并没有加载好
        // 获取这两个脚本                  
        //DataManager.GetComponent<PlayerData>().
        Debug.Log("DeckManager.Start()");
        PlayerData = DataManager.GetComponent<PlayerData>();
        CardStore = DataManager.GetComponent<CardStore>();

        //// 加了下面这两行，先于卡库和卡组的显示载入了CardStore和PlayerData，勉强能用了，但为啥原本什么都不做时也能正常运行呢？
        //CardStore.LoadCardData();
        ////Debug.Log("1111");
        //PlayerData.LoadPlayerData();
        
        // 我添加了脚本执行顺序，现在不用上面这两行了

        UpdateLibrary();
        UpdateDeck();

        /// 测试看来，PlayerData.Start()会在DeckManager.Start()整个执行完之后才被调用
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
        Debug.Log("Deckmanager.UpdateLibrary");
        Debug.Log("playerCards.Length = " + PlayerData.playerCards.Length.ToString());  // 鉴定： 为0了，所以不会进入循环
        for (int i = 0; i < PlayerData.playerCards.Length; i++)
        {
            // 如果一种卡的数量不是0，就在libraryPanel中生成一个
            if (PlayerData.playerCards[i] > 0)
            {
                //GameObject newCard = Instantiate(cardPrefab, libraryPanel);
                ////newCard.GetComponent<CardCounter>().counter.text = PlayerData.playerCards[i].ToString();
                //newCard.GetComponent<CardCounter>().SetCounter(PlayerData.playerCards[i]);
                //newCard.GetComponent<CardDisplay>().card = CardStore.cardList[i];
                //libraryDic.Add(i, newCard);

                CreatCard(i, CardState.Library);
                // libraryDic不用管了吗？
            }
            //Debug.Log("loop"); 

        }
    }

    /// <summary>
    /// (Scene开始时）更新卡组中的显示
    /// </summary>
    public void UpdateDeck()
    {
        Debug.Log("DeckManager.UpdateDeck()");
        for (int i = 0; i < PlayerData.playerDeck.Length; i++)
        {
            if (PlayerData.playerDeck[i] > 0)
            {
                //GameObject newCard = Instantiate(deckPrefab, deckPanel);
                ////newCard.GetComponent<CardCounter>().counter.text = PlayerData.playerDeck[i].ToString();
                //newCard.GetComponent<CardCounter>().SetCounter(PlayerData.playerDeck[i]);
                //newCard.GetComponent<CardDisplay>().card = CardStore.cardList[i];
                //deckDic.Add(i, newCard); 

                CreatCard(i, CardState.Deck);
            }
        }
    }

    /// <summary>
    /// （点击卡库or卡组中的牌时）更新卡牌
    /// <para>此方法可以根据卡牌的状态（所处位置），更新这张卡牌的显示，以及对应数据</para>
    /// <para>该方法已实现了卡牌数量、位置的更新，在ClickCard中只需调用即可</para>
    /// </summary>
    /// <param name="_state">点击的卡牌在library里还是deck里</param>
    /// <param name="_id"></param>
    public void UpdateCard(CardState _state, int _id)
    {
        if (_state == CardState.Deck)  // Deck中的卡移到Library中
        {
            PlayerData.playerDeck[_id]--;
            PlayerData.playerCards[_id]++;

            //下面这段注释是正确的:
            /// 该方法接下来做的所有事情是：
            /// 先把卡组中显示的数量-1
            /// 若数量-1后变成0了，则从对应字典中删除该项，表示该卡已不存在
            /// 然后  
            /// if 卡库中已经有该卡了，
            ///     就直接把卡库中显示的数量+1
            /// else if 卡库中还没有该卡
            ///     在卡库中创建该卡（创建卡的方法里会设置数量的）
            //deckDic[_id].GetComponent<CardCounter>().SetCounter(-1);  // 卡组中显示的数目-1
            if(!deckDic[_id].GetComponent<CardCounter>().SetCounter(-1))
            // 把卡组中显示的数量-1,若数量-1后变成0了，则从对应字典中删除该项，表示该卡已不存在
            // 不用担心，Scene中卡牌的销毁会在SetCounter中自动执行
            {
                deckDic.Remove(_id);
            }
            if(libraryDic.ContainsKey(_id))  // 判断键_id是否在library的字典中
            {
                //deckDic[_id].GetComponent<CardCounter>().SetCounter(-1);
                libraryDic[_id].GetComponent<CardCounter>().SetCounter(1);  // 卡库中显示额数量+1
            }
            else  // 如果不在，则需要重新创建并添加进字典里
                        //  （我不理解，如果不在字典里，就不可能需要把该卡移出Deck啊，
                        //  相反，要移入的目标位置倒是可能真的不存在)
            {
                //GameObject newCard = Instantiate(deckPrefab, deckPanel);
                //newCard.GetComponent<CardCounter>().SetCounter(PlayerData.playerDeck[_id]);
                //newCard.GetComponent<CardDisplay>().card = CardStore.cardList[_id];
                //deckDic.Add(_id, newCard);

                CreatCard(_id, CardState.Library);
            }
            //libraryDic[_id].GetComponent<CardCounter>().SetCounter(1);  // ps: 看不懂？libraryDic是字典，下同
            //deckDic[_id].GetComponent<CardCounter>().SetCounter(-1);
        }
        else if (_state == CardState.Library)
        {
            PlayerData.playerCards[_id]--;
            PlayerData.playerDeck[_id]++;

            //libraryDic[_id].GetComponent<CardCounter>().SetCounter(-1);  // 卡库中显示的数量-1 
            if(!libraryDic[_id].GetComponent<CardCounter>().SetCounter(-1))  // 曾因少写了'!'而出bug
            {
                libraryDic.Remove(_id);
            }

            ///这里出过bug
            ///错吧下面if里的deckDic写成libraryDic
            //if(libraryDic.ContainsKey(_id))
            if (deckDic.ContainsKey(_id))
            {
                //libraryDic[_id].GetComponent<CardCounter>().SetCounter(1);
                deckDic[_id].GetComponent<CardCounter>().SetCounter(1);
            }
            else
            {
                Debug.Log("CreatCard之前");
                CreatCard(_id, CardState.Deck);
            }
        }
    }

    /// <summary>
    /// 创建卡牌(到Library或Deck）
    /// </summary>
    public void CreatCard(int _id, CardState _cardState)
    {
        Debug.Log("CreatCard被调用");
        Transform targetPanel;  // 创建卡牌的目标位置
        GameObject targetPerfab;  // 要创建的预制件

        // refData的使用有点巧妙哦
        var refData = PlayerData.playerCards;  // refData默认为palyerCards，如果是deck，则在下面的else里转设为playerDeck
        Dictionary<int, GameObject> targetDic = libraryDic;  // 默认为librraryDic,如果是deck，则在下面的else里转设为deckDic
                                                             // 为何这个就用了字典，而上面直接用了var？思考!
                                                             //     大概明白，targetDic只是个字典类型的引用，让这个引用来表示lirbaryDic或deckDic


        if(_cardState == CardState.Library)
        {
            targetPanel = libraryPanel;
            targetPerfab = cardPrefab;
        }
        else /*if (_cardState == CardState.Deck)*/
        // 把else if 换成else，Instantiate里的两个实参就不报错了，好神奇
        {
            targetPanel = deckPanel;
            targetPerfab = deckPrefab;

            refData = PlayerData.playerDeck;
            targetDic = deckDic;
        }
        GameObject newCard = Instantiate(targetPerfab, targetPanel);  // 把else if 换成else，Instantiate里的两个实参就不报错了，好神奇
        newCard.GetComponent<CardCounter>().SetCounter(refData[_id]);
        newCard.GetComponent<CardDisplay>().card = CardStore.cardList [_id];
        targetDic.Add(_id, newCard);  // 在library或deck对应的字典中新增，表示卡库or卡组中已经有该卡了
    }
}
