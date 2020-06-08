using System;
using System.Collections.Generic;
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

    private IDictionary<PathOption, IPathfindingStrategy> lookupStrategy = new Dictionary<PathOption, IPathfindingStrategy>()
    {
        { PathOption.AStar, new Astar() },
        { PathOption.Dijksta, new Dijkstra() }
    };

    private List<Node> nodes;

    private void Start()
    {
        SetupTotal();
    }

    public void SetupTotal()
    {
        SetupNodes();
        CreateStrategy();
    }

    private void SetupNodes()
    {
        nodes = new List<Node>();

        foreach (Transform child in pointsParent)
        {
            child.gameObject.AddComponent<Node>();
            nodes.Add(child.GetComponent<Node>());
        }

        foreach (Node node in nodes)
        {
            node.AddNeigbours(nodes, neighbourDistance);
        }
    }

    private void CreateStrategy()
    {
        Context context = new Context();
        context.SetStrategy(lookupStrategy[pathOption]);
        context.FindPath(nodes);
    }

}
