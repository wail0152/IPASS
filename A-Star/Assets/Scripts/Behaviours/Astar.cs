using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astar : IPathfindingStrategy
{

    private List<Node> openNodes;
    private List<Node> closedNodes;
    private Node startNode;
    private Node endNode;

    public void Excute(List<Node> nodes)
    {
        Setup(nodes);
        Algorithm();
    }

    private void Setup(List<Node> nodes)
    {
        openNodes = new List<Node>();
        closedNodes = new List<Node>();
        startNode = nodes[0];
        endNode = nodes[nodes.Count - 1];
        openNodes.Add(startNode);
    }

    private void Algorithm()
    {
        while (true)
        {
            Node currentNode = LowestCostNode();
            openNodes.Remove(currentNode);
            closedNodes.Add(currentNode);

            if (currentNode == endNode)
                break;

            foreach (Node neighbour in currentNode.neighbours)
            {
                if (closedNodes.Contains(neighbour))
                    continue;

                if (!openNodes.Contains(neighbour))
                {
                    neighbour.gCost = Vector3.Distance(neighbour.transform.position, startNode.transform.position);
                    neighbour.hCost = Vector3.Distance(neighbour.transform.position, endNode.transform.position);
                    neighbour.parent = currentNode;
                    openNodes.Add(neighbour);
                }
            }
        }
    }

    private Node LowestCostNode()
    {
        Node currentNode = null;
        float lowestCost = Mathf.Infinity;
        foreach (Node openNode in openNodes)
        {
            if (openNode.fCost <= lowestCost)
            {
                lowestCost = openNode.fCost;
                currentNode = openNode;
            }
        }
        return currentNode;
    }

}
