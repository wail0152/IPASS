﻿using System.Collections;
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

    //Setting the gameManager refrence and getting all the surrounding neighbours
    public void AddNeigbours(GameManager gameManager, List<Node> nodes, float neighbourDistance)
    {
        this.gameManager = gameManager;

        neighbours = new List<Node>();
        foreach (Node node in nodes)
        {
            if (Vector3.Distance(node.transform.position, transform.position) < neighbourDistance)
                neighbours.Add(node);
        }
    }

    //Updating the connections when a node is moved
    private void Update()
    {
        if (transform.hasChanged && gameManager != null)
        {
            gameManager.UpdateNode(this);
            transform.hasChanged = false;
        }
    }

}
