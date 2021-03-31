using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGeneration : MonoBehaviour
{
    [Header("Room spawn amount settings")]
    public int width = 200;
    public int height = 200;
    public int generationAreaOuterBorder;

    [Header("Room spawn settings")]
    public Level[] levels;
    [Range(1, 5)]
    public int tunnelWidth;
    [Header("Game settings")]
    [Range(0, 100)]
    public int spawnPointCreationPrecentagePerTile;
    [Header("Room prefabs")]
    public GameObject spawnRoom;
    public GameObject exitRoom;
    public GameObject enemyRoom1;
    public GameObject enemyRoom2;
    public GameObject caveRoom;
    [Header("Object prefabs")]
    public GameObject spawnPoint;
    public GameObject item;

    [Header("Room space requirements (max amount should not go over generation area outer border)")]
    [Range(4, 100)]
    public int spawnRoomSpaceRequired;
    [Range(6, 100)]
    public int exitRoomSpaceRequired;
    [Range(5, 20)]
    public int enemyRoom1SpaceRequired = 6;
    [Range(15, 30)]
    public int enemyRoom2SpaceRequired = 16;
    [Range(15, 30)]
    public int caveRoomSpaceRequired = 16;


    private Room mainRoom;
    int[,] map;

    private MeshGenerator meshGenerator;

    private void Start()
    {
        meshGenerator = GetComponent<MeshGenerator>();
        GenerateMap();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject playerSpawnPoint = GameObject.FindGameObjectWithTag("PlayerSpawnPoint");
        player.transform.position = playerSpawnPoint.transform.position;
        //Instantiate(item, playerSpawnPoint.transform.position + Vector2.left);
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
            for (int i = 0; i < levels[0].roomList.Length; i++)
            {
                bool roomSpawned = false;
                int cannotSpawnRoomCounter = 0;
                while (!roomSpawned && cannotSpawnRoomCounter < 30)
                {
                    int x = Random.Range(generationAreaOuterBorder, width - generationAreaOuterBorder);
                    int y = Random.Range(generationAreaOuterBorder, height - generationAreaOuterBorder);
                    if (levels[0].roomList[i] == 0)
                    {
                        if (isCircleEmptyWalls(new Coord(x, y), enemyRoom1SpaceRequired))
                        {
                            Room room = new SpawnRoom(new Coord(x, y), map);
                            allRooms.Add(room);
                            roomSpawned = true;
                        }
                    }
                    else if (levels[0].roomList[i] == 1)
                    {
                        if (isCircleEmptyWalls(new Coord(x, y), spawnRoomSpaceRequired))
                        {
                            Room room = new ExitRoom(new Coord(x, y), map);
                            allRooms.Add(room);
                            roomSpawned = true;
                        }
                    }
                    else if (levels[0].roomList[i] == 2)
                    {
                        if (isCircleEmptyWalls(new Coord(x, y), enemyRoom1SpaceRequired))
                        {
                            Room room = new EnemyRoom1(new Coord(x, y), map);
                            allRooms.Add(room);
                            roomSpawned = true;
                        }
                    }
                    else if (levels[0].roomList[i] == 3)
                    {
                        if (isCircleEmptyWalls(new Coord(x, y), enemyRoom2SpaceRequired))
                        {
                            Room room = new EnemyRoom2(new Coord(x, y), map);
                            allRooms.Add(room);
                            roomSpawned = true;
                        }
                    }
                    else if (levels[0].roomList[i] == 4)
                    {
                        if (isCircleEmptyWalls(new Coord(x, y), caveRoomSpaceRequired))
                        {
                            Room room = new CaveRoom(new Coord(x, y), map);
                            allRooms.Add(room);
                            roomSpawned = true;
                        }
                    }
                    cannotSpawnRoomCounter++;
                }
                if (!roomSpawned)
                    break;
            }
            if (allRooms.Count == levels[0].roomList.Length)
                mapGenerated = true;

        }

        foreach (Room room in allRooms)
        {
            if (room.roomType == 0)
            {
                mainRoom = room;
                Debug.Log("Found a spawnroom and made it into a main room");
                room.isMainRoom = true;
                room.SetAccessibleFromMainRoom();
                break;
            }
        }
        ConnectClosestRooms(allRooms);
        InstansiateRooms(allRooms);
        CreateSpawnPoints(mainRoom);

        //generate mesh of the map
        meshGenerator.GenerateMesh(map, 1);
    }

    private void OnDrawGizmos()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Gizmos.color = map[x, y] == 1 ? Color.black : Color.white;
                Gizmos.DrawCube(new Vector3(x - width / 2 + 0.5f, y - height / 2 + 0.5f, 0), Vector3.one);
            }
        }
    }
    void ConnectClosestRooms(List<Room> allRooms, bool forceAccessibilityFromMainRoom = false)
    {
        List<Room> roomListA = new List<Room>();
        List<Room> roomListB = new List<Room>();

        if (forceAccessibilityFromMainRoom)
        {
            Debug.Log("forcing accessability");
            foreach (Room room in allRooms)
            {
                if (room.isAccessibleFromMainRoom)
                    roomListB.Add(room);
                else
                {
                    roomListA.Add(room);
                }
            }
        }
        else
        {
            roomListA = allRooms;
            roomListB = allRooms;
        }

        int bestDistance = 0;
        Coord bestTileA = new Coord();
        Coord bestTileB = new Coord();
        Room bestRoomA = new Room();
        Room bestRoomB = new Room();
        bool possibleConnectionFound = false;
        foreach (Room roomA in roomListA)
        {
            if (!forceAccessibilityFromMainRoom)
            {
                possibleConnectionFound = false;
                if (roomA.connectedRooms.Count > 0)
                    continue;
            }
            foreach (Room roomB in roomListB)
            {

                if (roomA == roomB || roomA.IsConnected(roomB))
                {
                    continue;
                }

                for (int tileIndexA = 0; tileIndexA < roomA.edgeTiles.Count; tileIndexA++)
                {

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
            }
            if (possibleConnectionFound && !forceAccessibilityFromMainRoom)
            {
                CreatePassage(bestRoomA, bestRoomB, bestTileA, bestTileB);
            }
        }
        if (possibleConnectionFound && forceAccessibilityFromMainRoom)
        {
            CreatePassage(bestRoomA, bestRoomB, bestTileA, bestTileB);
            ConnectClosestRooms(allRooms, true);
        }


        if (!forceAccessibilityFromMainRoom)
        {
            ConnectClosestRooms(allRooms, true);
        }
    }

    void CreatePassage(Room roomA, Room roomB, Coord tileA, Coord tileB)
    {
        Debug.Log("connecting" + roomA + "" + roomB);
        Room.ConnectRooms(roomA, roomB);
        Debug.DrawLine(CoordToWorldPoint(tileA), CoordToWorldPoint(tileB), Color.green, 5);
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
                        GameObject currentRoom = Instantiate(spawnRoom, new Vector2(-width / 2 + .5f + room.CentreTile.tileX, -height / 2 + .5f + room.CentreTile.tileY), Quaternion.identity);
                        currentRoom.transform.parent = gameObject.transform;
                    }
                    break;
                case 1:
                    {
                        GameObject currentRoom = Instantiate(exitRoom, new Vector2(-width / 2 + .5f + room.CentreTile.tileX, -height / 2 + .5f + room.CentreTile.tileY), Quaternion.identity);
                        currentRoom.transform.parent = gameObject.transform;
                    }
                    break;
                case 2:
                    {
                        GameObject currentRoom = Instantiate(enemyRoom1, new Vector2(-width / 2 + .5f + room.CentreTile.tileX, -height / 2 + .5f + room.CentreTile.tileY), Quaternion.identity);
                        currentRoom.transform.parent = gameObject.transform;
                    }
                    break;
                case 3:
                    {
                        GameObject currentRoom = Instantiate(enemyRoom2, new Vector2(-width / 2 + .5f + room.CentreTile.tileX, -height / 2 + .5f + room.CentreTile.tileY), Quaternion.identity);
                        currentRoom.transform.parent = gameObject.transform;
                    }
                    break;
                case 4:
                    {
                        GameObject currentRoom = Instantiate(caveRoom, new Vector2(-width / 2 + .5f + room.CentreTile.tileX, -height / 2 + .5f + room.CentreTile.tileY), Quaternion.identity);
                        currentRoom.transform.parent = gameObject.transform;
                    }
                    break;
            }
        }
    }

    public void CreateSpawnPoints(Room spawnRoom)
    {
        for (int x = generationAreaOuterBorder; x < width - generationAreaOuterBorder; x++)
        {
            for (int y = generationAreaOuterBorder; y < width - generationAreaOuterBorder; y++)
            {
                if (map[x, y] == 0 && !spawnRoom.tiles.Contains(new Coord(x, y)))
                {
                    if (Random.Range(0, 100) < spawnPointCreationPrecentagePerTile)
                    {

                        GameObject spawn = GameObject.Instantiate(spawnPoint, CoordToWorldPoint(new Coord(x, y)), Quaternion.identity);
                        spawn.transform.parent = gameObject.transform;
                    }
                }
            }
        }
    }


    public void ExitLevel()
    {
        //Do it anakin
        foreach (Transform child in gameObject.transform)
        {
            //this doesn't destroy the Walls gameobject
            if (!child.GetComponent<MeshFilter>())
            {
                Destroy(child.gameObject);
            }
        }

        GenerateMap();

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject[] playerSpawnPoint = GameObject.FindGameObjectsWithTag("PlayerSpawnPoint");
        player.transform.position = playerSpawnPoint[1].transform.position;
        Debug.Log(playerSpawnPoint[1].transform.position);
    }
    [System.Serializable]
    public class Level
    {
        public int[] roomList;
    }

}
