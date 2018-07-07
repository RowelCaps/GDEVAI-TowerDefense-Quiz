using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node{

    public int GridX; //x position in the node array
    public int GridY; //you know already same as the above but Y

    public bool IsWall;
    public Vector3 Position;

    public Node ParentNode; //for A* algo, this will store what node it previously came fromso it can trace the shortest path

    public int G_Cost; // the cost of moving to the next square
    public int H_Cost; // this  distance to the goal from this node

    public int F_Cost
    {
        get
        {
            return G_Cost + H_Cost;
        }
    }

    public Node(bool isWall, Vector3 pos, int gridX, int gridY)
    {
        this.IsWall = isWall;
        this.Position = pos;
        this.GridX = gridX;
        this.GridY = gridY;
    }
}
