using System.Collections;
using GOAP;
using UnityEngine;

public class CutTree : Action
{
    public Transform treeTransform;

    public CutTree(WorldState worldState, Transform treeTransform) : base("CutTree", worldState)
    {
        this.treeTransform = treeTransform;
    }
    public void Init()
    {
        prerequisites.Add(new Prerequisite("TreeNearby"));
        effects.Add(new Effect("HasWood"));
    }
    public override IEnumerator DoActionWithMovement(Movement movement)
    {
        yield return movement.MoveToDestination(treeTransform.position);
        yield return new WaitForSeconds(3f);
        ResourceManager.Instance.AddWood(7);
        worldState.AddState("HasWood");
    }
}