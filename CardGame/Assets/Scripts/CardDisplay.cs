



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  // unity的UI库

/// <summary>
/// 这个脚本是挂载在游戏中的"Card"这个对象上的
/// <para>Card对象中的子对象们，都会一一和这个CardDisplay类的成员挂接</para>
/// <para>该脚本用于显示卡牌</para>
/// <para>单独用一个脚本来控制卡牌的显示的原因是：卡牌有怪物卡和魔法卡两种类型，它们拥有的属性不同，
/// 需要根据卡牌的类型来显示不同的组件</para>
/// 
/// </summary>
public class CardDisplay : MonoBehaviour
{
    public Text nameText;  // Text: UnityEngine.UI.Text
    public Text attactText;
    public Text healthText;
    public Text effectText;

    public Image backgroundImage;  // Image: UnityEngine.UI.Text

    public Card card;  // 别的文件里的Card类竟然直接就能使用到














    // Start is called before the first frame update
    void Start()
    {
        ShowCard();  // 一进入游戏就要显示
    }

    // Update is called once per frame
    void Update()
    {
        
    }
















    public void ShowCard()
    {
        nameText.text = card.cardName;  // 将CardDisplay中的nameText的文字设置为卡牌card的cardName
        if (card is MonsterCard)
        {
            var monster = card as MonsterCard;
            //MonsterCard moster = (MonsterCard)card;  // 为什么不这么写？
            attactText.text = monster.attack.ToString();
            //attactText.text = card.attack.ToString();  // 这样不行~，attack下划线报错

            // 从这里开始，我先自己补全(能补全个屁嘞，还有其他事情要做呢）
            this.healthText.text = monster.healthPoint.ToString();

            effectText.gameObject.SetActive(false);  // 把effectText隐藏起来（怪物卡根本没有effect啊)
        }
        else if (card is SpellCard)
        {
            var spell = card as SpellCard;
            this.effectText.text = spell.effect;
            attactText.gameObject.SetActive(false);  // 把atk和health隐藏起来
            healthText.gameObject.SetActive(false);
        }
    }
}
