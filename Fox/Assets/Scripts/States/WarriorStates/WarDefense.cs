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
    public override void exit(){ }
}
