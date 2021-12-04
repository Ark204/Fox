using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stiff : FoxState
{
    public Stiff(StateController stateController) : base(stateController) { }
    private AnimatorStateInfo info;
    public override void enter()
    {
        //播放硬直动画
        m_animator.Play("skill_2");
        Debug.Log("主角：硬直");
    }
    public override void update()
    {
        info = m_animator.GetCurrentAnimatorStateInfo(0);
        if(info.normalizedTime>0.95f)
        {
            m_stateController.ChangeState("Idle");
        }
    }
    public override void exit()
    {
        
    }
}
