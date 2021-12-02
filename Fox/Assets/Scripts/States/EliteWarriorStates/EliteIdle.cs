using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliteIdle : WarriorState
{
    public EliteIdle(StateController stateController) : base(stateController) { }
    private float timer;
    public override void enter()
    {
        m_animator.Play("Idle");
    }
    public override void update()
    {
        timer += Time.fixedDeltaTime;
        if (timer >= m_oppssum.idleTime)
        {
            m_stateController.ChangeState("WarRun");
        }
        Defense();
        Chase();
    }
    public override void exit()
    {
        timer = 0;
    }
}
public class EliteRun : WarriorState
{
    public EliteRun(StateController stateController) : base(stateController) { }
    private int patrolPositon;
    public override void enter()
    {
        //≤•∑≈≈‹≤Ω∂Øª≠
        m_animator.Play("Walk");
    }
    public override void update()
    {
        FlipTo(m_oppssum.patrolPoint[patrolPositon]);
        m_transform.position = Vector2.MoveTowards(m_transform.position,
            m_oppssum.patrolPoint[patrolPositon].position, m_oppssum.speed * Time.fixedDeltaTime);
        if (Vector2.Distance(m_transform.position, m_oppssum.patrolPoint[patrolPositon].position) <= 1.5f)
        {
            m_stateController.ChangeState("WarIdle");
        }
        Defense();
        Chase();
    }
    public override void exit()
    {
        patrolPositon++;
        if (patrolPositon >= m_oppssum.patrolPoint.Length)
        {
            patrolPositon = 0;
        }
    }
}
