using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Up : Onsky
{
    public Up(StateController stateController) : base(stateController) { }
    public override void enter()
    {
        //�����л�Ϊjumping
        m_animator.Play("jumping");
        //��Ծ������һ
        m_fox.jumpCount--;
        //��Ծ
        m_rigidbody2D.velocity = new Vector2(m_rigidbody2D.velocity.x,m_fox.jumpforce);
    }
    public override void update()
    {
        //�����ƶ�
        base.update();
        //�����ֱ�����ٶ�С��0
        if(m_rigidbody2D.velocity.y<0)
        {
            //�л�Ϊ����״̬
            m_stateController.ChangeState("Down");
        }
    }
}
