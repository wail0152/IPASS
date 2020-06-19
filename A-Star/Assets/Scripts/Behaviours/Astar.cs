using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astar : IPathfindingStrategy
{

    private List<Node> openNodes;
    private List<Node> closedNodes;
    private Node startNode;
    private Node endNode;

    public void Excute(List<Node> nodes, Node startNode, Node endNode)
    {
        Setup(nodes, startNode, endNode);
        Algorithm();
    }

    private void Setup(List<Node> nodes, Node startNode, Node endNode)
    {
        openNodes = new List<Node>();
        closedNodes = new List<Node>();
        this.startNode = startNode;
        this.endNode = endNode;
        openNodes.Add(this.startNode);
    }

    private void Algorithm()
    {
        while (true)
        {
            Node currentNode = LowestCostNode();
            openNodes.Remove(currentNode);
            closedNodes.Add(currentNode);

            if (currentNode == endNode || currentNode == null)
                break;

            foreach (Node neighbour in currentNode.neighbours)
            {
                if (closedNodes.Contains(neighbour))
                    continue;

                if (!openNodes.Contains(neighbour))
                {
                    neighbour.gCost = Vector3.Distance(neighbour.transform.position, startNode.transform.position);
                    neighbour.hCost = Vector3.Distance(neighbour.transform.position, endNode.transform.position);
                    neighbour.parentNode = currentNode;
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
