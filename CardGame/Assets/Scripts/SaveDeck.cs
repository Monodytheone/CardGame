<<<<<<< HEAD
using System.Collections;
=======
锘縰sing System.Collections;
>>>>>>> parent of bb45b15 (绠�鍖朣aveDeck)
using System.Collections.Generic;
using UnityEngine;

/// <summary>
<<<<<<< HEAD
/// <para>点击SaveDeck按钮就把卡组保存到PlayerData.csv</para>
/// <para>挂在DataManager上</para>
/// <para>然后要在SaveDeck这个按钮的Inspector页面上把On Click()绑到该脚本的OnClickSaveDeck()方法</para>
=======
/// <para>鐐瑰嚮SaveDeck鎸夐挳灏辨妸鍗＄粍淇濆瓨鍒癙layerData.csv</para>
/// <para>鎸傚湪DataManager涓�</para>
/// <para>鐒跺悗瑕佸湪SaveDeck杩欎釜鎸夐挳鐨処nspector椤甸潰涓婃妸On Click()缁戝埌璇ヨ剼鏈殑OnClickSaveDeck()鏂规硶</para>
>>>>>>> parent of bb45b15 (绠�鍖朣aveDeck)
/// </summary>
public class SaveDeck : MonoBehaviour
{
    /// <summary>
<<<<<<< HEAD
    /// <para>点击SaveDeck按钮就把卡组保存到PlayerData.csv</para>
=======
    /// <para>鐐瑰嚮SaveDeck鎸夐挳灏辨妸鍗＄粍淇濆瓨鍒癙layerData.csv</para>
>>>>>>> parent of bb45b15 (绠�鍖朣aveDeck)
    /// </summary>
    public void OnClickSaveDeck()
    {
        GetComponent<PlayerData>().SavePlayerData();
    }
}
