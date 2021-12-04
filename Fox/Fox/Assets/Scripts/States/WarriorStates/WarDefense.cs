using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarDefense : WarriorState
{
    public WarDefense(StateController stateController) : base(stateController) { }
    public override void enter()
    {
        //����defense����
        m_animator.Play("Defence");
    }
    public override void update()
    {
        //������ǲ����ڹ���״̬
        if(!m_oppssum.checkAttack)
        {
            //�л���׷��״̬
            m_stateController.ChangeState("WarChase");
        }
    }
    public override void exit()
    {}
    protected override void OnGetAttack()
    {
        Debug.Log("�񵲹���");
        //ƽ��ֵ����
        m_oppssum.balance++;
        //ƽ�����������ƽ��ֵ
        if (m_oppssum.balance>m_oppssum.Maxbalance)
        {
            //�����Ӳֱ
            m_oppssum.stiffmulyiple = 3;
            m_stateController.ChangeState("WarHurt");
            //ƽ������0
            m_oppssum.balance = 0;
        }
    }
}