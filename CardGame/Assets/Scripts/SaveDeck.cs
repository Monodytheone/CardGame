using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <para>���SaveDeck��ť�Ͱѿ��鱣�浽PlayerData.csv</para>
/// <para>����DataManager��</para>
/// <para>Ȼ��Ҫ��SaveDeck�����ť��Inspectorҳ���ϰ�On Click()�󵽸ýű���OnClickSaveDeck()����</para>
/// </summary>
public class SaveDeck : MonoBehaviour
{
    /// <summary>
    /// <para>���SaveDeck��ť�Ͱѿ��鱣�浽PlayerData.csv</para>
    /// </summary>
    public void OnClickSaveDeck()
    {
        GetComponent<PlayerData>().SavePlayerData();
    }
}
