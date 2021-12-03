using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarAttack : WarriorState
{
    public WarAttack(StateController stateController) : base(stateController) { }
    private AnimatorStateInfo info;
    private bool attackEffect;
    public override void enter()
    {
        //播放攻击动画
        m_animator.Play("Attack_1");
        //初始化攻击是否已经生效
        attackEffect = false;
    }
    public override void update()
    {
        info = m_animator.GetCurrentAnimatorStateInfo(0);
        if (!attackEffect&&
            m_oppssum.cutEffect < info.normalizedTime && 
            Physics2D.OverlapCircle(m_oppssum.attackPoint.position, m_oppssum.attackR, m_oppssum.targetLayer))
        {
            attackEffect = true;
            //调用主角受到生效攻击函数
            m_oppssum.onAttackEffect?.Invoke(m_oppssum);
        }
        if (info.normalizedTime >=0.95f)
        {
            m_stateController.ChangeState("WarChase");
        }
    }
    public override void exit(){ }

}
