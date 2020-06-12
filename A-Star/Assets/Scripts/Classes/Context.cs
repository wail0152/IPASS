using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Context
{

    private IPathfindingStrategy strategy;

    public Context() {}

    public Context(IPathfindingStrategy strategy)
    {
        SetStrategy(strategy);
    }

    public void SetStrategy(IPathfindingStrategy strategy)
    {
        this.strategy = strategy;
    }

    public void FindPath(List<Node> nodes)
    {
        strategy.Excute(nodes);
    }
}
