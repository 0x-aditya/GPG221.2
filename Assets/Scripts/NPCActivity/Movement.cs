using System.Collections;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float speed;
    Vector3 finalDestination;

    public IEnumerator MoveToDestination(Vector3 destination)
    {
        finalDestination = destination;
        while (Vector3.Distance(transform.position, finalDestination) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, finalDestination, speed * Time.deltaTime);
            yield return null;
        }
    }
}