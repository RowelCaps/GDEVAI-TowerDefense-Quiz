using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour {

    [SerializeField]private Grid gridReference;

    public Transform StartPosition;
    public Transform TargetPosition;

	// Use this for initialization
	void Start () {
        gridReference = GetComponent<Grid>();
	}
	
	// Update is called once per frame
	void Update () {
        pathFinding(StartPosition.position, TargetPosition.position);
	}

    void pathFinding(Vector3 startPosition, Vector3 targetPosition)
    {
        Node start = gridReference.NodeFromWorldPoint(startPosition);
        Node target = gridReference.NodeFromWorldPoint(targetPosition);

        List<Node> openList = new List<Node>();
        HashSet<Node> closedList = new HashSet<Node>();

        openList.Add(start);

        while(openList.Count > 0)
        {
            Node currentNode = openList[0];

            for(int i = 0; i < openList.Count; i++)
            {
                if (openList[i].F_Cost < currentNode.F_Cost || openList[i].F_Cost == currentNode.F_Cost && openList[i].H_Cost < currentNode.H_Cost)
                    currentNode = openList[i];
            }

            if (currentNode == target)
                getFinalPath(start, target);

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            foreach (var neighborNode in gridReference.GetNeighboringNodes(currentNode))
            {
                if (!neighborNode.IsWall || closedList.Contains(neighborNode))
                    continue;

                int moveCost = currentNode.G_Cost + GetManhattanDistance(currentNode, neighborNode);

                if (moveCost < neighborNode.G_Cost || !openList.Contains(neighborNode))
                {
                    neighborNode.G_Cost = moveCost;
                    neighborNode.H_Cost = GetManhattanDistance(neighborNode, target);
                    neighborNode.ParentNode = currentNode;

                    if (!openList.Contains(neighborNode))
                        openList.Add(neighborNode);
                }
            }
        }
    }

    void getFinalPath(Node startNode, Node endNode)
    {
        List<Node> finalPath = new List<Node>();
        Node currentNode = endNode; 

        while(currentNode != startNode)
        {
            finalPath.Add(currentNode);
            currentNode = currentNode.ParentNode;
        }

        finalPath.Reverse();
        gridReference.FinalPath = finalPath;
    }

    int GetManhattanDistance(Node a, Node b)
    {
        int x = Mathf.Abs(a.GridX - b.GridX);
        int y = Mathf.Abs(a.GridY - b.GridY);

        return x + y;
    }
}
