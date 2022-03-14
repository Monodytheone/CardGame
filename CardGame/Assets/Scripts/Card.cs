// 我来打一行中文注释

public class Card
{
    public int id;
    public string cardName;

    //构造函数
    public Card(int _id, string _cardName)
    {
        id = _id;
        cardName = _cardName;
    }
}

// 怪物卡
public class MonsterCard: Card  
{
    public int attack; // 攻击力
    public int healthPoint;  // 当前血量
    public int heathPointMax;  // 血量上限
    /// <summary>
    /// 可攻击对方的次数
    /// </summary>
    public int attackTime;
    // 等级、属性等属性本例子就不添加了，本教程追求最简

    public MonsterCard (int _id, string _cardName, int _attack, int _healthPointMax) : base(_id, _cardName)  // 最后的base：这个构造哈数继承了父类的构造函数
    {
        attack = _attack;
        healthPoint = _healthPointMax;
        heathPointMax = _healthPointMax;
        attackTime = 2;
    }
}

// 魔法卡
public class SpellCard: Card
{
    public string effect;  // 技能描述 --用于存储技能效果

    public SpellCard (int _id, string _cardName, string _effect) : base(_id, _cardName)
    {
        effect = _effect;
    }
}














//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class NewBehaviourScript : MonoBehaviour
//{
//    // Start is called before the first frame update
//    void Start()
//    {
        
//    }

//    // Update is called once per frame
//    void Update()
//    {
        
//    }
//}
