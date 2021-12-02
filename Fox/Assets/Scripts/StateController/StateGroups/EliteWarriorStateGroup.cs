using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//¾«Ó¢ÎäÊ¿×´Ì¬×é
public class EliteWarriorStateGroup : IStateGroup
{
    public override void InitDictionary(StateController stateController)
    {
        states.Add("WarIdle", new EliteIdle(stateController));
        states.Add("WarRun", new EliteRun(stateController));
        states.Add("WarChase", new WarChase(stateController));
        states.Add("WarAttack", new WarAttack(stateController));
        states.Add("WarDefense", new WarDefense(stateController));
        states.Add("WarHurt", new WarHurt(stateController));
        states.Add("WarDeath", new WarDeath(stateController));

        firststate = states["WarIdle"];
    }
}
