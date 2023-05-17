using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Cohesion", fileName = "Cohesion")]
public class CohesionBehaviour : Behaviour
{
    public override Vector2 CalculateMove(FlockAgent agent,
        List<Transform> context, Flock flock)
    {
        if(context.Count == 0)
        {
            return Vector2.zero;
        }
        Vector2 cohesionMove = Vector2.zero;

        int count = 0;
        foreach(Transform item in context)
        {
            cohesionMove += (Vector2) item.position;
            count++;
        }
        if (count != 0)
        {
            cohesionMove /= count;
        }
        //direction from a to b = b - a
        cohesionMove = cohesionMove - (Vector2)agent.transform.position;
        cohesionMove.Normalize();

        return cohesionMove;
    }
}