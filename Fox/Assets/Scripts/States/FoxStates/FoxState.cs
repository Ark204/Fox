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
    //构造函数
    public FoxState(StateController stateController)
    {
        //stateController绑定
        m_stateController = stateController;
        //组件获取
        m_animator = m_stateController.GetComponent<Animator>();
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
        if (m_fox.cutPressed)
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
    protected virtual void Silk()
    {
        //如果按下射蜘蛛丝
        if (m_fox.silkPressed)
        {
            //加载蜘蛛丝预制体
            GameObject Silk = m_stateController.LoadPrefabs("Prefabs/silk");
            //初始化预制体的坐标
            Vector2 silkStart = m_fox.silkStart.position;
            Silk.transform.position = new Vector2(silkStart.x+1, silkStart.y);
            //设置发射点为父物体
            Silk.transform.SetParent(m_fox.silkStart);
            //计算旋转角度
            Vector3 vector3 = new Vector3(m_fox.shutPoint.x - m_transform.position.x, m_fox.shutPoint.y - m_transform.position.y, 0);
            vector3.Normalize();
            Vector3 horizontal = new Vector3(1, 0, 0);
            float degree = Vector3.Angle(horizontal, vector3);
            //旋转发射点
            m_fox.silkStart.transform.Rotate(0, 0,m_transform.localScale.x* degree);
            m_fox.silkPressed = false;
        }
    }
    //通用射击方法
    //protected virtual void Shut()
    //{
    //    //如果按下射击
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
