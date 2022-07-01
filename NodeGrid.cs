using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeGrid : MonoBehaviour
{
    private Node[,] grid;
    private int gridSizeX = 50;
    private int gridSizeY = 20;

    private List<string> walls = new List<string>();

    public List<Node> path;

    void Start()
    {
        grid = new Node[gridSizeX, gridSizeY];
        
        for (int i = 0; i < 300; i++)
        {           
            walls.Add($"{Random.Range(0, gridSizeX)}-{Random.Range(0, gridSizeY)}");           
        }
        GenerateNodes();
    }

    private void GenerateNodes ()
    {
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                if (walls.Contains($"{x}-{y}"))
                {
                    grid[x, y] = new Node(x, y, false);
                } else
                {
                    grid[x, y] = new Node(x, y, true);
                }
            }
        }
    }

    public int MaxSize
    {
        get { return gridSizeX * gridSizeY; }
    }

    public Node NodeByCoordinates (Vector3 coords)
    {
        return grid[(int)coords.x, (int)coords.y];
    }

    public List<Node> GetAdjacentNodes (Node node)
    {
        List<Node> list = new List<Node>();
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                {
                    continue;
                }

                int checkX = node.X + x;
                int checkY = node.Y + y;

                if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                {
                    list.Add(grid[checkX, checkY]);
                }
            }
        }
        return list;
    }

    void OnDrawGizmos()
    {
        if (grid != null)
        {
            foreach (Node n in grid)
            {
                Gizmos.color = Color.white;
                if (path != null)
                {                   
                    if (path.Contains(n))
                    {
                        Gizmos.color = Color.black;
                    }
                }
                if (!n.Walkable)
                {
                    Gizmos.color = Color.red;
                }
                Gizmos.DrawCube(new Vector3(n.X, n.Y, 0), new Vector3(0.95f, 0.95f, 0.1f));
            }
        }
    }
}
