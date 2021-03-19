using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGeneration : MonoBehaviour
{
    [Header("Room spawn amount settings")]
    public int width = 200;
    public int height = 200;

    [Header("Room spawn settings")]
    public int[] roomArray;
    [Range(1, 5)]
    public int tunnelWidth;
    [Header("Room prefabs")]
    public GameObject spawnRoom;
    public GameObject exitRoom;
    public GameObject enemyRoom1;

    [Header("Room space requirements")]
    [Range(5, 20)]
    public int enemyRoom1SpaceRequired = 6;
    [Range(4, 30)]
    public int spawnRoomSpaceRequired;
    [Range(6, 30)]
    public int exitRoomSpaceRequired;

    int[,] map;

    private MeshGenerator meshGenerator;

    private void Start()
    {
        meshGenerator = GetComponent<MeshGenerator>();
        GenerateMap();
    }


    private void GenerateMap()
    {
        map = new int[width, height];
        List<Room> allRooms = new List<Room>();
        bool mapGenerated = false;
        while (!mapGenerated)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    map[x, y] = 1;
                }
            }
            allRooms.Clear();
            for (int i = 0; i < roomArray.Length; i++)
            {
                bool roomSpawned = false;
                int cannotSpawnRoomCounter = 0;
                while (!roomSpawned && cannotSpawnRoomCounter < 30)
                {
                    int x = Random.Range(50, width - 50);
                    int y = Random.Range(50, height - 50);
                    if (roomArray[i] == 0)
                    {
                        if (isCircleEmptyWalls(new Coord(x, y), enemyRoom1SpaceRequired))
                        {
                            Room room = new SpawnRoom(new Coord(x, y), map);
                            allRooms.Add(room);
                            roomSpawned = true;
                        }
                    }
                    else if (roomArray[i] == 1)
                    {
                        if (isCircleEmptyWalls(new Coord(x, y), spawnRoomSpaceRequired))
                        {
                            Room room = new ExitRoom(new Coord(x, y), map);
                            allRooms.Add(room);
                            roomSpawned = true;
                        }
                    }
                    else if (roomArray[i] == 2)
                    {
                        if (isCircleEmptyWalls(new Coord(x, y), enemyRoom1SpaceRequired))
                        {
                            Room room = new EnemyRoom1(new Coord(x, y), map);
                            allRooms.Add(room);
                            roomSpawned = true;
                        }
                    }
                    cannotSpawnRoomCounter++;
                }
                if (!roomSpawned)
                    break;
            }
            if (allRooms.Count == roomArray.Length)
                mapGenerated = true;

        }
        ConnectClosingRooms(allRooms);
        InstansiateRooms(allRooms);

        //syytä kerttuu jos tää kaatuu tähän:
        meshGenerator.GenerateMesh(map, 1);

    }

    private void OnDrawGizmos()
    {
        /*made changes here
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
        */
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
        List<Coord> line = GetLine(tileA, tileB);

        foreach (Coord point in line)
        {
            DrawCircle(point, tunnelWidth);
        }
    }

    void DrawCircle(Coord c, int r)
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
    bool isCircleEmptyWalls(Coord c, int r)
    {
        for (int x = -r; x <= r; x++)
            for (int y = -r; y <= r; y++)
            {
                if (x * x + y * y <= r * r)
                {
                    int checkingX = c.tileX + x;
                    int checkingY = c.tileY + y;
                    if (map[checkingX, checkingY] == 0)
                    {
                        Debug.Log("we did it mom");
                        return false;
                    }

                }
            }
        return true;
    }

    List<Coord> GetLine(Coord from, Coord to)
    {
        List<Coord> line = new List<Coord>();
        int x = from.tileX;
        int y = from.tileY;

        int dx = to.tileX - from.tileX;
        int dy = to.tileY - from.tileY;
        bool inverted = false;
        int step = System.Math.Sign(dx);
        int gradientStep = System.Math.Sign(dy);

        int longest = Mathf.Abs(dx);
        int shortest = Mathf.Abs(dy);

        if (longest < shortest)
        {
            inverted = true;
            longest = Mathf.Abs(dy);
            shortest = Mathf.Abs(dx);

            step = System.Math.Sign(dy);
            gradientStep = System.Math.Sign(dx);
        }
        int gradientAccumulation = longest / 2;
        for (int i = 0; i < longest; i++)
        {
            line.Add(new Coord(x, y));

            if (inverted)
                y += step;
            else
                x += step;

            gradientAccumulation += shortest;
            if (gradientAccumulation >= longest)
            {
                if (inverted)
                    x += gradientStep;
                else
                    y += gradientStep;
                gradientAccumulation -= longest;
            }
        }
        return line;
    }
    Vector2 CoordToWorldPoint(Coord tile)
    {
        return new Vector2(-width / 2 + .5f + tile.tileX, -height / 2 + .5f + tile.tileY);
    }

    public void InstansiateRooms(List<Room> allRooms)
    {
        foreach (Room room in allRooms)
        {

            switch (room.roomType)
            {
                case 0:
                    {
                        Instantiate(spawnRoom, new Vector2(-width / 2 + .5f + room.CentreTile.tileX, -height / 2 + .5f + room.CentreTile.tileY), Quaternion.identity);
                    }
                    break;
                case 1:
                    {
                        Instantiate(exitRoom, new Vector2(-width / 2 + .5f + room.CentreTile.tileX, -height / 2 + .5f + room.CentreTile.tileY), Quaternion.identity);
                    }
                    break;
                case 2:
                    {
                        Instantiate(enemyRoom1, new Vector2(-width / 2 + .5f + room.CentreTile.tileX, -height / 2 + .5f + room.CentreTile.tileY), Quaternion.identity);
                    }
                    break;
            }
        }
    }
}
