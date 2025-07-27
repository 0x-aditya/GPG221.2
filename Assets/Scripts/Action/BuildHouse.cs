using GOAP;
using UnityEngine;

public class BuildHouse : Action
{
    GameObject housePrefab;
    Vector3 buildPosition;
    int woodCost = 5;
    int stoneCost = 3;
    int foodCost = 2; 

    public BuildHouse(WorldState worldState, GameObject housePrefab, Vector3 buildPosition) 
        : base("BuildHouse", worldState)
    {
        this.housePrefab = housePrefab;
        this.buildPosition = buildPosition;
    }

    public void Init()
    {
        prerequisites.Add(new Prerequisite("HasWood"));
        prerequisites.Add(new Prerequisite("HasStone"));
        prerequisites.Add(new Prerequisite("HasFood"));
        effects.Add(new Effect("HasHouse"));
    }

    public override void DoAction()
    {
        if (ResourceManager.Instance.HasResources(woodCost, stoneCost, foodCost))
        {
            ResourceManager.Instance.UseResources(woodCost, stoneCost, foodCost);
            Object.Instantiate(housePrefab, buildPosition, Quaternion.identity);
            worldState.AddState("HasHouse");
        }
        else
        {
            Debug.Log("Not enough resources to build house!");
        }
    }
}