using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Flock/Behaviour/Composite", fileName = "Composite")]
public class CompositeBehaviour : Behaviour
{
    [System.Serializable]
    public struct BehaviourGroup
    {
        public Behaviour behaviour;
        public float weights;
    }

    public BehaviourGroup[] behaviours;

    public override Vector2 CalculateMove(FlockAgent agent,
                                    List<Transform> context,
                                    Flock flock)
    {
        Vector2 move = Vector2.zero;

        foreach (BehaviourGroup behaviour in behaviours)
        {
            Vector2 patrialMove = behaviour.behaviour.CalculateMove(agent, context, flock);

            if (patrialMove == Vector2.zero) continue;

            patrialMove.Normalize();
            patrialMove *= behaviour.weights;

            move += patrialMove;
        }

        return move;
    }
}
