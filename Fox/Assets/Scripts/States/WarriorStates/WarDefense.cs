using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarDefense : WarriorState
{
    public WarDefense(StateController stateController) : base(stateController) { }
    public override void enter()
    {
        //播放defense动画
        m_animator.Play("Defence");
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
    public override void exit(){ }
}
