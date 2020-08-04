using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[ExecuteInEditMode]
public class Node : MonoBehaviour
{

    public List<Node> neighbours;
    public Node parentNode;
    public float gCost;
    public float hCost;
    public float fCost => gCost + hCost;

    private GameManager gameManager;

    private void Awake() => transform.hasChanged = false;

    //Setting the gameManager refrence and getting all the surrounding neighbours
    public void AddNeigbours(GameManager gameManager, List<Node> nodes, float neighbourDistance)
    {
        this.gameManager = gameManager;
        neighbours = nodes.Where(node => Vector3.Distance(node.transform.position, transform.position) < neighbourDistance).ToList();
    }

    //Updating the connections when a node is moved
    private void Update()
    {
        if (transform.hasChanged && gameManager != null)
        {
            gameManager.UpdateNode(this);
            transform.hasChanged = false;
        }
    }

}
