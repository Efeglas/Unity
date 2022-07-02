using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fox : MonoBehaviour
{
    public Transform end;   
    private List<Node> path;
    public float speed = 10.0f;
    private Node target;

    private GameObject nodeGridGameObject;
    private PathFinding pathfinding;
    private NodeGrid nodeGrid;

    void Awake()
    {
        nodeGridGameObject = GameObject.Find("NodeGrid");
        pathfinding = nodeGridGameObject.GetComponent<PathFinding>();
        target = new Node((int)transform.position.x, (int)transform.position.y, true);
    }
    void Start()
    {        
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            path = pathfinding.FindPath(transform.position, end.position);           
        }

        if (path != null)
        {           
            float step = speed * Time.deltaTime;           
            if (transform.position.x != target.X || transform.position.y != target.Y)
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(target.X, target.Y), step);
            }
            else
            {
                if (path.Count > 0)
                {                   
                    target = path[0];                   
                    path.RemoveAt(0);
                }

            }
        }
    }
}
