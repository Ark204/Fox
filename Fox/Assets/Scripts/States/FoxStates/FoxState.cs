using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxState : IState
{
    //stateController
    protected StateController m_stateController;
    //components
    protected Animator m_animator;
    protected Rigidbody2D m_rigidbody2D;
    protected Fox m_fox;
    protected Transform m_transform;
    //���캯��
    public FoxState(StateController stateController)
    {
        //stateController��
        m_stateController = stateController;
        //�����ȡ
        m_animator = m_stateController.GetComponent<Animator>();
        m_rigidbody2D = m_stateController.GetComponent<Rigidbody2D>();
        m_fox = m_stateController.GetComponent<Fox>();
        m_transform = m_stateController.GetComponent<Transform>();
    }
    //�ӿ�ʵ��
    public virtual void enter(){}
    public virtual void update()
    {
        //�ƶ��������
        MoveMent();
        Cut();
        Silk();
        Climb();
        //Shut();
    }
    public virtual void exit(){}
    public virtual void onCollisionEnter2D(Collision2D collision) { }

    public virtual void onTriggerEnter2D(Collider2D collision) { }

    public virtual void onTriggerStay2D(Collider2D collision) { }

    public virtual void onTriggerExit2D(Collider2D collision) { }
    public virtual void OnEvent() 
    {
        OnGetAttack();
    }
    //ͨ���ƶ�����
    protected void MoveMent()
    {
        float horizontalmove = Input.GetAxisRaw("Horizontal");
        float speed = m_fox.speed;
        m_rigidbody2D.velocity = new Vector2(horizontalmove * speed, m_rigidbody2D.velocity.y);
        if(horizontalmove!=0)
        {
            m_transform.localScale = new Vector3(horizontalmove, 1, 1);
        }
    }
    protected void Cut()
    {
        if (m_fox.cutPressed)
        {
            m_fox.cutPressed = false;
            m_stateController.ChangeState("Cut");
        }
    }
    //������Чʱ���ô˷���
    protected virtual void OnGetAttack()
    {
        //��������״̬
        m_stateController.ChangeState("Hurt");
        //Ѫ����ȥ���ǹ�����
        m_fox.Red -= m_fox.enemy.GetComponent<Oppssum>().cutforce;
    }
    //��չ����
    protected void Climb()
    {
        //������¡���/�¡���������������Χ
        if (m_fox.climbPressed&&m_fox.inLadder)
        {
            //��ֹ���汻�л�Ϊſ��״̬
            m_fox.defensePressed = false;
            //�л�Ϊclimb״̬
            m_stateController.ChangeState("Climb");
        }
    }
    protected virtual void Silk()
    {
        //���������֩��˿
        if (m_fox.silkPressed)
        {
            //����֩��˿Ԥ����
            GameObject Silk = m_stateController.LoadPrefabs("Prefabs/silk");
            //��ʼ��Ԥ���������
            Vector2 silkStart = m_fox.silkStart.position;
            Silk.transform.position = new Vector2(silkStart.x+1, silkStart.y);
            //���÷����Ϊ������
            Silk.transform.SetParent(m_fox.silkStart);
            //������ת�Ƕ�
            Vector3 vector3 = new Vector3(m_fox.shutPoint.x - m_transform.position.x, m_fox.shutPoint.y - m_transform.position.y, 0);
            vector3.Normalize();
            Vector3 horizontal = new Vector3(1, 0, 0);
            float degree = Vector3.Angle(horizontal, vector3);
            //��ת�����
            m_fox.silkStart.transform.Rotate(0, 0,m_transform.localScale.x* degree);
            m_fox.silkPressed = false;
        }
    }
    //ͨ���������
    //protected virtual void Shut()
    //{
    //    //����������
    //    if(m_fox.shutPressed)
    //    {
    //        GameObject bullet = m_stateController.LoadPrefabs("Prefabs/bullet");
    //        bullet.transform.position = new Vector2(m_transform.position.x + m_fox.shutStartdistance * m_transform.localScale.x, m_transform.position.y);
    //        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
    //        bulletRb.velocity = new Vector2(10 * m_transform.localScale.x, bulletRb.velocity.y);
    //        m_fox.shutPressed = false;
    //    }
    //}
    
}
