using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRoom2 : Room
{
    public EnemyRoom2(Coord centre, int[,] map)
    {
        CentreTile = centre;
        roomType = 3;
        width = 30;
        height = 20;
        for (int x = -10 + centre.tileX; x < 10 + centre.tileX; x++)
        {
            for (int y = -10 + centre.tileY; y < 10 + centre.tileY; y++)
            {
                map[x, y] = 0;
            }
        }
        for (int x = -2 + centre.tileX; x < 2 + centre.tileX; x++)
        {
            for (int y = -2 + centre.tileY; y < 2 + centre.tileY; y++)
            {
                map[x, y] = 1;
            }
        }
        map[-10 + centre.tileX, -10 + centre.tileY] = 1;
        map[9 + centre.tileX, -10 + centre.tileY] = 1;
        map[-10 + centre.tileX, 9 + centre.tileY] = 1;
        map[9 + centre.tileX, 9 + centre.tileY] = 1;



        SetRoomBorders(centre, map);
    }

}