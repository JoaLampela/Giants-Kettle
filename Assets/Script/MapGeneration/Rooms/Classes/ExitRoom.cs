using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitRoom : Room
{
    public ExitRoom(Coord centre, int[,] map)
    {
        CentreTile = centre;
        roomType = 1;
        Debug.Log("creating enemy room");
        tiles = new List<Coord>();
        int r = 8;
        for (int x = -r; x <= r; x++)
            for (int y = -r; y <= r; y++)
            {
                if (x * x + y * y <= r * r)
                {
                    int tileCoordX = centre.tileX + x;
                    int tileCoordY = centre.tileY + y;
                    map[tileCoordX, tileCoordY] = 0;
                    tiles.Add(new Coord(tileCoordX, tileCoordY));
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
        Debug.Log(edgeTiles.Count);
    }
}