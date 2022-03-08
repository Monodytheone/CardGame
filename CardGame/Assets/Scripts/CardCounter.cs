using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class CardCounter : MonoBehaviour
{
    ///// <summary>
    ///// 挂载Text_Counter，卡牌数量的计数器
    ///// </summary>
    //public Text counter;



    /// <summary>
    /// counter的显示
    /// </summary>
    public Text counterText;

    /// <summary>
    /// 卡牌数量计数器
    /// </summary>
    private int counter = 0;










    /// <summary>
    /// 既可用于设定counter的初始值，也可用于修改
    /// </summary>
    /// <param name="_value">可正可负，表示对counter加/减的值</param>
    /// <returns>若counter变为0销毁了卡，返回false；若没有销毁卡，返回true</returns>
    public bool SetCounter(int _value)
    {
        counter += _value;
        OnCounterChange();
        if (counter == 0)
        {
            Destroy(gameObject);
            return false;
        }
        return true;
    }
    
    /// <summary>
    /// 用于当counter值变化时，以更新counterText的显示
    /// 若counter降到0，则销毁该卡
    /// </summary>
    private void OnCounterChange()
    {
        counterText.text = counter.ToString();
    }
}
