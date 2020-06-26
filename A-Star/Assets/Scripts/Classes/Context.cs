using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Context
{

    private IPathfindingStrategy strategy;

    public Context() {}

    //Constructor to set the strategy
    public Context(IPathfindingStrategy strategy)
    {
        SetStrategy(strategy);
    }

    //Methode to set the strategy at runtime
    public void SetStrategy(IPathfindingStrategy strategy)
    {
        this.strategy = strategy;
    }

    //Using the strategy excute methode to find a path
    public void FindPath(List<Node> nodes, Node startNode, Node endNode)
    {
        strategy.Excute(nodes, startNode, endNode);
    }
}
