using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IStateGroup : MonoBehaviour
{
    //×´Ì¬×é×Öµä
    public Dictionary<string, IState> states = new Dictionary<string, IState>();
    public IState firststate;
    public virtual void InitDictionary(StateController stateController)
    { }
}
