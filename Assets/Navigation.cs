using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Navigation : MonoBehaviour
{
    [SerializeField] GameObject map;
    [SerializeField] GameObject ball;
    [SerializeField] GameObject emptyObject;
    GameObject currentTarget;
    GameObject previousTarget;

    Rigidbody rb;
    NavMeshAgent agent;

    float distanceToTarget;
    bool ontarget;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        GenerateRandomDestinationOnMap();
        emptyObject = new GameObject();
    }

    // Update is called once per frame
    void Update()
    {
        // get a floating value of the distance between the target and the object
        distanceToTarget = Vector3.Dot(transform.position, currentTarget.transform.position);
        if (distanceToTarget > 0)
        {
            MoveToTarget(currentTarget);
        }
        else if (distanceToTarget <= 0)
        {
            Debug.Log(distanceToTarget);
            if (currentTarget != null)
            {
                
                // Destroy the specified GameObject
                previousTarget = currentTarget;
                GenerateRandomDestinationOnMap();
                Destroy(previousTarget);
            }
            else
            {
                Debug.LogWarning("Object to delete is null.");
            }
        }
    }
    // generate a random point on the map
    void GenerateRandomDestinationOnMap()
    {
        Vector3 randomLocation = new Vector3(Random.Range(0, map.transform.lossyScale.x),
                                             Random.Range(0, map.transform.lossyScale.y),
                                             Random.Range(0, map.transform.lossyScale.z));
        currentTarget = Instantiate(emptyObject, randomLocation, Quaternion.identity);
    }
    // Move the agent to the targeted position
    void MoveToTarget(GameObject target)
    {
        agent.SetDestination(target.transform.position);
    }

    Vector2 Get2DObjectDistanceToTarget(Vector3 origin, Vector3 target)
    {
        return new Vector2 (0,0);
    }


}
