using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stiff : FoxState
{
    public Stiff(StateController stateController) : base(stateController) { }
    private AnimatorStateInfo info;
    public override void enter()
    {
        //����Ӳֱ����
        m_animator.Play("skill_2");
        Debug.Log("���ǣ�Ӳֱ");
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
