using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementController : MonoBehaviour
{
    EntityTargetingSystem targetingSystem;
    //Each variable name represents a compass point
    List<VectorNode> vectorNodes;
    VectorNode debugBestVectorNode;
    public float maxObstacleVectorDistance = 0.4f;
    public float maxAllyRange = 2;
    public float maxPlayerRange = 3f;
    public bool reachedEndOfPath = false;
    private float entitySpeed = 1;
    public bool useSpeedBuff = false;
    public float speedBuffAmount = 1.5f;
    public float speedBuffDistance = 10f;


    public AnimationCurve dodgeWeightTransformationCurve;
    public AnimationCurve circleWeightTransformationCurve;
    public AnimationCurve circlingDistanceHarshnessRegulator;
    public bool dodges;
    public bool dashes;
    private bool useScript;
    private bool slowed;
    private float preSlowEntitySpeed;
    private CircleCollider2D objectCollider;
    private Path path;
    private float nextWayPointDistance = 0.4f;
    private int currentWaypoint = 0;
    private Seeker seeker;
    private Rigidbody2D rb;
    private EntityStats entityStats;
    private Vector2 AstarDirection;
    private VectorNode lastDirection;
    // Start is called before the first frame update
    private void Awake()
    {

        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        targetingSystem = GetComponent<EntityTargetingSystem>();
        objectCollider = GetComponent<CircleCollider2D>();
        entityStats = GetComponent<EntityStats>();
    }

    void Start()
    {

        useScript = true;
        slowed = false;

        InvokeRepeating("UpdatePath", 0, 0.1f);



        vectorNodes = new List<VectorNode>();
        //Instansiate dir vector to point north;
        Vector2 dir = new Vector2(0, 1);

        VectorNode previousVectorNode = new VectorNode(dir);
        lastDirection = previousVectorNode;
        vectorNodes.Add(previousVectorNode);
        debugBestVectorNode = previousVectorNode;
        for (int i = 0; i < 15; i++)
        {
            dir = Quaternion.Euler(0, 0, 22.5f) * dir;
            VectorNode iteratedVectorNode = new VectorNode(dir);
            iteratedVectorNode.leftVector = previousVectorNode;
            previousVectorNode.rightVector = iteratedVectorNode;
            vectorNodes.Add(iteratedVectorNode);
            previousVectorNode = iteratedVectorNode;
        }
        vectorNodes[vectorNodes.Count - 1].rightVector = vectorNodes[0];
        vectorNodes[0].leftVector = vectorNodes[vectorNodes.Count - 1];

        entitySpeed = entityStats.currentSpeed / 100f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!slowed)
            entitySpeed = entityStats.currentSpeed / 100f;
        if (useScript)
        {
            if (reachedEndOfPath == false)
            {
                if (path == null)
                {
                    return;
                }
                if (currentWaypoint >= path.vectorPath.Count)
                {
                    reachedEndOfPath = true;
                    return;

                }
                else
                    reachedEndOfPath = false;

                float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
                if (distance < nextWayPointDistance)
                {
                    currentWaypoint++;
                    //Debug.Log("New waypoint");
                }
                AstarDirection = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
                //Debug.Log(AstarDirection + " " + (Vector2)path.vectorPath[currentWaypoint] + " " + rb.position + " Distance: " + Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]));
                Debug.DrawRay(gameObject.transform.position, AstarDirection, Color.red, Time.deltaTime);


                if (CalculateDistanceOfRemainingWaypoints() >= 2f)
                    foreach (VectorNode vn in vectorNodes)
                    {
                        vn.weight = 0;
                    }
                if (targetingSystem.target != null)
                {
                    if (CalculateDistanceOfRemainingWaypoints() >= maxPlayerRange)
                        AddWeightsByTarget();
                    else
                    {
                        CirclePlayer();
                        //Debug.Log("Pursuading");
                    }
                    lastDirection.weight = lastDirection.weight * 2f;
                    lastDirection.leftVector.weight = lastDirection.leftVector.weight * 1.5f;
                    lastDirection.rightVector.weight = lastDirection.rightVector.weight * 1.5f;
                    if (CalculateDistanceOfRemainingWaypoints() <= maxPlayerRange + 1f)
                    {
                        if (dodges)
                            DodgeCheck();
                    }

                    RemoveWeightsByObstacle();
                    ChangeWeightByAlly();
                    Vector2 endVelocity = FindBestMovementOption();
                    if (useSpeedBuff && CalculateDistanceOfRemainingWaypoints() > speedBuffDistance)
                        endVelocity *= speedBuffAmount;
                    GetComponent<Rigidbody2D>().velocity = endVelocity;

                }
            }
        }
    }
    private float CalculateDistanceOfRemainingWaypoints()
    {
        float distance = 0;
        Vector2 LastWaypointPosition = rb.position;
        foreach (Vector2 wayPoint in path.vectorPath)
        {
            distance += Vector2.Distance(LastWaypointPosition, wayPoint);
            LastWaypointPosition = wayPoint;
        }
        return distance;
    }
    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
    void UpdatePath()
    {
        //Debug.Log(seeker);
        //Debug.Log(rb);
        //Debug.Log(targetingSystem.target);
        if (targetingSystem.target != null)
            if (seeker.IsDone())
                seeker.StartPath(rb.position, targetingSystem.target.transform.position, OnPathComplete);
    }
    Vector2 FindBestMovementOption()
    {

        VectorNode bestVectorNode = vectorNodes[0];
        float currentWeight = vectorNodes[0].weight;
        foreach (VectorNode vectorNode in vectorNodes)
        {

            if (vectorNode.weight > currentWeight)
            {
                bestVectorNode = vectorNode;
                currentWeight = vectorNode.weight;
            }
        }
        lastDirection = bestVectorNode;
        Vector2 bestMovementOption = bestVectorNode.direction * entitySpeed;
        return bestMovementOption;
    }
    private void RemoveWeightsByObstacle()
    {
        foreach (VectorNode vectorNode in vectorNodes)
        {
            float obstacleRange = vectorNode.ObstacleCheck(gameObject, maxObstacleVectorDistance, objectCollider);
            if (obstacleRange == 0)
                continue;
            vectorNode.weight = vectorNode.weight * 1 / (maxObstacleVectorDistance / obstacleRange);
            VectorNode leftNode = vectorNode;
            for (int i = 1; i < 4; i++)
            {
                leftNode.weight = vectorNode.weight * 1 / (maxObstacleVectorDistance / obstacleRange);
                leftNode = leftNode.leftVector;
            }
            VectorNode rightNode = vectorNode;
            for (int i = 1; i < 4; i++)
            {

                rightNode.weight = vectorNode.weight * 1 / (maxObstacleVectorDistance / obstacleRange);
                rightNode = rightNode.rightVector;
            }
        }
    }
    private void ChangeWeightByAlly()
    {
        foreach (VectorNode vectorNode in vectorNodes)
        {
            float allyRange = vectorNode.EnemyCheck(gameObject, maxAllyRange, objectCollider);
            if (allyRange == 0)
                continue;
            vectorNode.weight = vectorNode.weight * 1 / (maxAllyRange / allyRange);
            VectorNode leftNode = vectorNode;
            for (int i = 1; i < 5; i++)
            {
                leftNode.weight = vectorNode.weight * 1 / (maxAllyRange / allyRange);
                leftNode = leftNode.leftVector;
            }
            VectorNode rightNode = vectorNode;
            for (int i = 1; i < 5; i++)
            {
                rightNode.weight = vectorNode.weight * 1 / (maxAllyRange / allyRange);
                rightNode = rightNode.rightVector;
            }
        }
    }
    private void CirclePlayer()
    {
        foreach (VectorNode vectorNode in vectorNodes)
        {
            VectorNode StartingNode = FindClosestPointingVectorNodeToAPoint(targetingSystem.target.transform.position);
            VectorNode leftNode = StartingNode;
            for (int i = 0; i < vectorNodes.Count; i++)
            {
                leftNode.weight = circleWeightTransformationCurve.Evaluate(Vector2.Dot(AstarDirection, leftNode.direction));
                leftNode = leftNode.leftVector;
            }
        }
        foreach (VectorNode vectorNode in vectorNodes)
            if (Vector2.Distance(targetingSystem.target.transform.position, rb.transform.position) < maxPlayerRange - maxPlayerRange / 4 && Vector2.Dot(AstarDirection, vectorNode.direction) < 0)
            {
                vectorNode.weight += Mathf.Abs(Vector2.Dot(AstarDirection, vectorNode.direction)) *
                    circlingDistanceHarshnessRegulator.Evaluate(Mathf.Clamp(Vector2.Distance(targetingSystem.target.transform.position, rb.transform.position), 0, maxPlayerRange - maxPlayerRange / 4));
            }
    }

    private void AddWeightsByTarget()
    {
        VectorNode closestVectorNode = FindClosestPointingVectorNodeToAPoint(AstarDirection * 1.5f + (Vector2)gameObject.transform.position);

        closestVectorNode.weight = 1;
        VectorNode leftNode = closestVectorNode;
        for (int i = 1; i < 8; i++)
        {
            if (Vector2.Dot(AstarDirection, leftNode.direction) < -0.5f)
                leftNode.weight = 0;
            else if (Vector2.Dot(AstarDirection, leftNode.direction) < 0)
            {
                leftNode.weight = Mathf.Abs(Vector2.Dot(AstarDirection, leftNode.direction));
            }
            else
                leftNode.weight = Vector2.Dot(AstarDirection, leftNode.direction) * 1.5f;
            leftNode = leftNode.leftVector;
        }
        VectorNode rightNode = closestVectorNode;
        for (int i = 1; i < 8; i++)
        {
            if (Vector2.Dot(AstarDirection, rightNode.direction) < 0)
                rightNode.weight = 0;
            else
                rightNode.weight = Vector2.Dot(AstarDirection, rightNode.direction);
            rightNode = rightNode.rightVector;
        }

    }
    private void DodgeCheck()
    {
        if (targetingSystem.target.GetComponent<MovementScript>() && targetingSystem.target.GetComponent<Player_Animations>().attacking)
        {
            if (targetingSystem.target.GetComponent<Player_Animations>().IsRanged())
            {

                VectorNode closestVectorNode = FindClosestPointingVectorNodeToAPoint(AstarDirection * 1.5f + (Vector2)gameObject.transform.position);

                foreach (VectorNode vectorNode in vectorNodes)
                {
                    vectorNode.weight *= dodgeWeightTransformationCurve.Evaluate(Vector2.Dot(closestVectorNode.direction, vectorNode.direction));
                }

            }
            else
            {


                VectorNode closestVectorNode = FindClosestPointingVectorNodeToAPoint(AstarDirection * 1.5f + (Vector2)gameObject.transform.position);
                VectorNode oppositeNode = closestVectorNode;
                for (int i = 1; i < vectorNodes.Count / 2; i++)
                {
                    oppositeNode = oppositeNode.leftVector;
                }

                float tempWeight = 3;
                oppositeNode.weight = tempWeight;
                VectorNode leftNode = oppositeNode;
                for (int i = 1; i < 8; i++)
                {
                    tempWeight -= 0.10f;
                    leftNode.weight = tempWeight;
                    leftNode = leftNode.leftVector;
                }
                tempWeight = 1;
                VectorNode rightNode = oppositeNode;
                for (int i = 1; i < 8; i++)
                {
                    tempWeight -= 0.115f;
                    rightNode.weight = tempWeight;
                    rightNode = rightNode.rightVector;
                }
            }
            if (dashes && GetComponent<Dash>()) GetComponent<EntityAbilityManager>().CastAbility(3);
        }
    }
    VectorNode FindClosestPointingVectorNodeToAPoint(Vector2 point)
    {
        VectorNode closestPointingNode = vectorNodes[0];
        float closestDistance = Vector2.Distance(closestPointingNode.direction + (Vector2)transform.position, point);
        for (int i = 1; i < 16; i++)
        {
            float currentNodeDistance = Vector2.Distance(vectorNodes[i].direction + (Vector2)transform.position, point);
            if (currentNodeDistance < closestDistance)
            {
                closestPointingNode = vectorNodes[i];
                closestDistance = currentNodeDistance;
            }
        }
        debugBestVectorNode = closestPointingNode;
        return closestPointingNode;
    }

    private void OnDrawGizmos()
    {

        foreach (VectorNode vector in vectorNodes)
        {
            Gizmos.DrawLine(gameObject.transform.position, (vector.ReturnWeightByDirection() + (Vector2)gameObject.transform.position));
            Gizmos.DrawLine(gameObject.transform.position, (debugBestVectorNode.direction * debugBestVectorNode.weight * 2f) + (Vector2)gameObject.transform.position);

        }

    }

    private class VectorNode
    {
        public VectorNode leftVector;
        public VectorNode rightVector;
        public float weight = 0;
        public Vector2 direction { private set; get; }

        public VectorNode(Vector2 dir)
        {
            direction = dir;
        }
        public Vector2 ReturnWeightByDirection()
        {
            return direction * weight;
        }
        public float ObstacleCheck(GameObject gameObject, float maxObstacleVectorDistance, CircleCollider2D gameObjectCollider)
        {
            RaycastHit2D hit = Physics2D.Raycast((Vector2)gameObject.transform.position + new Vector2(direction.x * gameObjectCollider.radius * 1.01f + gameObjectCollider.offset.x, direction.y * gameObjectCollider.radius * 1.01f + gameObjectCollider.offset.y), direction, maxObstacleVectorDistance);
            //Debug.DrawLine(gameObject.transform.position, direction * maxObstacleVectorDistance + (Vector2)gameObject.transform.position, Color.green, Time.deltaTime);
            //Debug.Log("raycasting");
            if (hit)
            {
                if (!hit.transform.CompareTag("Player") || !hit.transform.CompareTag("Enemy"))
                {
                    return hit.distance;
                }
            }
            return 0;
        }

        public float EnemyCheck(GameObject gameObject, float maxEnemyReactDistance, CircleCollider2D gameObjectCollider)
        {
            RaycastHit2D hit = Physics2D.Raycast((Vector2)gameObject.transform.position + new Vector2(direction.x * gameObjectCollider.radius * 1.01f + gameObjectCollider.offset.x, direction.y * gameObjectCollider.radius * 1.01f + gameObjectCollider.offset.y), direction, maxEnemyReactDistance);
            //Debug.DrawLine(gameObject.transform.position, direction * maxObstacleVectorDistance + (Vector2)gameObject.transform.position, Color.green, Time.deltaTime);
            //Debug.Log("raycasting");
            if (hit)
            {
                if (hit.transform.CompareTag("Enemy"))
                {
                    return hit.distance;
                }
            }
            return 0;
        }

        public float PlayerCheck(GameObject gameObject, float maxPlayerReactDistance, CircleCollider2D gameObjectCollider)
        {
            RaycastHit2D hit = Physics2D.Raycast((Vector2)gameObject.transform.position + new Vector2(direction.x * gameObjectCollider.radius * 1.01f + gameObjectCollider.offset.x, direction.y * gameObjectCollider.radius * 1.01f + gameObjectCollider.offset.y), direction, maxPlayerReactDistance);
            //Debug.DrawLine(gameObject.transform.position, direction * maxObstacleVectorDistance + (Vector2)gameObject.transform.position, Color.green, Time.deltaTime);
            //Debug.Log("raycasting");
            if (hit)
            {
                if (hit.transform.CompareTag("Player"))
                {
                    return hit.distance;
                }
            }
            return 0;
        }
    }
    IEnumerator SetHalting(float coolDown)
    {
        useScript = false;
        yield return new WaitForSeconds(coolDown);
        useScript = true;

    }
    public void SetMoveSlow(bool trueOrFalse)
    {
        if (!slowed && trueOrFalse)
        {
            preSlowEntitySpeed = entitySpeed;
            slowed = true;
            entitySpeed = entitySpeed / 1.5f;
        }
        if (slowed && !trueOrFalse)
        {
            entitySpeed = preSlowEntitySpeed;
            slowed = false;
        }
    }
    public void Halt(bool trueOrFalse)
    {
        useScript = !trueOrFalse;
    }

    public void KnockBack(float distance, GameObject source)
    {
        Vector2 dir = transform.position - source.transform.position;
        Vector2 distanceVector = dir.normalized * distance;
        rb.AddForce(distanceVector);
    }
}
