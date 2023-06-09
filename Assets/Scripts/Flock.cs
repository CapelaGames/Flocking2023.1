using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
/*    [Header("HEADER")]
    public float ahhhh;
    [Space(500)]
    public float ahhhh1;
    [Range(0f, 1f)]
    public float ahhhh2;
*/
    public FlockAgent agentPrefab;
    //Like an Array, but we can change the size of it
    public List<FlockAgent> agents;

    public Behaviour behaviour;

    [Range(10,500)]
    public int startingCount = 250;
    public float agentDensity = 0.08f;

    [Range(1f,10f)]
    public float contextRadius = 1.5f;
    [Range(0f, 1f)]
    public float avoidanceRadius = 0.75f;

    public float squareAvoidanceRadius;

    public float speed = 1f;

    void Start()
    {
        squareAvoidanceRadius = avoidanceRadius * avoidanceRadius;
        for(int i = 0; i < startingCount; i++)
        {
            Vector2 randomLocation = Random.insideUnitCircle
                                        * startingCount
                                        * agentDensity;
            //get a prefab
            //add the prefab to the scene
            FlockAgent newAgent = Instantiate(
                agentPrefab, //prefab we are spawning
                randomLocation, // random location
                Quaternion.Euler(new Vector3(0,0,
                                Random.Range(0,360f))),
                                //random rotation
                transform); // parent

            newAgent.name = "Agent " + (i + 1);
            agents.Add(newAgent);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach(FlockAgent agent in agents)
        {
            List<Transform> context = GetNearbyObjects(agent);

            Vector2 move = behaviour.CalculateMove(agent,
                                                    context,
                                                    this);

            agent.Move(move * speed);
        }
    }

    private List<Transform> GetNearbyObjects(FlockAgent agent)
    {
        List<Transform> context = new List<Transform>();
        Collider2D[] contextColliders = 
          Physics2D.OverlapCircleAll(agent.transform.position,
                                    contextRadius);
        
        foreach(Collider2D foundCollider in contextColliders)
        {
            if(foundCollider != agent.agentCollider)
            {
                context.Add(foundCollider.transform);
            }
        }

        return context;

    }
}
