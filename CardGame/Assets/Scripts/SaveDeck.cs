<<<<<<< HEAD
using System.Collections;
=======
﻿using System.Collections;
>>>>>>> parent of bb45b15 (简化SaveDeck)
using System.Collections.Generic;
using UnityEngine;

/// <summary>
<<<<<<< HEAD
/// <para>���SaveDeck��ť�Ͱѿ��鱣�浽PlayerData.csv</para>
/// <para>����DataManager��</para>
/// <para>Ȼ��Ҫ��SaveDeck�����ť��Inspectorҳ���ϰ�On Click()�󵽸ýű���OnClickSaveDeck()����</para>
=======
/// <para>点击SaveDeck按钮就把卡组保存到PlayerData.csv</para>
/// <para>挂在DataManager上</para>
/// <para>然后要在SaveDeck这个按钮的Inspector页面上把On Click()绑到该脚本的OnClickSaveDeck()方法</para>
>>>>>>> parent of bb45b15 (简化SaveDeck)
/// </summary>
public class SaveDeck : MonoBehaviour
{
    /// <summary>
<<<<<<< HEAD
    /// <para>���SaveDeck��ť�Ͱѿ��鱣�浽PlayerData.csv</para>
=======
    /// <para>点击SaveDeck按钮就把卡组保存到PlayerData.csv</para>
>>>>>>> parent of bb45b15 (简化SaveDeck)
    /// </summary>
    public void OnClickSaveDeck()
    {
        GetComponent<PlayerData>().SavePlayerData();
    }
}
