using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : IHeapItem<Node>
{
    private int x;
    public int X { get; set; }

    private int y;
    public int Y { get; set; }

    private int g;
    public int G { get; set; }

    private int h;
    public int H { get; set; }

    public int F { get { return G + H; } }

    private bool walkable;
    public bool Walkable { get; set; }

    private Node parent;

    private int heapIndex;

    public Node Parent { get; set; }
    public Node (int x, int y, bool walkable)
    {
        this.X = x;
        this.Y = y;
        this.Walkable = walkable;
    }

    public int HeapIndex
    {
        get { return heapIndex; }
        set { heapIndex = value; }
    }

    public int CompareTo(Node nodeToCompare)
    {
        int compare = F.CompareTo(nodeToCompare.F);
        if (compare == 0)
        {
            compare = H.CompareTo(nodeToCompare.H);
        }
        return -compare;
    }
}
