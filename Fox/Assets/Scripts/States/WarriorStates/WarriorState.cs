using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorState : IState
{
    //stateController
    protected StateController m_stateController;
    //components
    protected Animator m_animator;
    protected Rigidbody2D m_rigidbody2D;
    protected Oppssum m_oppssum;
    protected Transform m_transform;
    //构造函数
    public WarriorState(StateController stateController)
    {
        //stateController绑定
        m_stateController = stateController;
        //组件获取
        m_animator = m_stateController.GetComponent<Animator>();
        m_rigidbody2D = m_stateController.GetComponent<Rigidbody2D>();
        m_oppssum = m_stateController.GetComponent<Oppssum>();
        m_transform = m_stateController.GetComponent<Transform>();
    }
    //接口实现
    public virtual void enter() { }
    public virtual void update()
    {
        //移动输入更新
        //Cut();
    }
    public virtual void exit() { }
    public virtual void onCollisionEnter2D(Collision2D collision) { }

    public virtual void onTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Fox"))
        {
            m_oppssum.target = collision.transform;
        }
    }

    public virtual void onTriggerStay2D(Collider2D collision) { }

    public virtual void onTriggerExit2D(Collider2D collision) 
    {
        if (collision.CompareTag("Fox"))
        {
            m_oppssum.target = null;
        }
    }
    public virtual void OnEvent() 
    {
        OnGetAttack();
    }
    //改变朝向
    protected void FlipTo(Transform target)
    {
        if(target!=null)
        {
            if(m_transform.position.x>target.position.x)
            {
                m_transform.localScale = new Vector3(1, 1, 1);
            }
            else if(m_transform.position.x<target.position.x)
            {
                m_transform.localScale = new Vector3(-1, 1, 1);
            }
        }
    }
    protected void Chase()
    {
        if (m_oppssum.target != null &&
            m_oppssum.target.position.x > m_oppssum.chasePoint[0].position.x &&
            m_oppssum.target.position.x < m_oppssum.chasePoint[1].position.x)
        {
            m_stateController.ChangeState("WarChase");
        }
    }
    protected void Defense()
    {
        //如果检测到发起进攻
        if(m_oppssum.checkAttack)
        {
            m_stateController.ChangeState("WarDefense");
        }
    }
    //攻击生效时调用此方法
    protected virtual void OnGetAttack()
    {
        //进入受伤状态
        m_stateController.ChangeState("WarHurt");
        //血量减去主角攻击力
        m_oppssum.HP -= m_oppssum.target.GetComponent<Fox>().cutforce;
    }
}
