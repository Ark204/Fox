using UnityEngine;

public class Cut : FoxState
{
    public Cut(StateController stateController) : base(stateController) { }
    private AnimatorStateInfo info;
    private bool attackEffect;
    public override void enter()
    {
        base.enter();
        m_fox.onCutStart?.Invoke(m_fox);
        //刹车
        m_rigidbody2D.velocity = new Vector2(0, 0);
        //初始化攻击是否已经生效
        attackEffect = false;
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
        //砍到一半攻击生效
        if(!attackEffect&&
            m_fox.cutEffect < info.normalizedTime &&
            Physics2D.OverlapCircle(m_fox.attackPoint.position, m_fox.attackR, m_fox.enemies))
        {
            //攻击已经生效
            attackEffect = true;
            //调用敌人受到生效攻击函数
            m_fox.onAttackEffect?.Invoke(m_fox);
        }
        //攻击生效后可以发动二次进攻
        if(0.54f < info.normalizedTime)
        {
            //处决或者二段砍
            Cut();
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
    public override void exit()
    {
        m_fox.onCutEnd?.Invoke();
    }
    
}
