using UnityEngine;

public class Cut : FoxState
{
    private float cutTime;
    public Cut(StateController stateController) : base(stateController) { }
    public override void enter()
    {
        base.enter();
        //初始化状态持续时间
        cutTime = m_fox.cutTime;
        //砍的次数减一
        m_fox.cutCount--;
        if(m_fox.cutCount==1)
        {
            Debug.Log("主角：播放第一砍动画");
        }
        if(m_fox.cutCount==0)
        {
            Debug.Log("主角：播放第二砍动画");
        }
        //开启攻击范围
        m_stateController.GetComponentInChildren<CircleCollider2D>().enabled = true;
    }
    public override void update()
    {
        cutTime -= Time.fixedDeltaTime;
        //二段砍
        if(m_fox.cutPressed&&m_fox.cutCount>0)
        {
            m_fox.cutPressed = false;
            //进入二砍状态
            m_stateController.ChangeState("Cut");
        }
        //砍到一半攻击生效
        if(m_fox.cutTime/2-Time.fixedDeltaTime < cutTime && cutTime < m_fox.cutTime/2
            && Physics2D.OverlapCircle(m_fox.attackPoint.position, m_fox.attackR, m_fox.enemies))
        {
            //调用敌人受到生效攻击函数
            if(m_fox.enemy!=null)
            {
                m_fox.enemy.GetComponent<StateController>().StateEvent();
            }
        }
        //砍完了
        if(cutTime<0)
        {
            //如果是第二次斩结束
            if (m_fox.cutCount == 0)
            {
                //进入硬直
                m_stateController.ChangeState("Stiff");
            }
            //返回idle状态
            else
              m_stateController.ChangeState("Idle");
        }
    }
    public override void exit()
    {
        m_stateController.GetComponentInChildren<CircleCollider2D>().enabled = false;
    }
    //检测敌人是否在范围内
    public override void onTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemies")
        {
            m_fox.enemy = collision.transform;
        }
    }
}
