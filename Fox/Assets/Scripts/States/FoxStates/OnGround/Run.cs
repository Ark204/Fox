using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Run : OnGround
{
    public Run(StateController stateController) : base(stateController) { }
    public override void enter()
    {
        base.enter();
        //动画切换为running
        m_animator.Play("walking");
    }
    public override void update()
    {
        base.update();
        //如果水平速度小于0.1
        if(Mathf.Abs(m_rigidbody2D.velocity.x)<0.2)
        {
            //切换为idle状态
            m_stateController.ChangeState("Idle");
        }
    }
}
