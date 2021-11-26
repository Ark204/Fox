using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarDefense : WarriorState
{
    public WarDefense(StateController stateController) : base(stateController) { }
    public override void enter()
    {
        //播放defense动画
        Debug.Log("敌人：播放防御动画");
    }
    public override void update()
    {
        //如果主角不处于攻击状态
        if(!m_oppssum.checkAttack)
        {
            //切换会追击状态
            m_stateController.ChangeState("WarChase");
        }
    }
    public override void exit()
    {
        Debug.Log("敌人：防御结束");
    }
    protected override void OnGetAttack()
    {
        Debug.Log("格挡攻击");
        //平衡值增加
        m_oppssum.balance++;
        //平衡条超过最大平衡值
        if (m_oppssum.balance>m_oppssum.Maxbalance)
        {
            //进入大硬直
            m_oppssum.stiffmulyiple = 3;
            m_stateController.ChangeState("WarHurt");
            //平衡条清0
            m_oppssum.balance = 0;
        }
        
    }
}
