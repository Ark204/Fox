using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defense : OnGround
{
    public Defense(StateController stateController) : base(stateController) { }
    public override void enter()
    {
        //播放hurt动画
        Debug.Log("播放防御动画");
    }
    public override void update()
    {
        //如果不再按下下蹲键
        if (!m_fox.defensePressed)
        {
            //切回站立状态
            m_stateController.ChangeState("Idle");
        }
    }
    public override void exit()
    {
        Debug.Log("防御结束");
    }
    protected override void OnGetAttack()
    {
        //平衡值增加
        m_fox.balance++;
        //平衡条超过最大平衡值
        if (m_fox.balance > m_fox.Maxbalance)
        {
            //进入大硬直
            //m_oppssum.stiffmulyiple = 2;
            m_stateController.ChangeState("Hurt");
            //平衡条清0
            m_fox.balance = 0;
        }
    }
}
