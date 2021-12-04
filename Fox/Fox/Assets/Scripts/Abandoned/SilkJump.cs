using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SilkJump : FoxState
{
    public SilkJump(StateController stateController) : base(stateController) { }
    public override void enter()
    {
        m_animator.Play("somersault");
    }
    public override void update()
    {
        
    }
    public override void exit()
    {
        
    }
}
