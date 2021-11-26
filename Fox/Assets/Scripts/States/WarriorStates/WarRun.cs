using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarRun : WarriorState
{
    public WarRun(StateController stateController) : base(stateController) { }
    private int patrolPositon;
    public override void enter()
    {
        //≤•∑≈≈‹≤Ω∂Øª≠
        m_animator.Play("run");
    }
    public override void update()
    {
        FlipTo(m_oppssum.patrolPoint[patrolPositon]);
        m_transform.position = Vector2.MoveTowards(m_transform.position,
            m_oppssum.patrolPoint[patrolPositon].position, m_oppssum.speed * Time.fixedDeltaTime);
        if(Vector2.Distance(m_transform.position,m_oppssum.patrolPoint[patrolPositon].position)<=1.5f)
        {
            m_stateController.ChangeState("WarIdle");
        }
        Defense();
    }
    public override void exit()
    {
        patrolPositon++;
        if(patrolPositon>=m_oppssum.patrolPoint.Length)
        {
            patrolPositon = 0;
        }
    }
    
}
