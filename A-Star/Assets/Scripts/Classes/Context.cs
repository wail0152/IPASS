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

    public void FindPath()
    {
        Debug.Log("Context: doing something with the strategy (not sure how it'll do it)");
        strategy.Excute();
    }
}
