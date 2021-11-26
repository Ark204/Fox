using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Run : OnGround
{
    public Run(StateController stateController) : base(stateController) { }
    public override void enter()
    {
        base.enter();
        //�����л�Ϊrunning
        m_animator.Play("run");
    }
    public override void update()
    {
        base.update();
        //���ˮƽ�ٶ�С��0.1
        if(Mathf.Abs(m_rigidbody2D.velocity.x)<0.2)
        {
            //�л�Ϊidle״̬
            m_stateController.ChangeState("Idle");
        }
    }
}
