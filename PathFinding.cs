using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class PathFinding : MonoBehaviour
{
    NodeGrid nodeGrid;

    public Transform start, end;

    void Awake()
    {
        nodeGrid = GetComponent<NodeGrid>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            FindPath(start.position, end.position);
        }
    }
    public void FindPath (Vector3 startPos, Vector3 targetPos)
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();
        Node startNode = nodeGrid.NodeByCoordinates(startPos);
        Node targetNode = nodeGrid.NodeByCoordinates(targetPos);
        

        Heap<Node> openSet = new Heap<Node>(nodeGrid.MaxSize);
        List<Node> closedSet = new List<Node>();

        openSet.Add(startNode);
        while (openSet.Count > 0)
        {
            Node currentNode = openSet.RemoveFirst();
            /*for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].F < currentNode.F || openSet[i].F == currentNode.F && openSet[i].H < currentNode.H)
                {
                    currentNode = openSet[i];
                }
            }*/           

            //openSet.Remove(currentNode);
            closedSet.Add(currentNode);
            

            if (currentNode == targetNode)
            {
                sw.Stop();
                print($"Path found: {sw.ElapsedMilliseconds} ms");
                RetracePath(startNode, targetNode);
                return;
            }

            foreach (Node neighbour in nodeGrid.GetAdjacentNodes(currentNode))
            {

                if (!neighbour.Walkable || closedSet.Contains(neighbour))
                {                   
                    continue;
                }               

                int newMovementCostToNeighbour = currentNode.G + GetDistance(currentNode, neighbour);
                if (newMovementCostToNeighbour < neighbour.G || !openSet.Contains(neighbour))
                {
                    neighbour.G = newMovementCostToNeighbour;
                    neighbour.H = GetDistance(neighbour, targetNode);
                    neighbour.Parent = currentNode;

                    if (!openSet.Contains(neighbour))
                    {
                        openSet.Add(neighbour);
                    }
                }
            }
        }
    }

    private void RetracePath (Node startNode, Node targetNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = targetNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.Parent;
        }

        path.Reverse();       
        nodeGrid.path = path;
    }

    private int GetDistance(Node nodeA, Node nodeB)
    {
        int distanceX = Mathf.Abs(nodeA.X - nodeB.X);
        int distanceY = Mathf.Abs(nodeA.Y - nodeB.Y);

        if (distanceX > distanceY)
        {
            return 14 * distanceY + 10 * (distanceX - distanceY);
        }
        return 14 * distanceX + 10 * (distanceY - distanceX);
    }
}
