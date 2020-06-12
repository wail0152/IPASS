using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum PathOption
    {
        AStar,
        Dijksta
    }

    public PathOption pathOption;
    public Transform pointsParent;
    public float neighbourDistance;
    public bool bakePathOnStart;

    private IDictionary<PathOption, IPathfindingStrategy> lookupStrategy = new Dictionary<PathOption, IPathfindingStrategy>()
    {
        { PathOption.AStar, new Astar() },
        { PathOption.Dijksta, new Dijkstra() }
    };

    private List<Node> nodes;

    private void Start()
    {
        if (bakePathOnStart)
            SetupTotal();
    }

    public void SetupTotal()
    {
        SetupNodes();
        CreateStrategy();
        SceneView.RepaintAll();
    }

    public void SetupNodes(bool forceReload = false)
    {
        if (nodes == null)
            nodes = new List<Node>();

        if (nodes.Count != pointsParent.childCount || forceReload)
        {
            nodes.Clear();

            foreach (Transform child in pointsParent)
            {
                nodes.Add(child.GetComponent<Node>());
            }

            foreach (Node node in nodes)
            {
                node.AddNeigbours(this, nodes, neighbourDistance);
            }
        }
    }

    private void CreateStrategy()
    {
        Context context = new Context();
        context.SetStrategy(lookupStrategy[pathOption]);
        context.FindPath(nodes);
    }

    private void OnDrawGizmos()
    {
        SetupNodes();

        Gizmos.color = Color.green;
        foreach (Node node in nodes)
        {
            foreach (Node neigbour in node.neighbours)
            {
                Gizmos.DrawLine(node.transform.position, neigbour.transform.position);
            }
        }

        Gizmos.color = Color.red;
        Node currentNode = nodes[nodes.Count - 1];
        while (currentNode.parentNode != null)
        {
            Gizmos.DrawLine(currentNode.transform.position, currentNode.parentNode.transform.position);
            currentNode = currentNode.parentNode;
        }
    }
    
    private List<Node> GetPathToEnd()
    {
        List<Node> pathToEnd = new List<Node>();

        Node currentNode = nodes[nodes.Count - 1];
        pathToEnd.Add(currentNode);
        while (currentNode.parentNode != null)
        {
            currentNode = currentNode.parentNode;
            pathToEnd.Add(currentNode);
        }

        return pathToEnd;
    }

}
