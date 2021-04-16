using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRoom : Room
{
    public TestRoom(Coord centre, int[,] map)
    {
        CentreTile = centre;
        roomType = 5;
        
        
        //this determines everything, make it uneven
        width = 33;
        height = width / 2 + 1;

        int counter = width / 2;
        for (int y = centre.tileY - height / 2; y <= centre.tileY + height / 2; y++) {
            for (int x = centre.tileX - counter; x <= centre.tileX + counter; x++) {
                int tileCoordX = x;
                int tileCoordY = y;
                map[tileCoordX, tileCoordY] = 0;
            }
            counter--;
        }

        map[centre.tileX, centre.tileY] = 1;
        map[centre.tileX, centre.tileY + 2] = 1;
        map[centre.tileX, centre.tileY - 2] = 1;

        //eye
        //map[centre.tileX - 3, centre.tileY - 3] = 1;


        for (int y = centre.tileY - 2; y <= centre.tileY + 2; y++)
        {
            for (int x = centre.tileX - 2; x <= centre.tileX + 2; x++)
            {
                //if y = 0 or 3, or if x = 0 or 3
                if (x - centre.tileX == -2 || y - centre.tileY == -2 || x - centre.tileX == 2 || y - centre.tileY == 2)
                {
                    //if the coordinates are not a corner
                    if (((x - centre.tileX) + (y - centre.tileY)) != 0 && ((x - centre.tileX) + (y - centre.tileY) != 4) && ((x - centre.tileX) + (y - centre.tileY) != -4)) {
                        Debug.Log("X JA Y " + (x - centre.tileX) + " " + (y - centre.tileY));
                        Debug.Log("SUMMA " + ((x - centre.tileX) + (y - centre.tileY)));
                        if (y == centre.tileY)
                            {
                                if (x == centre.tileX - 2)
                                {
                                    map[x - 1, y] = 1;
                                    continue;
                                }
                                else
                                {
                                    map[x + 1, y] = 1;
                                    continue;
                                }
                        }

                        map[x, y] = 1;
                    }
                }
            }
        }

        SetRoomBorders(centre, map);
    }
   
}

