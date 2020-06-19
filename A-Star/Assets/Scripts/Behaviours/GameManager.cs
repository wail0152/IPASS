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
    public bool SetupNodeOnBake;

    [Header("Node Settings")]
    public Transform start;
    public Transform end;
    public bool drawPath;
    public bool drawConnections;

    private Node startNode;
    private Node endNode;
    private List<Node> nodes = new List<Node>();

    public enum PathOption
    {
        AStar,
        Dijksta
    }

    private IDictionary<PathOption, IPathfindingStrategy> lookupStrategy = new Dictionary<PathOption, IPathfindingStrategy>()
    {
        { PathOption.AStar, new Astar() },
        { PathOption.Dijksta, new Dijkstra() }
    };

    private void Start()
    {
        SetupNodes();

        if (bakePathOnStart)
            SetupAlgorithm();
    }

    public void SetupAlgorithm()
    {
        startNode = FindClosestNode(start);
        endNode = FindClosestNode(end);
        UseStrategy();
    }

    public void SetupNodes()
    {
        nodes.Clear();

        foreach (Node node in FindObjectsOfType<Node>())
        {
            nodes.Add(node);
        }

        nodes?.ForEach(node => node.AddNeigbours(this, nodes, neighbourDistance));
    }

    private void UseStrategy()
    {
        Context context = new Context();
        context.SetStrategy(lookupStrategy[pathOption]);
        context.FindPath(nodes, startNode, endNode);
    }

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

    private void OnDrawGizmos()
    {
        if (drawConnections)
        {
            Gizmos.color = Color.green;
            nodes?.ForEach(node =>
            {
                node?.neighbours?.ForEach(neighbour =>
                {
                    Gizmos.DrawLine(node.transform.position, neighbour.transform.position);
                });
            });
        }
        
        if (drawPath && nodes != null && startNode && endNode)
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
    
    private List<Node> GetPathToEnd()
    {
        List<Node> pathToEnd = new List<Node>();

        Node currentNode = endNode;
        pathToEnd.Add(currentNode);
        while (currentNode != startNode)
        {
            currentNode = currentNode.parentNode;
            pathToEnd.Add(currentNode);
        }

        return pathToEnd;
    }

}
