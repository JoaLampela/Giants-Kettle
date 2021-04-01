using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class KIRBYMOVETEST : MonoBehaviour
{
    EntityTargetingSystem targetingSystem;
    //Each variable name represents a compass point
    List<VectorNode> vectorNodes;
    VectorNode debugBestVectorNode;
    float maxObstacleVectorDistance = 0.2f;
    float maxAllyRange = 1;
    CircleCollider2D objectCollider;
    Path path;
    float nextWayPointDistance = 0.5f;
    int currentWaypoint = 0;
    public bool reachedEndOfPath = false;
    Seeker seeker;
    Rigidbody2D rb;
    Vector2 AstarDirection;
    public float entitySpeed = 15;

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        targetingSystem = GetComponent<EntityTargetingSystem>();

        InvokeRepeating("UpdatePath", 0, 0.1f);



        objectCollider = GetComponent<CircleCollider2D>();
        vectorNodes = new List<VectorNode>();
        //Instansiate dir vector to point north;
        Vector2 dir = new Vector2(0, 1);

        VectorNode previousVectorNode = new VectorNode(dir);
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


    }

    // Update is called once per frame
    void Update()
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

        AstarDirection = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWayPointDistance)
            currentWaypoint++;
        Debug.DrawRay(gameObject.transform.position, AstarDirection, Color.red, Time.deltaTime);


        if (path.vectorPath.Count - currentWaypoint >= 4)
            foreach (VectorNode vn in vectorNodes)
            {
                vn.weight = 0;
            }
        if (targetingSystem.target != null)
        {
            if (path.vectorPath.Count - currentWaypoint >= 4)
                AddWeightsByTarget(2);
            /*else
            {
                CirclePlayer();
            }*/
            if (path.vectorPath.Count - currentWaypoint >= 5)
            {
                DodgeCheck();
            }

            RemoveWeightsByObstacle();
            ChangeWeightByAlly();
            GetComponent<Rigidbody2D>().velocity = FindBestMovementOption();

        }
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
        Vector2 bestMovementOption = bestVectorNode.direction * entitySpeed;
        return bestMovementOption;
    }
    private void RemoveWeightsByObstacle()
    {
        foreach (VectorNode vectorNode in vectorNodes)
        {
            float obstacleRange = vectorNode.EnemyCheck(gameObject, maxObstacleVectorDistance, objectCollider);
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
            float allyRange = vectorNode.ObstacleCheck(gameObject, maxAllyRange, objectCollider);
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
            float obstacleRange = vectorNode.ObstacleCheck(gameObject, maxObstacleVectorDistance, objectCollider);
            if (obstacleRange == 0)
                continue;
            vectorNode.weight = 0.2f;
            float tempweight = 0.2f;
            VectorNode leftNode = vectorNode;
            for (int i = 1; i < 5; i++)
            {
                tempweight += 0.2f;
                leftNode.weight = tempweight;
                leftNode = leftNode.leftVector;
            }
            VectorNode rightNode = vectorNode;
            tempweight = 0.2f;
            for (int i = 1; i < 5; i++)
            {

                tempweight += 0.2f;
                rightNode.weight = tempweight;
                rightNode = rightNode.rightVector;
            }
        }
    }

    private void AddWeightsByTarget(float desiredDistance)
    {
        VectorNode closestVectorNode = FindClosestPointingVectorNodeToAPoint(AstarDirection * 1.5f + (Vector2)gameObject.transform.position);

        float tempWeight = 1;
        closestVectorNode.weight = tempWeight;
        VectorNode leftNode = closestVectorNode;
        for (int i = 1; i < 8; i++)
        {
            tempWeight -= 0.10f;
            leftNode.weight = tempWeight;
            leftNode = leftNode.leftVector;
        }
        tempWeight = 1;
        VectorNode rightNode = closestVectorNode;
        for (int i = 1; i < 8; i++)
        {
            tempWeight -= 0.115f;
            rightNode.weight = tempWeight;
            rightNode = rightNode.rightVector;
        }

    }
    private void DodgeCheck()
    {
        if (targetingSystem.target.GetComponent<MovementScript>() && targetingSystem.target.GetComponent<MovementScript>().attacking)
        {


            VectorNode closestVectorNode = FindClosestPointingVectorNodeToAPoint(AstarDirection * 1.5f + (Vector2)gameObject.transform.position);
            VectorNode oppositeNode = closestVectorNode;
            for (int i = 1; i < vectorNodes.Count / 2; i++)
            {
                oppositeNode = oppositeNode.leftVector;
            }

            float tempWeight = 1;
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
}