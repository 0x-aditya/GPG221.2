using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GOAP;

public class Harvest : Action
{
    private Transform farmTransform;

    public Harvest(WorldState worldState, Transform farmTransform) : base("Harvest", worldState)
    {
        this.farmTransform = farmTransform;
    }
    public void Init()
    {
        prerequisites.Add(new Prerequisite("FarmNearby"));
        effects.Add(new Effect("HasFood"));
    }
    public override void DoAction()
    {
        ResourceManager.Instance.AddFood(5);
        worldState.AddState("HasFood");
    }
    public override IEnumerator DoActionWithMovement(Movement movement)
    {
        yield return movement.MoveToDestination(farmTransform.position);
        yield return new WaitForSeconds(7f);
        ResourceManager.Instance.AddFood(5);
        worldState.AddState("HasFood");
    }
}
