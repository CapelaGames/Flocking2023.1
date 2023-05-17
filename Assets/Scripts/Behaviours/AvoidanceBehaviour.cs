using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Avoidance")]
public class AvoidanceBehaviour : Behaviour
{
    public override Vector2 CalculateMove(FlockAgent agent,
                                   List<Transform> context,
                                   Flock flock)
    {
        if(context.Count == 0)
        {
            return Vector2.zero;
        }

        Vector2 avoidanceMove = Vector2.zero;

        int count = 0;
        foreach(Transform item in context) 
        {
            Vector3 directionFromItem = agent.transform.position - item.position;
            if (directionFromItem.sqrMagnitude <= flock.squareAvoidanceRadius)
            {
                avoidanceMove += (Vector2)directionFromItem;
                count++;
            }
        }
        if(count != 0) 
        {
            avoidanceMove /= count;
        }
        return avoidanceMove;
    }
}
