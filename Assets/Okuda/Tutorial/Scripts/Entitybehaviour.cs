using Bolt;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entitybehaviour : Bolt.EntityBehaviour
{
    public override void Attached()
    {
        base.Attached();
        Debug.Log("Attached");
    }

    public override void ControlGained()
    {
        base.ControlGained();
        Debug.Log("ControlGained");
    }

    public override void ControlLost()
    {
        base.ControlLost();
        Debug.Log("ControlLost");
    }

    public override void Detached()
    {
        base.Detached();
        Debug.Log("Detached");
    }

    public override void ExecuteCommand(Command command, bool resetState)
    {
        base.ExecuteCommand(command, resetState);
        Debug.Log("ExecuteCommand");
    }

    public override void Initialized()
    {
        base.Initialized();
        Debug.Log("Initialized");
    }

    public override void MissingCommand(Command previous)
    {
        base.MissingCommand(previous);
        Debug.Log("MissingCommand");
    }

    public override void SimulateController()
    {
        base.SimulateController();
        Debug.Log("SimulateController");
    }

    public override void SimulateOwner()
    {
        base.SimulateOwner();
        Debug.Log("SimulateOwner");
    }


}
