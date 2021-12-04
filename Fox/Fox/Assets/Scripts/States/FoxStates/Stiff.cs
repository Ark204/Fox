using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stiff : FoxState
{
    public Stiff(StateController stateController) : base(stateController) { }
    private float lastTime;
    public override void enter()
    {
        //播放hurt动画
        Debug.Log("主角：播放硬直动画");
        lastTime = m_fox.stiffTime;
    }
    public override void update()
    {
        lastTime -= Time.fixedDeltaTime;
        if(lastTime<0)
        {
            m_stateController.ChangeState("Idle");
        }
    }
    public override void exit()
    {
        Debug.Log("主角：硬直结束");
    }
}
