using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarAttack : WarriorState
{
    public WarAttack(StateController stateController) : base(stateController) { }
    private float timer;
    public override void enter()
    {
    }
    public override void update()
    {
        timer += Time.fixedDeltaTime;
        if (m_oppssum.cutTime / 2 - Time.fixedDeltaTime < timer && timer < m_oppssum.cutTime / 2)
        {
            //调用敌人受到生效攻击函数
            if (m_oppssum.target != null)
            {
                m_oppssum.target.GetComponent<StateController>().StateEvent();
            }
        }
        if (timer >= m_oppssum.cutTime)
        {
            m_stateController.ChangeState("WarChase");
        }
    }
    public override void exit()
    {
        timer = 0;
    }

}
