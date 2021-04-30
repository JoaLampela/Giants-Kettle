using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoom : Room
{
    public BossRoom(Coord centre, int[,] map)
    {
        CentreTile = centre;
        roomType = 10;
        width = 30;
        height = 20;
        for (int x = -width / 2 + centre.tileX; x < width / 2 + centre.tileX; x++)
        {
            for (int y = -height / 2 + centre.tileY; y < height / 2 + centre.tileY; y++)
            {
                map[x, y] = 0;
            }
        }
        SetRoomBorders(centre, map);

        height = 40;
        for (int x = -2 + centre.tileX; x < 3 + centre.tileX; x++)
        {
            for (int y = -height + centre.tileY + 15; y < height + centre.tileY - 15; y++)
            {
                map[x, y] = 0;
            }
        }


        DrawCircle(new Coord(centre.tileX, centre.tileY + height - 20), 7, map);
        DrawCircle(new Coord(centre.tileX, centre.tileY - height + 20), 7, map);


    }


    void DrawCircle(Coord c, int r, int[,] map)
    {
        for (int x = -r; x <= r; x++)
            for (int y = -r; y <= r; y++)
            {
                if (x * x + y * y <= r * r)
                {
                    int drawX = c.tileX + x;
                    int drawY = c.tileY + y;
                    map[drawX, drawY] = 0;
                }
            }
    }
}
