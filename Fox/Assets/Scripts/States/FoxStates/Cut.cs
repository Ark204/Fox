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
    }
    public override void update()
    {
        cutTime -= Time.fixedDeltaTime;
        //处决或者二段砍
        Cut();
        //砍到一半攻击生效
        if(m_fox.cutTime/2-Time.fixedDeltaTime < cutTime && 
            cutTime < m_fox.cutTime/2&&
            Physics2D.OverlapCircle(m_fox.attackPoint.position, m_fox.attackR, m_fox.enemies))
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
    public override void exit(){ }
}
