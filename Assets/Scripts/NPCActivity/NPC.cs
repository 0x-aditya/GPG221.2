using GOAP;
using UnityEngine;
using System.Collections;
public class NPC : MonoBehaviour
{
    public WorldState worldState;
    public GOAPManager planner;
    public GameObject housePrefab;
    public Transform buildPosition;
    private Movement movement;

    private GameObject tree;
    private GameObject stone;
    private GameObject farm;
    
    private CutTree cutTree;
    private MineStone mineStone;
    private Harvest harvest;
    private BuildHouse buildHouse;

    public Transform[] patrolPoints;
    private int patrolIndex = 0;
    private bool isBuildingHouse = false;
    private float speed = 2f;
    
    void Start()
    {
        worldState = new WorldState();
        planner = new GOAPManager(worldState);

        tree = GameObject.FindGameObjectsWithTag("Tree")[0];
        stone = GameObject.FindGameObjectsWithTag("Stone")[0];
        farm = GameObject.FindGameObjectsWithTag("Farm")[0];
        
        cutTree = new CutTree(worldState, tree.transform); cutTree.Init();
        mineStone = new MineStone(worldState, stone.transform); mineStone.Init();
        harvest = new Harvest(worldState, farm.transform); harvest.Init();
        buildHouse = new BuildHouse(worldState, housePrefab, buildPosition.position); buildHouse.Init();

        planner.AddAction(cutTree);
        planner.AddAction(mineStone);
        planner.AddAction(harvest);
        planner.AddAction(buildHouse);

        worldState.AddState("TreeNearby");
        worldState.AddState("StoneNearby");
        worldState.AddState("FarmNearby");
        
        movement = GetComponent<Movement>();

    }

    void Update()
    {
        if (!isBuildingHouse && patrolPoints != null && patrolPoints.Length > 0)
        {
            Patrol();
        }
    }

    void Patrol()
    {
        Transform target = patrolPoints[patrolIndex];
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            patrolIndex = (patrolIndex + 1) % patrolPoints.Length;
        }
    }

    public void BuildHouseForButton()
    {
        if (!isBuildingHouse)
        {
            isBuildingHouse = true;
            StartCoroutine(PlanToBuildHouse());
        }
    }
    
    public void MineStoneForButton()
    {
        if (!isBuildingHouse && !worldState.HasState("HasStone"))
        {
            isBuildingHouse = true;
            StartCoroutine(PlanToMineStone());
        }
    }
    IEnumerator PlanToMineStone()
    {
        if (!worldState.HasState("HasStone"))
        {
            while (!ResourceManager.Instance.HasResources(0, 3, 0))
            {
                var a = planner.GetNextAction("HasStone");
                if (a is MineStone mine)
                    yield return mine.DoActionWithMovement(movement);
                Destroy(stone);
            }
        }
        isBuildingHouse = false;
    }

    IEnumerator PlanToBuildHouse()
    {
        if (!worldState.HasState("HasHouse"))
        {
            while (!ResourceManager.Instance.HasResources(5, 0, 0))
            {
                var a = planner.GetNextAction("HasWood");
                if (a is CutTree cut)
                    yield return cut.DoActionWithMovement(movement);
                Destroy(tree);
            }

            while (!ResourceManager.Instance.HasResources(0, 3, 0))
            {
                var a = planner.GetNextAction("HasStone");
                if (a is MineStone mine)
                    yield return mine.DoActionWithMovement(movement);
                Destroy(stone);
            }
    
            while (!ResourceManager.Instance.HasResources(0, 0, 2))
            {
                var a = planner.GetNextAction("HasFood");
                if (a is Harvest farmLand)
                    yield return farmLand.DoActionWithMovement(movement);
                Destroy(farm);
            }

            yield return movement.MoveToDestination(buildPosition.position);

            var build = planner.GetNextAction("HasHouse");
            if (build is BuildHouse buildHouse)
            {
                buildHouse.DoAction();
            }
        }
        isBuildingHouse = false;
    }
}