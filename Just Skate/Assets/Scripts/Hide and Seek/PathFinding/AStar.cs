using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
public class AStar
{
    //public GridGenerator Grid = new GridGenerator(EnemyMovement.colliders);

    public List<Node> FindPath(Node startNode, Node targetNode, GridGenerator Grid )
    {
        Grid.SetValues(startNode, targetNode);
        var toSearch = new List<Node> { startNode };
        var processed = new List<Node>();

        while (toSearch.Any())
        {
            var current = toSearch[0];
            foreach (var t in toSearch)
            {
                if (t.F < current.F || t.F == current.F && t.H < current.H)
                {
                    current = t;
                }
            }

            processed.Add(current);
            toSearch.Remove(current);

            if (current == targetNode)
            {
                var currentPathTile = targetNode;
                var path = new List<Node>();
                while (currentPathTile != startNode)
                {
                    path.Add(currentPathTile);
                    currentPathTile = currentPathTile.Connection;
                }
                path.Reverse();
                return path;
            }

            foreach (var neighbor in current.GetNeighbors().Where(t => !t.isWall && !processed.Contains(t)))
            {
                var inSearch = toSearch.Contains(neighbor);

                var costToNeighbor = current.G + current.GetDistance(neighbor);

                if (!inSearch || costToNeighbor < neighbor.G)
                {
                    neighbor.SetG(costToNeighbor);
                    neighbor.SetConnection(current);

                    if (!inSearch)
                    {
                        //neighbor.SetH(neighbor.GetDistance(targetNode));
                        toSearch.Add(neighbor);
                    }
                }
            }


        }
        return null;
    }

}
