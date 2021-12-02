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
    //构造函数
    public FoxState(StateController stateController)
    {
        //stateController绑定
        m_stateController = stateController;
        //组件获取
        m_animator = m_stateController.GetComponentInChildren<Animator>();
        m_rigidbody2D = m_stateController.GetComponent<Rigidbody2D>();
        m_fox = m_stateController.GetComponent<Fox>();
        m_transform = m_stateController.GetComponent<Transform>();
    }
    //接口实现
    public virtual void enter(){}
    public virtual void update()
    {
        //移动输入更新
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
    //通用移动方法
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
        //如果按下攻击并且属于可以背部处决状态
        if (m_fox.backExecuteable && m_fox.cutPressed)
        {
            m_fox.cutPressed = false;
            m_stateController.ChangeState("BackExecute");
        }
        //如果按下攻击并且属于可以正面处决状态
        if (m_fox.norExecuteable&& m_fox.cutPressed)
        {
            m_fox.cutPressed = false;
            m_stateController.ChangeState("Execute");
        }
        //如果按下攻击且攻击次数大于0,进入攻击状态
        if (m_fox.cutPressed&&m_fox.cutCount>0)
        {
            m_fox.cutPressed = false;
            m_stateController.ChangeState("Cut");
        }
    }
    //攻击生效时调用此方法
    protected virtual void OnGetAttack()
    {
        //进入受伤状态
        m_stateController.ChangeState("Hurt");
        //血量减去主角攻击力
        m_fox.Red -= m_fox.enemy.GetComponent<Oppssum>().cutforce;
    }
    //扩展功能
    protected void Climb()
    {
        //如果按下“上/下”键，且在梯子周围
        if (m_fox.climbPressed&&m_fox.inLadder)
        {
            //防止后面被切换为趴下状态
            m_fox.defensePressed = false;
            //切换为climb状态
            m_stateController.ChangeState("Climb");
        }
    }
}
