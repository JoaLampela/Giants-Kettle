using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGeneration : MonoBehaviour
{
    public int width = 30;
    public int height = 30;

    [Range(0, 100)]
    public int randomFillPrecent;

    int[,] map;

    private void Start()
    {
        GenerateMap();
    }

    private void GenerateMap()
    {
        map = new int[width, height];
        List<Coord> roomACoords = new List<Coord>();
        List<Coord> roomBCoords = new List<Coord>();
        List<Coord> roomCCoords = new List<Coord>();
        List<Coord> roomDCoords = new List<Coord>();
        List<Coord> roomECoords = new List<Coord>();
        List<Coord> roomFCoords = new List<Coord>();
        List<Coord> roomGCoords = new List<Coord>();
        List<Coord> roomHCoords = new List<Coord>();
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if ((10 < x && x < 30) && (30 < y && y < 40))
                {
                    roomACoords.Add(new Coord(x, y));
                    map[x, y] = 0;
                }
                else if ((2 < x && x < 10) && (10 < y && y < 20))
                {
                    roomBCoords.Add(new Coord(x, y));
                    map[x, y] = 0;
                }
                else if ((35 < x && x < 45) && (5 < y && y < 19))
                {
                    roomCCoords.Add(new Coord(x, y));
                    map[x, y] = 0;
                }
                else if ((30 < x && x < 45) && (20 < y && y < 35))
                {
                    roomDCoords.Add(new Coord(x, y));
                    map[x, y] = 0;
                }
                else if ((50 < x && x < 60) && (45 < y && y < 55))
                {
                    roomECoords.Add(new Coord(x, y));
                    map[x, y] = 0;
                }
                else if ((61 < x && x < 70) && (45 < y && y < 55))
                {
                    roomFCoords.Add(new Coord(x, y));
                    map[x, y] = 0;
                }
                else if ((50 < x && x < 60) && (56 < y && y < 66))
                {
                    roomGCoords.Add(new Coord(x, y));
                    map[x, y] = 0;
                }
                else if ((61 < x && x < 70) && (55 < y && y < 66))
                {
                    roomHCoords.Add(new Coord(x, y));
                    map[x, y] = 0;
                }
                else
                    map[x, y] = 1;

            }
        }
        Room roomA = new Room(roomACoords, map);
        Room roomB = new Room(roomBCoords, map);
        Room roomC = new Room(roomCCoords, map);
        Room roomD = new Room(roomDCoords, map);
        Room roomE = new Room(roomECoords, map);
        Room roomF = new Room(roomFCoords, map);
        Room roomG = new Room(roomGCoords, map);
        Room roomH = new Room(roomHCoords, map);

        List<Room> allRooms = new List<Room>();
        allRooms.Add(roomA);
        allRooms.Add(roomB);
        allRooms.Add(roomC);
        allRooms.Add(roomD);
        allRooms.Add(roomE);
        allRooms.Add(roomF);
        allRooms.Add(roomG);
        allRooms.Add(roomH);
        ConnectClosingRooms(allRooms);
    }

    private void OnDrawGizmos()
    {
        if (map != null)
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Gizmos.color = (map[x, y] == 1) ? Color.black : Color.white;
                    Vector2 pos = new Vector2(-width / 2 + x + 0.5f, -height / 2 + y + 0.5f);
                    Gizmos.DrawCube(pos, Vector3.one * 0.9f);
                }
            }
    }

    void ConnectClosingRooms(List<Room> AllRooms)
    {
        int bestDistance = 0;
        Coord bestTileA = new Coord();
        Coord bestTileB = new Coord();
        Room bestRoomA = new Room();
        Room bestRoomB = new Room();


        foreach (Room roomA in AllRooms)
        {
            bool possibleConnectionFound = false;

            foreach (Room roomB in AllRooms)
            {

                if (roomA == roomB)
                {
                    continue;
                }
                if (roomA.IsConnected(roomB))
                {
                    possibleConnectionFound = false;
                    continue;
                }

                for (int tileIndexA = 0; tileIndexA < roomA.edgeTiles.Count; tileIndexA++)
                    for (int tileIndexB = 0; tileIndexB < roomB.edgeTiles.Count; tileIndexB++)
                    {
                        Coord tileA = roomA.edgeTiles[tileIndexA];
                        Coord tileB = roomB.edgeTiles[tileIndexB];
                        int distanceBetweenRooms = (int)(Mathf.Pow(tileA.tileX - tileB.tileX, 2) + Mathf.Pow(tileA.tileY - tileB.tileY, 2));
                        if (distanceBetweenRooms < bestDistance || !possibleConnectionFound)
                        {
                            bestDistance = distanceBetweenRooms;
                            possibleConnectionFound = true;
                            bestTileA = tileA;
                            bestTileB = tileB;
                            bestRoomA = roomA;
                            bestRoomB = roomB;
                        }
                    }
            }
            if (possibleConnectionFound)
            {
                CreatePassage(bestRoomA, bestRoomB, bestTileA, bestTileB);
            }
        }
    }

    void CreatePassage(Room roomA, Room roomB, Coord tileA, Coord tileB)
    {
        Debug.Log("connecting" + roomA + "" + roomB);
        Room.ConnectRooms(roomA, roomB);
        Debug.DrawLine(CoordToWorldPoint(tileA), CoordToWorldPoint(tileB), Color.green, 100);
    }

    Vector2 CoordToWorldPoint(Coord tile)
    {
        return new Vector2(-width / 2 + .5f + tile.tileX, -height / 2 + .5f + tile.tileY);
    }

}
