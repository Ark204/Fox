using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ÆÕÍ¨ÎäÊ¿×´Ì¬×é
public class WarriorStateGroup : IStateGroup
{
    public override void InitDictionary(StateController stateController)
    {
        states.Add("WarIdle", new WarIdle(stateController));
        states.Add("WarRun", new WarRun(stateController));
        states.Add("WarChase", new WarChase(stateController));
        states.Add("WarAttack", new WarAttack(stateController));
        states.Add("WarDefense", new WarDefense(stateController));
        states.Add("WarHurt", new WarHurt(stateController));
        states.Add("WarDeath", new WarDeath(stateController));

        firststate = states["WarIdle"];
    }
}
