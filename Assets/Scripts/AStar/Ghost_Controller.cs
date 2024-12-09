using System.Collections.Generic;
using UnityEngine;

public class Ghost_Controller : MonoBehaviour
{
    public Node currentNode;
    public List<Node> path = new List<Node>();

    private void Start()
    {
        currentNode = AStarManager.instance.FindNearestNode(transform.position);
    }

    private void OnDrawGizmos()
    {
        if (path.Count > 0)
        {
            Gizmos.color = Color.blue;
            for (int i = 1; i < path.Count; i++)
            {
                Gizmos.DrawLine(path[i].transform.position, path[i-1].transform.position);
            }
        }
    }

    public void CreatePath()
    {
        if (path.Count > 0)
        {
            int x = 0;

            // Target position of the next node
            Vector3 targetPosition = new Vector3(path[x].transform.position.x, path[x].transform.position.y, path[x].transform.position.z);

            // Move towards the target
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, 3 * Time.deltaTime);

            // Calculate the direction to the target
            Vector3 direction = (targetPosition - transform.position).normalized;

            // Rotate to face the target
            if (direction != Vector3.zero) // Ensure direction is valid
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5); // Smoothly rotate
            }

            // Check if close to the target
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                currentNode = path[x];
                path.RemoveAt(x);
            }
        }
        else
        {
            Node[] nodes = FindObjectsOfType<Node>();
            while (path == null || path.Count == 0)
            {
                currentNode = AStarManager.instance.FindNearestNode(transform.position);
                path = AStarManager.instance.GeneratePath(currentNode, nodes[Random.Range(0, nodes.Length)]);
            }
        }
    }
}
