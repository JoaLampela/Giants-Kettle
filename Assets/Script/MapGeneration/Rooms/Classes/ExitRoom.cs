using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitRoom : Room
{
    public ExitRoom(Coord centre, int[,] map)
    {
        CentreTile = centre;
        roomType = 1;
        tiles = new List<Coord>();
        int r = 8;
        width = 18;
        height = 18;

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