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
        //���Ź�������
        m_animator.Play("Attack_1");
        //��ʼ�������Ƿ��Ѿ���Ч
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
            //���������ܵ���Ч��������
            m_oppssum.onAttackEffect?.Invoke(m_oppssum);
        }
        if (info.normalizedTime >=0.95f)
        {
            m_stateController.ChangeState("WarChase");
        }
    }
    public override void exit(){ }

}
