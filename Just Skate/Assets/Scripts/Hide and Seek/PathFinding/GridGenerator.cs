// 85:85 units map
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator
{
    public int deltaX = -171;
    public int deltaY = 57;
    public int sizeX = 85;
    public int sizeY = 85;
    public Node[,] Grid = new Node[85, 85];
    public GridGenerator(List<Collider2D> colliders)
    {
        for (int i = 0; i < sizeX; i++)
        {
            for (int j = 0; j < sizeY; j++)
            {
                Grid[i, j] = new Node(i, j, Grid);
                if (OnCollider(colliders, Grid[i, j]))
                {
                    Grid[i, j].isWall = true;
                }
                else
                {
                    Grid[i, j].isWall = false;
                }
            }
        }
    }

    bool OnCollider(List<Collider2D> colliders, Node node)
    {
        Vector2 point = new Vector2(node.Xcoord + deltaX, node.Ycoord + deltaY);
        for (int i = 0; i < colliders.Count; i++)
        {

            Collider2D collider = colliders[i];
            if (collider.OverlapPoint(point))
            {
                return true;
            }
        }
        return false;
    }

    public void SetValues(Node startNode, Node targetNode)
    {
        for (int i = 0; i < sizeX; i++)
        {
            for (int j = 0; j < sizeY; j++)
            {
                Grid[i, j].SetG(FindG(startNode, Grid[i, j]));
                Grid[i, j].SetH(FindH(Grid[i, j], targetNode));
                Grid[i, j].SetF(FindF(Grid[i, j]));
            }
        }
    }

    public float FindG(Node startNode, Node node)
    {
        float length = node.GetDistance(startNode);
        return length;
    }

    public float FindH(Node node, Node targetNode)
    {

        float length = node.GetDistance(targetNode);
        return length;
    }
    public float FindF(Node node)
    {
        return node.G + node.H;
    }
}

