using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KIRBYMOVETEST : MonoBehaviour
{
    EntityTargetingSystem targetingSystem;
    //Each variable name represents a compass point
    List<VectorNode> vectorNodes;
    VectorNode debugBestVectorNode;
    float maxObstacleVectorDistance = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
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

        targetingSystem = GetComponent<EntityTargetingSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (targetingSystem.target != null)
        {
            AddWeightsByTarget(2);
            RemoveWeightsByObstacle();

            GetComponent<Rigidbody2D>().velocity = FindBestMovementOption();
        }
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
        Vector2 bestMovementOption = bestVectorNode.direction * 10f;
        return bestMovementOption;
    }
    private void RemoveWeightsByObstacle()
    {
        foreach (VectorNode vectorNode in vectorNodes)
        {
            float obstacleRange = vectorNode.ObstacleCheck(gameObject, maxObstacleVectorDistance);
            if (obstacleRange == 0)
                continue;
            Debug.Log("obstacle range: " + obstacleRange);
            vectorNode.weight = vectorNode.weight * 1 / (maxObstacleVectorDistance / obstacleRange);
            VectorNode leftNode = vectorNode;
            float counter = 2;
            for (int i = 1; i < 4; i++)
            {
                leftNode.weight = vectorNode.weight * 1 / (maxObstacleVectorDistance / obstacleRange);
                leftNode = leftNode.leftVector;
                counter += 2;
            }
            VectorNode rightNode = vectorNode;
            counter = 2;
            for (int i = 1; i < 4; i++)
            {

                rightNode.weight = vectorNode.weight * 1 / (maxObstacleVectorDistance / obstacleRange);
                rightNode = rightNode.rightVector;
                counter += 2;
            }


        }
    }

    private void AddWeightsByTarget(float desiredDistance)
    {
        VectorNode closestVectorNode = FindClosestPointingVectorNodeToAPoint(targetingSystem.target.transform.position);
        if (Vector2.Distance(gameObject.transform.position, targetingSystem.target.transform.position) >= desiredDistance)
        {
            float tempWeight = 1;
            closestVectorNode.weight = tempWeight;
            VectorNode leftNode = closestVectorNode;
            for (int i = 1; i < 8; i++)
            {
                tempWeight -= 0.115f;
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
        public float ObstacleCheck(GameObject gameObject, float maxObstacleVectorDistance)
        {

            RaycastHit2D hit = Physics2D.Raycast((Vector2)gameObject.transform.position, direction, maxObstacleVectorDistance);
            //Debug.DrawLine(gameObject.transform.position, direction * maxObstacleVectorDistance + (Vector2)gameObject.transform.position, Color.green, Time.deltaTime);
            //Debug.Log("raycasting");
            if (hit && (!hit.transform.CompareTag("Player") || !hit.transform.CompareTag("Enemy")))
            {
                return hit.distance;
            }
            return 0;
        }
    }
}
