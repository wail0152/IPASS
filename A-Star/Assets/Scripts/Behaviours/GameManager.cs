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

    IDictionary<PathOption, IPathfindingStrategy> lookupStrategy = new Dictionary<PathOption, IPathfindingStrategy>()
    {
        { PathOption.AStar, new Astar() },
        { PathOption.Dijksta, new Dijkstra() }
    };

    private void Start()
    {
        CreateStrategy();
    }

    private void CreateStrategy()
    {
        Context context = new Context();
        context.SetStrategy(lookupStrategy[pathOption]);
        context.FindPath();
    }

    public void GeneratePath()
    {

    }
}
