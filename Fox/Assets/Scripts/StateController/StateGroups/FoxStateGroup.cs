using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Ö÷½Ç×´Ì¬×é
public class FoxStateGroup : IStateGroup
{
    public override void InitDictionary(StateController stateController)
    {
        states.Add("Idle", new Idle(stateController));
        states.Add("Run", new Run(stateController));
        states.Add("Sprint", new Sprint(stateController));
        states.Add("Up", new Up(stateController));
        states.Add("Down", new Down(stateController));
        states.Add("Climb", new Climb(stateController));
        states.Add("Cut", new Cut(stateController));
        states.Add("Hurt", new Hurt(stateController));
        states.Add("SilkJump", new SilkJump(stateController));
        states.Add("Stiff", new Stiff(stateController));
        states.Add("Defense", new Defense(stateController));
        firststate = states["Idle"];
    }
}
