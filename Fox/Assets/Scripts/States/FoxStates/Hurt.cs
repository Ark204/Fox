using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurt : FoxState
{
    public Hurt(StateController stateController) : base(stateController) { }
    private AnimatorStateInfo info;
    public override void enter()
    {
        base.enter();
        //����hurt����
        m_animator.Play("skill_1");
        m_transform.Translate(-1.0f * m_fox.enemy.localScale.x, 0, 0);
    }
    public override void update()
    {
        info = m_animator.GetCurrentAnimatorStateInfo(0);
        if(info.normalizedTime>=0.95f)
        {
            m_stateController.ChangeState("Idle");
        }
    }
    public override void exit()
    {
        
    }
}
