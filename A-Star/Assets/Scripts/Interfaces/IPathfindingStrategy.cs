using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPathfindingStrategy
{
    //Standard methode for a pathfinding strategy in which you need nodes a start node and an end node
    void Excute(List<Node> nodes, Node startNode, Node endNode);
}
