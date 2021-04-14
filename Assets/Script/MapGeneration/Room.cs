using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : IComparable<Room>
{
    public bool isMainRoom;
    public bool isAccessibleFromMainRoom;
    public List<Coord> tiles;
    public List<Coord> edgeTiles;
    public List<Coord> hallWayTiles;
    public List<Coord> edgeWallTiles;
    public List<Room> connectedRooms;
    public int roomSize;
    public float spaceRequirement;
    public int roomType;
    public Coord CentreTile;
    public Room() { }
    protected int width;
    protected int height;
    public Room(List<Coord> roomTiles, int[,] map)
    {
        isMainRoom = false;
        isAccessibleFromMainRoom = false;
        roomType = -1;
        tiles = roomTiles;
        roomSize = tiles.Count;
        connectedRooms = new List<Room>();
        edgeTiles = new List<Coord>();
        edgeWallTiles = new List<Coord>();
        hallWayTiles = new List<Coord>(); ;
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

    public void SetAccessibleFromMainRoom()
    {
        if (!isAccessibleFromMainRoom)
        {
            isAccessibleFromMainRoom = true;
            foreach (Room connectedRoom in connectedRooms)
            {
                connectedRoom.SetAccessibleFromMainRoom();

            }
        }
    }

    public static void ConnectRooms(Room roomA, Room roomB)
    {
        if (roomA.isAccessibleFromMainRoom)
        {
            roomB.SetAccessibleFromMainRoom();
        }
        else if (roomB.isAccessibleFromMainRoom)
        {
            roomA.SetAccessibleFromMainRoom();
        }
        roomA.connectedRooms.Add(roomB);
        roomB.connectedRooms.Add(roomA);
    }

    public int CompareTo(Room otherRoom)
    {
        return otherRoom.roomSize.CompareTo(roomSize);
    }

    public bool IsConnected(Room otherRoom)
    {
        return connectedRooms.Contains(otherRoom);
    }
    public void SetEdgeTilesNewType(int[,] map, int type)
    {
        foreach (Coord wallEdgeTile in edgeWallTiles)
        {
            if (map[wallEdgeTile.tileX, wallEdgeTile.tileY] != 1)
            {
                map[wallEdgeTile.tileX, wallEdgeTile.tileY] = type;
            }
        }
    }
    public void SetRoomBorders(Coord centre, int[,] map)
    {
        edgeTiles = new List<Coord>();
        edgeWallTiles = new List<Coord>();
        hallWayTiles = new List<Coord>(); ;
        connectedRooms = new List<Room>();
        tiles = new List<Coord>();
        for (int x = -(width) / 2 + centre.tileX; x < (width) / 2 + centre.tileX; x++)
        {
            for (int y = -(height) / 2 + centre.tileY; y < (height) / 2 + centre.tileY; y++)
            {
                if (map[x, y] == 0) tiles.Add(new Coord(x, y));
            }
        }
        roomSize = tiles.Count;
        Debug.Log("Room size: " + roomSize);
        foreach (Coord tile in tiles)
        {
            for (int x = tile.tileX - 1; x <= tile.tileX + 1; x++)
            {
                for (int y = tile.tileY - 1; y <= tile.tileY + 1; y++)
                {
                    if (map[x, y] == 1)
                    {
                        edgeTiles.Add(tile);
                        edgeWallTiles.Add(new Coord(x, y));
                    }
                }
            }
        }
        Debug.Log("Roomstats\nRoom size: " + roomSize + "\nEdge tiles: " + edgeTiles.Count + "\nEdge wall tiles: " + edgeWallTiles.Count);
    }
}
