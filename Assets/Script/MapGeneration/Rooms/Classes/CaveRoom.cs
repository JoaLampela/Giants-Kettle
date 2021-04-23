using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CaveRoom : Room
{
    private bool useRandomSeed = true;
    private string seed;
    private int randomFillPrecent = 59;



    public CaveRoom(Coord centre, int[,] map)
    {
        width = 30;
        height = 30;
        roomType = 4;
        CentreTile = centre;

        RandomFillMap(centre, map);
        for (int i = 0; i < 2; i++)
            SmoothMap(centre, map);
        ProcessMap(centre, map);
        SetRoomBorders(centre, map);
    }

    private void RandomFillMap(Coord centre, int[,] map)
    {




        for (int x = -width / 2 + centre.tileX; x < width / 2 + centre.tileX; x++)
        {
            for (int y = -height / 2 + centre.tileY; y < height / 2 + centre.tileY; y++)
            {
                map[x, y] = (Random.Range(0, 100) < randomFillPrecent) ? 0 : 1;
            }
        }
    }

    void SmoothMap(Coord centre, int[,] map)
    {
        for (int x = -width / 2 + centre.tileX; x < width / 2 + centre.tileX; x++)
        {
            for (int y = -height / 2 + centre.tileY; y < height / 2 + centre.tileY; y++)
            {
                int neighbourWallTiles = GetSorroundingWallCount(x, y, map);
                if (neighbourWallTiles > 4)
                    map[x, y] = 1;
                else if (neighbourWallTiles < 4)
                    map[x, y] = 0;
            }
        }
    }
    int GetSorroundingWallCount(int gridX, int gridY, int[,] map)
    {
        int wallCount = 0;
        for (int neighbourX = gridX - 1; neighbourX <= gridX + 1; neighbourX++)
            for (int neighbourY = gridY - 1; neighbourY <= gridY + 1; neighbourY++)
            {

                if (neighbourX != gridX || neighbourY != gridY)
                    wallCount += map[neighbourX, neighbourY];
            }

        return wallCount;
    }

    void ProcessMap(Coord centre, int[,] map)
    {
        //Debug.Log("Process map");
        List<List<Coord>> wallregions = GetRegions(0, centre, map);
        int largestListSize = 0;
        int largestListIndex = 0;
        //Debug.Log("Wallregion count" + wallregions.Count);
        for (int i = 0; i < wallregions.Count; i++)
        {
            if (wallregions[i].Count > largestListSize)
            {
                largestListSize = wallregions[i].Count;
                largestListIndex = i;
                //Debug.Log("so far found index of largest list: " + i);
            }
        }
        //Debug.Log("Largest list size: " + largestListSize + " Largest list: " + largestListIndex);
        List<Coord> tileList = new List<Coord>();

        foreach (List<Coord> coordList in wallregions)
        {
            foreach (Coord coord in coordList)
            {
                tileList.Add(new Coord(coord.tileX, coord.tileY));
            }
        }
        //remove duplicates
        tileList = tileList.Distinct().ToList();
        foreach (Coord coord in tileList)
        {
            if (!wallregions[largestListIndex].Contains(coord))
            {
                map[coord.tileX, coord.tileY] = 1;
            }
        }


    }

    List<List<Coord>> GetRegions(int tileType, Coord centre, int[,] map)
    {
        Debug.Log("fetching regions");
        List<List<Coord>> regions = new List<List<Coord>>();
        int[,] mapFlags = new int[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < width; y++)
            {
                if (mapFlags[x, y] == 0 && map[x + centre.tileX - width / 2, y + centre.tileY - height / 2] == tileType)
                {
                    List<Coord> newRegion = GetRegionTiles(x, y, centre, map);
                    regions.Add(newRegion);
                    foreach (Coord tile in newRegion)
                    {
                        mapFlags[tile.tileX - centre.tileX + width / 2, tile.tileY - centre.tileY + height / 2] = 1;
                    }
                }
            }
        }
        return regions;
    }

    List<Coord> GetRegionTiles(int startX, int startY, Coord centre, int[,] map)
    {
        //Debug.Log("Fetching region tiles");
        List<Coord> tiles = new List<Coord>();
        int[,] mapFlags = new int[width, height];
        int tileType = map[startX + centre.tileX - width / 2, startY + centre.tileY - height / 2];

        Queue<Coord> queue = new Queue<Coord>();
        queue.Enqueue(new Coord(startX + centre.tileX - width / 2, startY + centre.tileY - height / 2));
        mapFlags[startX, startY] = 1;

        while (queue.Count > 0)
        {
            Coord tile = queue.Dequeue();
            tiles.Add(tile);
            for (int x = tile.tileX - 1; x <= tile.tileX + 1; x++)
                for (int y = tile.tileY - 1; y <= tile.tileY + 1; y++)
                {
                    if (x > centre.tileX - width / 2 && x < centre.tileX + width / 2 && y > centre.tileY - height / 2 && y < centre.tileY + height / 2)
                        if (y == tile.tileY || x == tile.tileX)
                        {
                            if (mapFlags[x - centre.tileX + width / 2, y - centre.tileY + height / 2] == 0 && map[x, y] == tileType)
                            {
                                mapFlags[x - centre.tileX + width / 2, y - centre.tileY + height / 2] = 1;
                                queue.Enqueue(new Coord(x, y));
                                //Debug.Log("Queueboi");
                            }
                        }
                }
        }
        return tiles;
    }

}
