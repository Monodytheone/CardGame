using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 游戏阶段显示
/// </summary>
public class PhaseDisplay : MonoBehaviour
{
    public BattleManager BattleManager;

    /// <summary>
    /// 显示游戏阶段的Text游戏对象
    /// </summary>
    public Text phaseText;









    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateText();
    }







    /// <summary>
    /// 更新phaseText显示的游戏阶段
    /// </summary>
    public void UpdateText()
    {
        phaseText.text = BattleManager.GamePhase.ToString();
    }
}
