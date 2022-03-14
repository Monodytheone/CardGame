using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 控制BattleScene里的格子
/// </summary>
public class Block : MonoBehaviour, IPointerDownHandler
{
    /// <summary>
    /// 用于存放格子里召唤的(怪兽)卡
    /// </summary>
    public GameObject card;

    /// <summary>
    /// 格子高亮
    /// 如果格子可以等待召唤，则显示出来（高亮）
    /// </summary>
    public GameObject SummonBlock;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (SummonBlock.activeInHierarchy)  // 仅当高亮显示在Hierarchy中被激活时，确认召唤
        {
            BattleManager.Instance.SummonConfirm(transform);  // (把自己的transform传过去)
        }
    }



}
