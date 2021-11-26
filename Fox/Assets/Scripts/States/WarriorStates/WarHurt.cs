using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarHurt : WarriorState
{
    public WarHurt(StateController stateController) : base(stateController) { }
    private float timer=0;
    public override void enter()
    {
        base.enter();
        //播放hurt动画
        Debug.Log("敌人：受伤");
        //击退
        m_transform.Translate(-0.5f* m_transform.localScale.x, 0, 0);
    }
    public override void update()
    {
        timer += Time.fixedDeltaTime;
        if(timer>m_oppssum.stiffTime*m_oppssum.stiffmulyiple)
        {
            if (m_oppssum.HP <= 0)
            {
                m_stateController.ChangeState("WarDeath");
            }
            else
            {
                m_stateController.ChangeState("WarChase");
            }
        }
    }
    public override void exit()
    {
        //硬直时间倍数初始化
        m_oppssum.stiffmulyiple = 1;
        //处决标志去除
        m_oppssum.execute.gameObject.SetActive(false);
        //计时器清零
        timer =0;
        Debug.Log("敌人：恢复");
    }
}
public class WarDeath : WarriorState
{
    public WarDeath(StateController stateController) : base(stateController) { }
    public override void enter()
    {
        m_animator.Play("death");
    }
    public override void update()
    {
        
    }
    public override void exit()
    {
        
    }
}