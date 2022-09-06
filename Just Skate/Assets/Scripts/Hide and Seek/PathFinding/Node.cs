using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public class Node
{
    public int MOVE_DIAGONAL_COST = 14;
    public int MOVE_STRAIGHT_COST = 10;
    public Node[,] Grid;
    public int Xcoord;
    public int Ycoord;
    public bool isWall = false;
    public float G { get; private set; }
    public float H { get; private set; }
    public float F { get; private set; }
    public Node Connection { get; private set; }
    public void SetConnection(Node node) => Connection = node;

    public void SetG(float g) => G = g;

    public void SetH(float h) => H = h;

    public void SetF(float f) => F = f;

    public Node(int a_Xcoord, int a_Ycoord, Node[,] a_Grid)
    {
        Xcoord = a_Xcoord;
        Ycoord = a_Ycoord;
        Grid = a_Grid;
    }

    public List<Node> GetNeighbors()
    {
        List<Node> neighbors = new List<Node>();
        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                if (!(i == 0 && j == 0) && Xcoord < 84 && Xcoord > 0 && Ycoord < 84 && Ycoord > 0)
                {
                    neighbors.Add(Grid[Xcoord + i, Ycoord + j]);
                }
            }
        }
        return neighbors;
    }

    public float GetDistance(Node node)
    {
        int xDistance = Math.Abs(this.Xcoord - node.Xcoord);
        int yDistance = Math.Abs(this.Ycoord - node.Ycoord);
        int remaining = Math.Abs(xDistance - yDistance);
        return MOVE_DIAGONAL_COST * Math.Min(xDistance, yDistance) + MOVE_STRAIGHT_COST * remaining;
    }
}
