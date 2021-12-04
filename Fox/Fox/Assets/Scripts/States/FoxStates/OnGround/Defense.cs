using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defense : OnGround
{
    public Defense(StateController stateController) : base(stateController) { }
    public override void enter()
    {
        //����hurt����
        m_animator.Play("defend");
    }
    public override void update()
    {
        //������ٰ����¶׼�
        if (!m_fox.defensePressed)
        {
            //�л�վ��״̬
            m_stateController.ChangeState("Idle");
        }
    }
    public override void exit()
    {
        
    }
    protected override void OnGetAttack()
    {
        //ƽ��ֵ����
        m_fox.balance++;
        //ƽ�����������ƽ��ֵ
        if (m_fox.balance > m_fox.Maxbalance)
        {
            //�����Ӳֱ
            //m_oppssum.stiffmulyiple = 2;
            m_stateController.ChangeState("Hurt");
            //ƽ������0
            m_fox.balance = 0;
        }
    }
}
