using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Node : MonoBehaviour
{

    public List<Node> neighbours;
    public Node parentNode;
    public float gCost;
    public float hCost;
    public float fCost
    {
        get { return gCost + hCost;  }
    }

    private GameManager gameManager;

    public void AddNeigbours(GameManager gameManager, List<Node> nodes, float neighbourDistance)
    {
        this.gameManager = gameManager;

        neighbours = new List<Node>();

        foreach (Node node in nodes)
        {
            if (Vector3.Distance(transform.position, node.transform.position) <= neighbourDistance && this != node)
                neighbours.Add(node);
        }
    }

    private void Update()
    {
        if (transform.hasChanged && gameManager != null)
        {
            gameManager.SetupNodes();
            transform.hasChanged = false;
        }
    }

}
