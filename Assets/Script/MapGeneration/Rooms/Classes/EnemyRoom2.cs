using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRoom2 : Room
{
    public EnemyRoom2(Coord centre, int[,] map)
    {
        CentreTile = centre;
        roomType = 3;
        //Debug.Log("creating enemy room");
        tiles = new List<Coord>();
        for (int x = -10 + centre.tileX; x < 10 + centre.tileX; x++)
        {
            for (int y = -10 + centre.tileY; y < 10 + centre.tileY; y++)
            {
                map[x, y] = 0;
                tiles.Add(new Coord(x, y));
            }
        }
        for (int x = 10 + centre.tileX; x < 15 + centre.tileX; x++)
        {
            for (int y = 5 + centre.tileY; y < 10 + centre.tileY; y++)
            {
                map[x, y] = 0;
                tiles.Add(new Coord(x, y));
            }
        }
        //Debug.Log(tiles.Count);
        roomSize = tiles.Count;
        connectedRooms = new List<Room>();
        edgeTiles = new List<Coord>();
        foreach (Coord tile in tiles)
        {
            for (int x = tile.tileX - 1; x <= tile.tileX + 1; x++)
            {
                for (int y = tile.tileY - 1; y <= tile.tileY + 1; y++)
                {
                    if (map[x, y] == 1)
                    {
                        edgeTiles.Add(tile);
                    }
                }
            }
        }
        //Debug.Log(edgeTiles.Count);
    }

}