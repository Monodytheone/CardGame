﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 卡牌是在手牌区中还是在格子中
/// </summary>
public enum BattleCardState
{
    /// <summary>
    /// 在手牌区
    /// </summary>
    inHand,

    /// <summary>
    /// 在格子里
    /// </summary>
    inBlock,

}

public class BattleCard : MonoBehaviour, IPointerDownHandler
{
    /// <summary>
    /// 
    /// </summary>
    public int playerID;

    /// <summary>
    /// 手牌所处位置，默认为在手牌区
    /// </summary>
    public BattleCardState state = BattleCardState.inHand;

    /// <summary>
    /// 攻击次数
    /// </summary>
    public int AttackCount;
    private int attackCount;






    public void OnPointerDown(PointerEventData eventData)
    {
        //GamePhase currentPhase = BattleManager.Instance.GamePhase;  // 当前游戏阶段

        //// 当游戏阶段不是本卡所属玩家的行动阶段时，不允许召唤
        //if(playerID == 0 && !(currentPhase == GamePhase.playerAction))
        //{
        //    Debug.Log("不允许召唤");
        //    return;
        //}
        //else if(playerID == 1 && !(currentPhase == GamePhase.enemyAction))
        //{
        //    Debug.Log("不允许召唤");
        //    return;
        //}

 
        if (GetComponent<CardDisplay>().card is MonsterCard)
        {
            // 当怪物卡在手牌区里被点击时，发起召唤请求
            if (state == BattleCardState.inHand)
            {
                BattleManager.Instance.SummonRequest(playerID, gameObject);
            }
            // 当怪物卡在场上被点击时，发起攻击请求
            else if(state == BattleCardState.inBlock && attackCount > 0)
            {
                BattleManager.Instance.AttackRequest(playerID, gameObject);
            }
        }
    }

    /// <summary>
    /// 用于重置攻击次数（上场时）
    /// </summary>
    public void ResetAttack()
    {
        attackCount = AttackCount;
    }
}
