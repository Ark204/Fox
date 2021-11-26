using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stiff : FoxState
{
    public Stiff(StateController stateController) : base(stateController) { }
    private float lastTime;
    public override void enter()
    {
        //����hurt����
        Debug.Log("���ǣ�����Ӳֱ����");
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
        Debug.Log("���ǣ�Ӳֱ����");
    }
}
