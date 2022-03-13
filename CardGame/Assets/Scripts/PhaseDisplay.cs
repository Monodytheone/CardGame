using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 游戏阶段显示
/// </summary>
public class PhaseDisplay : MonoBehaviour
{
    //public BattleManager BattleManager;
    // BattleManager已经是静态的了，不需要再专门获取了

    /// <summary>
    /// 显示游戏阶段的Text游戏对象
    /// </summary>
    public Text phaseText;









    // Start is called before the first frame update
    void Start()
    {
        BattleManager.Instance.phaseChangeEvent.AddListener(UpdateText);  // 添加回合改变的收听者 --- "UpdateText()方法"
    }

    // Update is called once per frame
    void Update()
    {

    }







    /// <summary>
    /// 更新phaseText显示的游戏阶段
    /// </summary>
    public void UpdateText()
    {
        //phaseText.text = BattleManager.GamePhase.ToString();
        phaseText.text = BattleManager.Instance.GamePhase.ToString();
    }
}
