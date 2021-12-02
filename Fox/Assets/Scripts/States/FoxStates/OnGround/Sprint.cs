using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprint : OnGround
{
    private float moveTime;
    public Sprint(StateController stateController) : base(stateController) { }
    public override void enter()
    {
        base.enter();
        //初始化移动时间
        moveTime= m_fox.sprintTime;
        m_animator.Play("shift");
        //取消碰撞
        Physics2D.IgnoreLayerCollision(10, 13);
    }
    public override void update()
    {
        moveTime -= Time.fixedDeltaTime;
        //如果冲刺结束
        if(moveTime<0)
        {
            m_stateController.ChangeState("Idle");
        }
        else
        {
            m_rigidbody2D.velocity = new Vector2(m_transform.localScale.x * m_fox.sprintspeed, m_rigidbody2D.velocity.y);
        }
    }
    public override void exit()
    {
        m_animator.Play("shift_2");
        //还原碰撞
        Physics2D.IgnoreLayerCollision(10, 13, false);
        //进入冷却
        m_fox.sprintCurrCd = m_fox.sprintCd;
    }
}
