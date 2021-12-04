using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGround : FoxState
{
    //���캯��
    public OnGround(StateController stateController) : base(stateController) { }
    public override void enter()
    {
        //�������״̬ʱ����������Ծ����
        m_fox.jumpCount = 2;
        //�������ÿ�����
        m_fox.cutCount = 2;
    }
    public override void update()
    {
        base.update();
        Sprint();
        GroundJump();
        GroundFall();
        Defense();
        //����ſ���л�
        //Grouch();
    }
    //������Ծ
    protected void GroundJump()
    {
        //����кϷ���Ծ����
        if(m_fox.jumpPressed)
        {
            //�л�Ϊ��Ծ״̬
            m_stateController.ChangeState("Up");
            m_fox.jumpPressed = false;
        }
    }
    protected void GroundFall()
    {
        //����ӵ�������
        if(m_rigidbody2D.velocity.y<-0.1)
        {
            //�л�Ϊ����״̬
            m_stateController.ChangeState("Down");
        }
    }
    protected void Sprint()
    {
        if(m_fox.sprintPressed)
        {
            //�л�Ϊ���״̬
            m_stateController.ChangeState("Sprint");
            //���ð���
            m_fox.sprintPressed = false;
        }
    }
    protected void Defense()
    {
        if (m_fox.defensePressed)
        {
            //�л�Ϊ����״̬
            m_stateController.ChangeState("Defense");
        }
    }
    //Obsolete
    [System.Obsolete("����ſ��״̬", true)]
    protected void Grouch()
    {
        //if (m_fox.grouchPressed)
        //{

        //    //�л�Ϊſ��״̬
        //    //m_stateController.ChangeState(FoxState.grouch);
        //}
    }
}
