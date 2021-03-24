using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRoom1 : Room
{
    public EnemyRoom1(Coord centre, int[,] map)
    {
        CentreTile = centre;
        roomType = 2;
        Debug.Log("creating enemy room");
        tiles = new List<Coord>();
        for (int x = -5 + centre.tileX; x < 5 + centre.tileX; x++)
        {
            for (int y = -5 + centre.tileY; y < 5 + centre.tileY; y++)
            {
                map[x, y] = 0;
                tiles.Add(new Coord(x, y));
            }
        }
        Debug.Log(tiles.Count);
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