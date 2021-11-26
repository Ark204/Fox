using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarIdle : WarriorState
{
    public WarIdle(StateController stateController) : base(stateController) { }
    private float timer;
    public override void enter()
    {
        m_animator.Play("idle");
    }
    public override void update()
    {
        timer += Time.fixedDeltaTime;
        if(timer>=m_oppssum.idleTime)
        {
            m_stateController.ChangeState("WarRun");
        }
        Defense();
    }
    public override void exit()
    {
        timer = 0;
    }
   
}
