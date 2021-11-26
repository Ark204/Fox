using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGround : FoxState
{
    //构造函数
    public OnGround(StateController stateController) : base(stateController) { }
    public override void enter()
    {
        //进入地面状态时重新设置跳跃次数
        m_fox.jumpCount = 2;
        //重新设置砍次数
        m_fox.cutCount = 2;
    }
    public override void update()
    {
        base.update();
        Sprint();
        GroundJump();
        GroundFall();
        Defense();
        //弃用趴下切换
        //Grouch();
    }
    //地上跳跃
    protected void GroundJump()
    {
        //如果有合法跳跃输入
        if(m_fox.jumpPressed)
        {
            //切换为跳跃状态
            m_stateController.ChangeState("Up");
            m_fox.jumpPressed = false;
        }
    }
    protected void GroundFall()
    {
        //如果从地面落下
        if(m_rigidbody2D.velocity.y<-0.1)
        {
            //切换为下落状态
            m_stateController.ChangeState("Down");
        }
    }
    protected void Sprint()
    {
        if(m_fox.sprintPressed)
        {
            //切换为冲刺状态
            m_stateController.ChangeState("Sprint");
            //重置按键
            m_fox.sprintPressed = false;
        }
    }
    protected void Defense()
    {
        if (m_fox.defensePressed)
        {
            //切换为防御状态
            m_stateController.ChangeState("Defense");
        }
    }
    //Obsolete
    [System.Obsolete("废弃趴下状态", true)]
    protected void Grouch()
    {
        //if (m_fox.grouchPressed)
        //{

        //    //切换为趴下状态
        //    //m_stateController.ChangeState(FoxState.grouch);
        //}
    }
}
