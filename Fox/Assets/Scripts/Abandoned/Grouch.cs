using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Obsolete("����ſ��״̬",true)]
public class Grouch : OnGround
{
    public Grouch(StateController stateController) : base(stateController) { }
    public override void enter()
    {
        base.enter();
        //ȡ���Ϸ���ײ��
        m_stateController.GetComponent<PolygonCollider2D>().enabled = false;
        //����ſ�¶���
        m_animator.Play("crouch");
    }
    //��дupdate����
    public override void update()
    {
        MoveMent();
        //�����Լ���Shut����
        //Shut();
        //GroundJump();//���ٴ�����Ծ
        Grouch();
        GroundFall();
        //������ٰ����¶׼�������û���ϰ��������������
        //if (m_rigidbody2D.velocity.y<-0.1||(!m_fox.grouchPressed && !Physics2D.OverlapCircle(m_fox.CellingCheck.position, 0.2f,m_fox.layerMask)))
        //{
        //    //�л�վ��״̬
        //    m_stateController.ChangeState("Idle");
        //} 
    }
    public override void exit()
    {
        m_stateController.GetComponent<PolygonCollider2D>().enabled = true;
    }
    //��дShut����
    //protected override void Shut()
    //{
    //    //����������
    //    if (m_fox.shutPressed)
    //    {
    //        GameObject bullet = m_stateController.LoadPrefabs("Prefabs/bullet");
    //        //��������߶�
    //        bullet.transform.position = new Vector2(m_transform.position.x + m_fox.shutStartdistance * m_transform.localScale.x, m_transform.position.y+m_fox.crouchdistance);
    //        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
    //        bulletRb.velocity = new Vector2(10 * m_transform.localScale.x, bulletRb.velocity.y);
    //        m_fox.shutPressed = false;
    //    }
    //}
}
