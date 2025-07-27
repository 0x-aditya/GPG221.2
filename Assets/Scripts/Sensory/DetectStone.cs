using System.Collections.Generic;
using UnityEngine;

public class DetectStone : MonoBehaviour
{
    [SerializeField] private float detectionRadius = 5f;
    [SerializeField] private NPC npc;
    private List<GameObject> detectedStones = new List<GameObject>();

    void Update()
    {
        DetectNearbyStones();
    }

    private void DetectNearbyStones()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius);
        detectedStones.Clear();

        bool foundStone = false;
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Stone"))
            {
                foundStone = true;
                break;
            }
        }

        if (npc != null && npc.worldState != null)
        {
            if (foundStone)
            {
                npc.worldState.AddState("StoneNearby");
            }
            else
            {
                npc.worldState.RemoveState("StoneNearby");
            }
        }
    }
}
