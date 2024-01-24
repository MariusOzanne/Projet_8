
using UnityEngine;
using UnityEngine.AI;

public class Navigation : MonoBehaviour
{
    [SerializeField] GameObject map;
    [SerializeField] GameObject ball;
    [SerializeField] GameObject startPoint;
    [SerializeField] float timeUntilRetreival = 30.0f;
    Vector3 currentTargetPosition;

    float lastRetreval;

    Rigidbody rb;
    NavMeshAgent agent;

    float distanceToTarget;
    bool chasingBall;
    bool carryingBall;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        GenerateRandomDestinationOnMap();
    }

    // Update is called once per frame
    void Update()
    {
        // get a floating value of the distance between the target and the object
        DistanceToTargetWithVector2(currentTargetPosition, transform.position);
        if (distanceToTarget > 0)
        {
            MoveToTarget(currentTargetPosition);
        }
        else if (distanceToTarget <= 0 && !chasingBall)
        {
            // checks if the object is reseting the ball to its original position
            if (carryingBall)
            {
                freeBall(ball);
                // reset the duration of the last retrieval
                lastRetreval = Time.time;
            }
            //check if the last retrieval is greater than n seconds, 
            if (Time.time - lastRetreval >= timeUntilRetreival)
            {
                // chase the ball and set chasingBall to true
                currentTargetPosition = ball.transform.position;
                chasingBall = true;
            }
            else
            {
                GenerateRandomDestinationOnMap();
            }


        }
        else if (distanceToTarget <= 0 && chasingBall)
        {
            CarryBall(ball);
            currentTargetPosition = startPoint.transform.position;
            chasingBall = false;
            carryingBall = true;
        }
    }

    // generate a random point on the map
    void GenerateRandomDestinationOnMap()
    {
        
        float randomXPosition = Random.Range(-map.transform.localScale.x / 2, map.transform.localScale.x / 2);
        float randomZPosition = Random.Range(-map.transform.localScale.z / 2, map.transform.localScale.z / 2);

        Vector3 randomLocation = new Vector3(map.transform.position.x + randomXPosition,
                                             map.transform.position.y,
                                             map.transform.position.z + randomZPosition);
        currentTargetPosition = randomLocation;

        carryingBall = false;
    }

    // Move the agent to the targeted position
    void MoveToTarget(Vector3 targetPosition)
    {
        agent.SetDestination(targetPosition);
    }

    void DistanceToTargetWithVector2(Vector3 targetPosition, Vector3 objectPosition)
    {
        // find the distance between two Vector3, regardless of the Y axis
        distanceToTarget = Vector2.Distance(new Vector2(targetPosition.x, targetPosition.z),
                                            new Vector2(objectPosition.x, objectPosition.z));
    }

    void CarryBall(GameObject target)
    {
        if (target != null && gameObject != null)
        {
            target.GetComponent<Collider>().enabled = false;
            target.GetComponent<Rigidbody>().useGravity = false;
            target.transform.SetParent(transform);
            if (target.transform.position.y <= 6f )
            {
                target.transform.position.Set(target.transform.position.x,
                                              target.transform.position.y + 0.2f,
                                              target.transform.position.z);
            }
        }
        else
        {
            Debug.LogError("Make sure both GameObjects exist");
        }

    }

    void freeBall(GameObject target)
    {
        // check if the target exists
        if (target != null)
        {
            target.GetComponent<SphereCollider>().enabled = true;
            target.GetComponent<Rigidbody>().useGravity = true;
            target.transform.SetParent(null);
        }
        else
        {
            Debug.LogError("target does not exist");
        }
    }
}
