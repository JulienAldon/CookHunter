using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

namespace Toolbox
{

    public interface IGraph
    {
        IEnumerable<Vector3Int> Neighbors(Vector3Int v);
        float Cost(Vector3Int a, Vector3Int b);
    }

 
    public class FourDirectionGraph : IGraph
    {
        Tilemap map;

        public FourDirectionGraph(Tilemap map)
        {
            this.map = map;
        }

        public IEnumerable<Vector3Int> Neighbors(Vector3Int v)
        {
            foreach (Vector3Int dir in Utils.FourDirections)
            {
                Vector3Int next = v + dir;

                if (map.IsCellEmpty(next))
                {
                    yield return next;
                }
            }
        }

        public float Cost(Vector3Int a, Vector3Int b)
        {
            return Vector3Int.Distance(a, b);
        }
    }
    
    public class MoveGraph : IGraph
    {
        Tilemap map;

        public MoveGraph(Tilemap map)
        {
            this.map = map;
        }

        public IEnumerable<Vector3Int> Neighbors(Vector3Int v)
        {
            foreach (Vector3Int dir in Utils.FourDirections)
            {
                Vector3Int next = v + dir;

                if (map.IsCellEmpty(next))
                {
                    yield return next;
                }
            }

            foreach (Vector3Int dir in Utils.DiagonalDirections)
            {
                Vector3Int next = v + dir;

                if (map.IsCellEmpty(next))
                {
                    Vector3Int adjacent1 = v + new Vector3Int(dir.x, 0, 0);
                    Vector3Int adjacent2 = v + new Vector3Int(0, dir.y, 0);

                    if (map.IsCellEmpty(adjacent1) && map.IsCellEmpty(adjacent2))
                    {
                        yield return next;
                    }
                }
            }
        }

        public float Cost(Vector3Int a, Vector3Int b)
        {
            return Vector3Int.Distance(a, b);
        }
    }
}
