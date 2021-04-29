using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRoom1 : Room
{
    public EnemyRoom1(Coord centre, int[,] map)
    {
        CentreTile = centre;
        roomType = 2;
        width = 13;
        height = 13;
        for (int x = -width / 2 + centre.tileX; x < width / 2 + centre.tileX; x++)
        {
            for (int y = -height / 2 + centre.tileY; y < height / 2 + centre.tileY; y++)
            {
                map[x, y] = 0;
            }
        }
        SetRoomBorders(centre, map);
    }

}