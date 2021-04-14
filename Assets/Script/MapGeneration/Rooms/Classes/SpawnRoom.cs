using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRoom : Room
{
    public SpawnRoom(Coord centre, int[,] map)
    {
        CentreTile = centre;
        roomType = 0;
        Debug.Log("creating enemy room");
        tiles = new List<Coord>();
        int r = 6;
        width = 14;
        height = 14;
        for (int x = -r; x <= r; x++)
            for (int y = -r; y <= r; y++)
            {
                if (x * x + y * y <= r * r)
                {
                    int tileCoordX = centre.tileX + x;
                    int tileCoordY = centre.tileY + y;
                    map[tileCoordX, tileCoordY] = 0;
                }
            }
        SetRoomBorders(centre, map);
    }
}