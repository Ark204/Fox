using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Onsky : FoxState
{
    public Onsky(StateController stateController) : base(stateController) { }
    public override void enter() { }
    public override void update()
    {
        base.update();
        if (m_fox.jumpPressed && m_fox.jumpCount > 0)
        {
            //���������Ծ״̬
            m_stateController.ChangeState("Up");
            m_fox.jumpPressed = false;
        }
    }
}
