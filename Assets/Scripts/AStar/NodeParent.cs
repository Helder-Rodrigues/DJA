using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UIElements;

public class NodeParent : MonoBehaviour
{
    [SerializeField] private GameObject NodePreFab;
    private Vector2 StartPosition = new(-2, -12);
    private Vector2 EndPosition = new(-84.5f, 75);

    public List<Node> nodes = new List<Node>();

    private void Awake()
    {
        GenerateGrid();
    }

    public void GenerateGrid()
    {
        if (NodePreFab == null)
            return;

        float spacing = 3f;
        float yPosition = -6.5f;

        int columns = Mathf.CeilToInt(Mathf.Abs(EndPosition.x - StartPosition.x) / spacing);
        int rows = Mathf.CeilToInt(Mathf.Abs(EndPosition.y - StartPosition.y) / spacing);

        for (int row = 0; row <= rows; row++)
        {
            for (int col = 0; col <= columns; col++)
            {
                float xPos = StartPosition.x - col * spacing;
                float zPos = StartPosition.y + row * spacing;

                Vector3 nodePosition = new Vector3(xPos, yPosition, zPos);
                GameObject nodeObj = Instantiate(NodePreFab, nodePosition, Quaternion.identity, transform);

                Node nodeComponent = nodeObj.GetComponent<Node>();
                if (nodeComponent != null)
                {
                    nodeObj.name += row + "_" + col;
                    nodeComponent.NodeParent = this;
                    nodes.Add(nodeComponent);
                }
            }
        }

        foreach (Node node in nodes)
        {
            if (node != null)
                AddConnections(node);
        }
    }

    private void AddConnections(Node node)
    {
        float connectionRange = 3.1f;

        foreach (Node otherNode in nodes)
        {
            if (otherNode != null && otherNode != node)
                // Check if the other node is within connection range
                if (Vector3.Distance(node.transform.position, otherNode.transform.position) <= connectionRange)
                    // Check for obstacles between the two nodes
                    if (!IsObstacleBetween(node.transform.position, otherNode.transform.position))
                        node.connections.Add(otherNode); // Add to the connections list if no obstacle
        }
    }

    private bool IsObstacleBetween(Vector3 start, Vector3 end)
    {
        // Direction from the current node to the other node
        Vector3 direction = (end - start).normalized;
        float distance = Vector3.Distance(start, end);

        // Perform the raycast
        if (Physics.Raycast(start, direction, out RaycastHit hit, distance))
        {
            // Check if the hit object is a "Wall_Ghost"
            if (hit.collider.name.ToLower() == "wall_ghost")
            {
                return true; // An obstacle is detected
            }
        }

        return false; // No obstacle detected
    }

}
