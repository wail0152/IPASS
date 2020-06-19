using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPathfindingStrategy
{
    void Excute(List<Node> nodes, Node startNode, Node endNode);
}
