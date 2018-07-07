    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {

	public Transform StartPosition;
    public LayerMask WallMask; // this is the mask that the program will look for when trying to find obstruction e.g. walls and stuff
    public Vector2 GridSize;
    public float NodeRadius; // this stores how big each square on the graph will be
    public float DistanceBetweenNodes; //the distance that the squares will spawn from each other

    Node[,] NodeArray;// the array of nodes that the A* algorithm uses 
    public List<Node> FinalPath; //Completed path

    private float nodeDiameter;
    private int gridSizeX, gridSizeY; //size of the grid in array units

    private void Start()
    {
        nodeDiameter = NodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(GridSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(GridSize.y / nodeDiameter);
        createGrid();
    }


    private void createGrid()
    {
        NodeArray = new Node[gridSizeX, gridSizeY];
        Vector3 bottomLeft = transform.position - Vector3.right * gridSizeX/2 - Vector3.forward * gridSizeY/2;

        for(int x = 0; x < gridSizeX; x++)
        {
            for(int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = bottomLeft + Vector3.right * (x * nodeDiameter + NodeRadius) + Vector3.forward * (y * nodeDiameter + NodeRadius);

                bool isWallFlag = true;

                // if the node is not being obstructed
                //Quick collision check against the current node and anything in the world at its position. if it is colliding with
                // an object with a wallmask the statement will return false      
                if(Physics.CheckSphere(worldPoint, NodeRadius, WallMask))
                {
                    isWallFlag = false;
                }

                NodeArray[x, y] = new Node(isWallFlag, worldPoint, x, y);
            }
        }
    }

    // function that gets the neighboring nodes of the given node
    public List<Node> GetNeighboringNodes(Node node)
    {
        List<Node> neightborList = new List<Node>();

        int checkX, checkY;

        checkX = node.GridX + 1;
        checkY = node.GridY;

        if(checkX >= 0 && checkX < gridSizeX) // check if xpos is in range
        {
            if(checkY >= 0 && checkY < gridSizeY)
            {
                neightborList.Add(NodeArray[checkX, checkY]); // add the grid to the available neighbors list
            }
        }

        // check the bottom side of the current node

        checkX = node.GridX;
        checkY = node.GridY - 1;

        if(checkX >= 0 && checkX < gridSizeX)
        {
            if(checkY >=0 && checkY < gridSizeY)
            {
                neightborList.Add(NodeArray[checkX, checkY]);
            }
        }

        checkX = node.GridX - 1;
        checkY = node.GridY;

        if (checkX >= 0 && checkX < gridSizeX)
            if(checkY >= 0 && checkY < gridSizeY)
                neightborList.Add(NodeArray[checkX, checkY]);

        checkX = node.GridX;
        checkY = node.GridY + 1;

        if (checkX >= 0 && checkX < gridSizeX)
            if (checkY >= 0 && checkY < gridSizeY)
                neightborList.Add(NodeArray[checkX, checkY]);


        return neightborList;
    }

    //gets the closest node to the give world position

    public Node NodeFromWorldPoint(Vector3 WorldPoint)
    {
        float xPos = ((WorldPoint.x + gridSizeX / 2) / gridSizeX);
        float yPos = ((WorldPoint.z + gridSizeY / 2) / gridSizeY);

        xPos = Mathf.Clamp01(xPos);
        yPos = Mathf.Clamp01(yPos);

        int x = Mathf.RoundToInt((gridSizeX - 1) * xPos);
        int y = Mathf.RoundToInt((gridSizeY - 1) * yPos);

        return NodeArray[x, y];
    }

    // draw the wireFrame

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridSizeX, 1, gridSizeY));

        if(NodeArray != null)
        {
            foreach(Node n in NodeArray)
            {
                if (n.IsWall)
                {
                    Gizmos.color = Color.white;
                }
                else
                    Gizmos.color = Color.yellow;

                if(FinalPath != null)
                {
                    if(FinalPath.Contains(n))
                    {
                        Gizmos.color = Color.red;
                    }
                }

                Gizmos.DrawCube(n.Position, Vector3.one * (nodeDiameter - DistanceBetweenNodes));
            }
        }
    }
}

