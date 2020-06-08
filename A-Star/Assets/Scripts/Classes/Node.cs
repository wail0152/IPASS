using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{

    public List<Node> neighbours = new List<Node>();
    public Node parent;
    public float gCost;
    public float hCost;
    public float fCost
    {
        get { return gCost + hCost;  }
    }

    public void AddNeigbours(List<Node> nodes, float neighbourDistance)
    {
        foreach (Node node in nodes)
        {
            if (Vector3.Distance(transform.position, node.transform.position) <= neighbourDistance && this != node)
                neighbours.Add(node);
        }
    }

}
