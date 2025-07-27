using System.Collections;
using GOAP;
using UnityEngine;

public class MineStone : Action
{
    private Transform stoneTransform;

    public MineStone(WorldState worldState, Transform stoneTransform) : base("MineStone", worldState)
    {
        this.stoneTransform = stoneTransform;
    }
    public void Init()
    {
        prerequisites.Add(new Prerequisite("StoneNearby"));
        effects.Add(new Effect("HasStone"));
    }

    public override IEnumerator DoActionWithMovement(Movement movement)
    {
        yield return movement.MoveToDestination(stoneTransform.position);
        yield return new WaitForSeconds(5f);
        ResourceManager.Instance.AddStone(2);
        worldState.AddState("HasStone");
    }
}