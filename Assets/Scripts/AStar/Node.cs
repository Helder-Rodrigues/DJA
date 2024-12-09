using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public NodeParent NodeParent;
    public Node cameFrom;
    public List<Node> connections;

    public float gScore;
    public float hScore;

    public float FScore()
    {
        return gScore + hScore;
    }

    //draw all connections
    private void OnDrawGizmos()
    {
        if (false)//connections.Count > 0)
        {
            Gizmos.color = Color.blue;
            for (int i = 0; i < connections.Count; i++)
            {
                Gizmos.DrawLine(transform.position, connections[i].transform.position);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        string collName = other.name.ToLower();

        if (collName == "wall_ghost")
        {
            foreach (var node in NodeParent.nodes)
            {
                if (node.connections.Contains(this))
                    node.connections.Remove(this);
            }
            Destroy(gameObject);
        }
    }
}
