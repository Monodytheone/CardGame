using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 表示卡牌是处在卡库中还是卡组中
/// </summary>
public enum CardState
{
    Library, Deck
}

/// <summary>
/// <para>若点击卡库中的牌，则移动一张该牌到卡组；若点击卡组中的牌，则移动一张该牌到卡库</para>
/// <para>挂在预制件上</para>
/// </summary>
public class ClickCard : MonoBehaviour, IPointerDownHandler
{
    // 以下两脚本数据的获取方式参照DeckManager
    private DeckManager DeckManager;
    //private PlayerData PlayerData;     // 为啥又把这个注释掉了？我想大概是因为DeckManager里已经有PlayerData了

    ///// <summary>
    ///// 挂DataManager，用于传入PlyerData
    ///// </summary>
    //public GameObject DataManager;

    /// <summary>
    /// 卡牌位置（卡库or卡牌？)
    /// <para>不用担心state的值是从哪里获得的，挂在</para>
    /// </summary>
    public CardState state;







    // Start is called before the first frame update
    void Start()
    {
        DeckManager = GameObject.Find("DeckManager").GetComponent<DeckManager>();  // 要了解GameObject.Find()的功能
        //PlayerData = DataManager.GetComponent<PlayerData>();
        //PlayerData = GameObject.Find("DataManager").GetComponent<PlayerData>();  // 要了解这种写法和DeckManager里写法的对比
    }

    // Update is called once per frame
    void Update()
    {
        
    }







    /// <summary>
    /// 鼠标点击，与本类继承的 IPointerDownHandler 相应
    /// </summary>
    /// <param name="pointerDown"></param>
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("点击有效");
        int id = this.GetComponent<CardDisplay>().card.id;
        // 这个CardDisplay又是谁身上的呢？
        //   答：是this
        //   再问：this是谁呢？是鼠标点在的那个物体（卡牌）吗?
        //   再答：似乎是的
        //

        DeckManager.UpdateCard(state, id);
    }


}
