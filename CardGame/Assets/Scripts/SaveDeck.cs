using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <para>点击SaveDeck按钮就把卡组保存到PlayerData.csv</para>
/// <para>挂在DataManager上</para>
/// <para>然后要在SaveDeck这个按钮的Inspector页面上把On Click()绑到该脚本的OnClickSaveDeck()方法</para>
/// </summary>
public class SaveDeck : MonoBehaviour
{
    /// <summary>
    /// <para>点击SaveDeck按钮就把卡组保存到PlayerData.csv</para>
    /// </summary>
    public void OnClickSaveDeck()
    {
        GetComponent<PlayerData>().SavePlayerData();
    }
}
