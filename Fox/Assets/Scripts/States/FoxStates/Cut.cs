using UnityEngine;

public class Cut : FoxState
{
    private AnimatorStateInfo info;
    public Cut(StateController stateController) : base(stateController) { }
    public override void enter()
    {
        base.enter();
        m_rigidbody2D.velocity = new Vector2(0, 0);
        //砍的次数减一
        m_fox.cutCount--;
        if(m_fox.cutCount==1)
        {
            m_animator.Play("attack_1");
        }
        if(m_fox.cutCount==0)
        {
            m_animator.Play("attack_2");
        }
    }
    public override void update()
    {
        info = m_animator.GetCurrentAnimatorStateInfo(0);
        //处决或者二段砍
        Cut();
        //砍到一半攻击生效
        if(0.5f < info.normalizedTime &&
            info.normalizedTime < 0.53&&
            Physics2D.OverlapCircle(m_fox.attackPoint.position, m_fox.attackR, m_fox.enemies))
        {
            //调用敌人受到生效攻击函数
            if(m_fox.enemy!=null)
            {
                m_fox.enemy.GetComponent<StateController>().StateEvent();
            }
        }
        //砍完了
        if (info.normalizedTime >= 0.95f)
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
