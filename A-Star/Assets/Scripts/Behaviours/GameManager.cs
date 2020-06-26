using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [Header("Algorithm Settings")]
    public PathOption pathOption;
    public float neighbourDistance;
    public bool bakePathOnStart;

    [Header("Node Settings")]
    public Transform start;
    public Transform end;
    public bool drawPath;
    public bool drawConnections;

    public delegate void BakePath();
    public BakePath OnBakePath;
    private Node startNode;
    private Node endNode;
    private List<Node> nodes = new List<Node>();

    public enum PathOption
    {
        AStar,
        Dijksta
    }

    //Mapping enums to IPathfindingStrategy to be able to cleanly add new strategys
    private IDictionary<PathOption, IPathfindingStrategy> lookupStrategy = new Dictionary<PathOption, IPathfindingStrategy>()
    {
        { PathOption.AStar, new Astar() },
        { PathOption.Dijksta, new Dijkstra() }
    };

    //Setting up the nodes by finding them all and getting the connections and excuting the chosen algorithm if bakePathOnStart
    private void Start()
    {
        //SetupNodes();

        if (bakePathOnStart)
            SetupAlgorithm();
    }

    //Finding the closest nodes to the start and end markers, using the strategy and invoking the OnBakePath event to let subsribers know that the path has been changed
    public void SetupAlgorithm()
    {
        startNode = FindClosestNode(start);
        endNode = FindClosestNode(end);
        UseStrategy();
        OnBakePath?.Invoke();
    }

    //Finding all the nodes in the scene and setting their neigbours
    public void SetupNodes()
    {
        nodes.Clear();
        nodes.AddRange(FindObjectsOfType<Node>());
        foreach (Node node in nodes)
        {
            node.AddNeigbours(this, nodes, neighbourDistance);
        }
    }

    //Making a new strategy and setting the correct pathfinding strategy for the context after that FindPath
    private void UseStrategy()
    {
        Context context = new Context();
        context.SetStrategy(lookupStrategy[pathOption]);
        context.FindPath(nodes, startNode, endNode);
    }

    //Finding the closest node to a transform and returning it
    private Node FindClosestNode(Transform trans)
    {
        Node closestNode = null;
        float closestDistance = Mathf.Infinity;
        foreach (Node node in nodes)
        {
            if (Vector3.Distance(trans.position, node.transform.position) < closestDistance)
            {
                closestDistance = Vector3.Distance(trans.position, node.transform.position);
                closestNode = node;
            }
        }
        return closestNode;
    }

    //Drawing the connections to each neighbour and drawing the calculated path
    private void OnDrawGizmos()
    {
        if (drawConnections)
        {
            Gizmos.color = Color.green;
            nodes?.ForEach(node => {
                node?.neighbours?.ForEach(neighbour => Gizmos.DrawLine(node.transform.position, neighbour.transform.position));
            });
        }
        
        if (drawPath && startNode && endNode)
        {
            Gizmos.color = Color.red;
            Node currentNode = endNode;
            while (currentNode != startNode)
            {
                Gizmos.DrawLine(currentNode.transform.position, currentNode.parentNode.transform.position);
                currentNode = currentNode.parentNode;
            }
        }
    }
    
    //Getting the path from the start node to the end node
    public List<Node> GetPathToEnd()
    {
        List<Node> pathToEnd = new List<Node>();

        Node currentNode = endNode;
        pathToEnd.Add(currentNode);
        while (currentNode != startNode)
        {
            currentNode = currentNode.parentNode;
            pathToEnd.Add(currentNode);
        }
        pathToEnd.Reverse();

        return pathToEnd;
    }

}
