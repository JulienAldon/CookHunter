﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Toolbox
{
    public static class TilemapBreadthFirst
    {
        public static Vector3Int? BreadthFirstSearch(Vector3Int position, Vector3Int[] directions, Func<Vector3Int, bool> isGoal, Func<Vector3Int, Vector3Int, bool> isConnected)
        {
            Queue<Vector3Int> queue = new Queue<Vector3Int>();
            queue.Enqueue(position);

            HashSet<Vector3Int> visited = new HashSet<Vector3Int>();

            while (queue.Count > 0)
            {
                Vector3Int node = queue.Dequeue();

                if (isGoal(node))
                {
                    return node;
                }

                foreach (Vector3Int dir in directions)
                {
                    Vector3Int next = node + dir;

                    if (isConnected(node, next) && !visited.Contains(next))
                    {
                        queue.Enqueue(next);
                    }
                }

                visited.Add(node);
            }

            return null;
        }

        public static Vector3Int? BreadthFirstSearch(this Tilemap tilemap, Vector3Int position, Vector3Int[] directions, Func<Vector3Int, bool> isGoal, Func<Vector3Int, Vector3Int, bool> isConnected)
        {
            return BreadthFirstSearch(position, directions, isGoal, isConnected);
        }

        public static Vector3? BreadthFirstSearch(this Tilemap tilemap, Vector3 position, Vector3Int[] directions, Func<Vector3Int, bool> isGoal, Func<Vector3Int, Vector3Int, bool> isConnected)
        {
            Vector3Int start = tilemap.WorldToCell(position);

            Vector3Int? resultInt = BreadthFirstSearch(start, directions, isGoal, isConnected);

            Vector3? result = null;

            if (resultInt.HasValue)
            {
                result = tilemap.GetCellCenterWorld(resultInt.Value);
            }

            return result;
        }

        public static Vector3Int? BreadthFirstSearch(this Tilemap tilemap, Vector3Int position, Func<Vector3Int, bool> isGoal, Func<Vector3Int, Vector3Int, bool> isConnected)
        {
            return BreadthFirstSearch(position, Utils.FourDirections, isGoal, isConnected);
        }

        public static Vector3? BreadthFirstSearch(this Tilemap tilemap, Vector3 position, Func<Vector3Int, bool> isGoal, Func<Vector3Int, Vector3Int, bool> isConnected)
        {
            Vector3Int start = tilemap.WorldToCell(position);

            Vector3Int? resultInt = BreadthFirstSearch(start, Utils.FourDirections, isGoal, isConnected);

            Vector3? result = null;

            if (resultInt.HasValue)
            {
                result = tilemap.GetCellCenterWorld(resultInt.Value);
            }

            return result;
        }

        public static Vector3? ClosestEmptyCell(this Tilemap tilemap, Vector3 position, Vector3Int[] directions)
        {
            return tilemap.BreadthFirstSearch(
                position,
                directions,
                tilemap.IsCellEmpty,
                (current, next) => true
            );
        }

        public static Vector3? ClosestEmptyCell(this Tilemap tilemap, Vector3 position)
        {
            return tilemap.ClosestEmptyCell(position, Utils.FourDirections);
        }

        public static List<Vector3Int> BreadthFirstTraversal(Vector3Int position, Vector3Int[] directions, Func<Vector3Int, Vector3Int, bool> isConnected)
        {
            Queue<Vector3Int> queue = new Queue<Vector3Int>();
            queue.Enqueue(position);

            HashSet<Vector3Int> visited = new HashSet<Vector3Int>();

            while (queue.Count > 0)
            {
                Vector3Int node = queue.Dequeue();

                foreach (Vector3Int dir in directions)
                {
                    Vector3Int next = node + dir;

                    if (isConnected(node, next) && !visited.Contains(next))
                    {
                        queue.Enqueue(next);
                    }
                }

                visited.Add(node);
            }

            return visited.ToList();
        }

        public static List<Vector3Int> BreadthFirstTraversal(this Tilemap tilemap, Vector3Int position, Vector3Int[] directions, Func<Vector3Int, Vector3Int, bool> isConnected)
        {
            return BreadthFirstTraversal(position, directions, isConnected);
        }


        public static List<Vector3> BreadthFirstTraversal(this Tilemap tilemap, Vector3 position, Vector3Int[] directions, Func<Vector3Int, Vector3Int, bool> isConnected)
        {
            Vector3Int start = tilemap.WorldToCell(position);

            List<Vector3Int> positions = BreadthFirstTraversal(start, directions, isConnected);

            return positions.Select(p => tilemap.GetCellCenterWorld(p)).ToList();
        }

        public static List<Vector3Int> BreadthFirstTraversal(this Tilemap tilemap, Vector3Int position, Func<Vector3Int, Vector3Int, bool> isConnected)
        {
            return BreadthFirstTraversal(position, Utils.FourDirections, isConnected);
        }

        public static List<Vector3> BreadthFirstTraversal(this Tilemap tilemap, Vector3 position, Func<Vector3Int, Vector3Int, bool> isConnected)
        {
            return tilemap.BreadthFirstTraversal(position, Utils.FourDirections, isConnected);
        }
    }
}