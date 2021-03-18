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
                else
                    map[x, y] = 1;

            }
        }
        Room roomA = new Room(roomACoords, map);
        Room roomB = new Room(roomBCoords, map);
        Room roomC = new Room(roomCCoords, map);
        Room roomD = new Room(roomDCoords, map);

        List<Room> allRooms = new List<Room>();
        allRooms.Add(roomA);
        allRooms.Add(roomB);
        allRooms.Add(roomC);
        allRooms.Add(roomD);
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
                    Gizmos.DrawCube(pos, Vector3.one);
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
        bool possibleConnectionFound = false;

        foreach (Room roomA in AllRooms)
        {


            foreach (Room roomB in AllRooms)
            {

                if (roomA == roomB)
                {
                    continue;
                }
                if (roomA.IsConnected(roomB))
                {
                    possibleConnectionFound = false;
                    break;
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

    struct Coord
    {
        public int tileX;
        public int tileY;

        public Coord(int x, int y)
        {
            tileX = x;
            tileY = y;
        }
    }

    class Room
    {
        public List<Coord> tiles;
        public List<Coord> edgeTiles;
        public List<Room> connectedRooms;
        public int roomSize;

        public Room()
        {

        }

        public Room(List<Coord> roomTiles, int[,] map)
        {
            tiles = roomTiles;
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
        public static void ConnectRooms(Room roomA, Room roomB)
        {
            roomA.connectedRooms.Add(roomB);
            roomB.connectedRooms.Add(roomA);
        }
        public bool IsConnected(Room otherRoom)
        {
            return connectedRooms.Contains(otherRoom);
        }
    }
}
