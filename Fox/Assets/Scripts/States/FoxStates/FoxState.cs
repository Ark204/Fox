using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
        m_animator = m_stateController.GetComponentInChildren<Animator>();
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
        //������¹����������ڿ��Ա�������״̬
        if (m_fox.backExecuteable && m_fox.cutPressed)
        {
            m_fox.cutPressed = false;
            m_stateController.ChangeState("BackExecute");
        }
        //������¹����������ڿ������洦��״̬
        if (m_fox.norExecuteable&& m_fox.cutPressed)
        {
            m_fox.cutPressed = false;
            m_stateController.ChangeState("Execute");
        }
        //������¹����ҹ�����������0,���빥��״̬
        if (m_fox.cutPressed&&m_fox.cutCount>0)
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
}
