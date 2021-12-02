using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarAttack : WarriorState
{
    public WarAttack(StateController stateController) : base(stateController) { }
    private AnimatorStateInfo info;
    public override void enter()
    {
        //播放攻击动画
        m_animator.Play("Attack_1");
    }
    public override void update()
    {
        info = m_animator.GetCurrentAnimatorStateInfo(0);
        if (0.5f < info.normalizedTime && info.normalizedTime < 0.535f&&
            Physics2D.OverlapCircle(m_oppssum.attackPoint.position, m_oppssum.attackR, m_oppssum.targetLayer))
        {
            //调用主角受到生效攻击函数
            if (m_oppssum.target != null)
            {
                m_oppssum.target.GetComponent<StateController>().StateEvent();
            }
        }
        if (info.normalizedTime >=0.95f)
        {
            m_stateController.ChangeState("WarChase");
        }
    }
    public override void exit(){ }

}
