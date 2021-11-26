using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Climb : FoxState
{
    public Climb(StateController stateController) : base(stateController) { }
    public override void enter()
    {
        base.enter();
        LockLocation();
        //��ʼ����Ծ����
        m_fox.jumpCount = 2;
        //ȡ������
        m_rigidbody2D.gravityScale = 0;
        //ȡ����ײЧ��,�ı�Ϊ������
        m_stateController.GetComponent<PolygonCollider2D>().isTrigger = true;
        m_stateController.GetComponent<BoxCollider2D>().isTrigger = true;
        //����climb����
        m_animator.Play("climb");
    }
    public override void update()
    {
        //���ڲ��ٵ��û���Ļ����ƶ��Լ����Ч�����ʲ��ٵ��û�����·���
        //�����ƶ�
        ClimbMent();
        //�뿪����ʱ
        if(!m_fox.inLadder)
        {
            //�л��ص�վ��״̬
            m_stateController.ChangeState("Idle");
        }
        //������Ծ
        Jump();
    }
    public override void exit()
    {
        //��ԭ��ײЧ��
        m_stateController.GetComponent<PolygonCollider2D>().isTrigger = false;
        m_stateController.GetComponent<BoxCollider2D>().isTrigger = false;
        //��ԭ����
        m_rigidbody2D.gravityScale = 3;
    }
    void ClimbMent()
    {
        //����
        float verticalmove = Input.GetAxisRaw("Vertical");
        m_rigidbody2D.velocity = new Vector2(m_rigidbody2D.velocity.x,verticalmove * m_fox.speed );
    }
    void Jump()
    {
        //����кϷ���Ծ����
        if (m_fox.jumpPressed)
        {
            //�л�Ϊ��Ծ״̬
            m_stateController.ChangeState("Up");
            m_fox.jumpPressed = false;
        }
    }
    void LockLocation()
    {
        //����climb״̬ʱ����λ��
        m_rigidbody2D.velocity = new Vector2(0, 0);
        m_transform.position = new Vector3(m_fox.Ladder.position.x, m_transform.position.y, m_transform.position.z);
    }
}
