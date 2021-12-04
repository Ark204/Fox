using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : OnGround
{
    public Idle(StateController stateController) : base(stateController) { }
    public override void enter()
    {
        base.enter();
        m_animator.Play("Idle");
    }
    public override void update()
    {
        base.update();
        //���ˮƽ�����ٶȴ���0
        if(Mathf.Abs(m_rigidbody2D.velocity.x)>0.2)
        {
            //�л�Ϊrun״̬
            m_stateController.ChangeState("Run");
        }
    }
}
