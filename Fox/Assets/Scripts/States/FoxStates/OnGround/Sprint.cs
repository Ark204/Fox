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
        //进入无敌
        //取消重力
        //m_rigidbody2D.gravityScale = 0;
        ////取消两个碰撞器
        //m_stateController.GetComponent<BoxCollider2D>().enabled = false;
        //m_stateController.GetComponent<PolygonCollider2D>().enabled = false;
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
        //还原两个碰撞器
        //m_stateController.GetComponent<BoxCollider2D>().enabled = true;
        //m_stateController.GetComponent<PolygonCollider2D>().enabled = true;
        ////恢复重力
        //m_rigidbody2D.gravityScale = 3;
        //退出无敌
    }
}
