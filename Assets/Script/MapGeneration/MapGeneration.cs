using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGeneration : MonoBehaviour
{
    [Header("Size settings")]
    public float gridSize = 2;

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
    public int levelsPassed;
    [Range(0, 100)]
    public int combatRoomSpawnPointCreationPrecentagePerTile;
    [Header("Room prefabs")]
    public GameObject spawnRoom;
    public GameObject exitRoom;
    public GameObject enemyRoom1;
    public GameObject enemyRoom2;
    public GameObject caveRoom;
    public GameObject testRoom;
    public GameObject bossRoom;
    [Header("Object prefabs")]
    public GameObject combatRoomSpawner;
    public GameObject spawnPoint;
    public GameObject item;
    public GameObject testPlantPrefab;
    public GameObject testParticle;


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
    [Range(15, 30)]
    public int testRoomSpaceRequired = 16;
    [Range(40, 60)]
    public int bossRoomSpaceRequired = 50;

    internal MapNode[,] mapNodes;
    internal int[,] map;
    private Room mainRoom;
    private MapNode playerLocationNode;



    private MeshGenerator meshGenerator;

    private void Start()
    {
        meshGenerator = GetComponent<MeshGenerator>();
        GenerateMap();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject playerSpawnPoint = GameObject.FindGameObjectWithTag("PlayerSpawnPoint");
        player.transform.position = playerSpawnPoint.transform.position;
        GameObject.FindGameObjectWithTag("UI").GetComponent<PauseMenu>().StopLoad();
        //Instantiate(item, playerSpawnPoint.transform.position + Vector2.left);
    }
    private void LateUpdate()
    {
        UpdatePlayerLocationNode();
    }

    private void UpdatePlayerLocationNode()
    {
        Transform playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        playerLocationNode = mapNodes[Mathf.RoundToInt((float)(width - (width / 2 - playerTransform.position.x / gridSize))), Mathf.RoundToInt((float)height - (height / 2 - playerTransform.position.y / gridSize))];
        if (playerLocationNode != null)
            playerLocationNode.LocationAction();
    }
    private void GenerateMap()
    {
        mapNodes = new MapNode[width, height];
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
            for (int i = 0; i < levels[levelsPassed].roomList.Length; i++)
            {
                bool roomSpawned = false;
                int cannotSpawnRoomCounter = 0;
                while (!roomSpawned && cannotSpawnRoomCounter < 30)
                {
                    int x = Random.Range(generationAreaOuterBorder, width - generationAreaOuterBorder);
                    int y = Random.Range(generationAreaOuterBorder, height - generationAreaOuterBorder);
                    if (levels[levelsPassed].roomList[i] == 0)
                    {
                        if (isCircleEmptyWalls(new Coord(x, y), enemyRoom1SpaceRequired))
                        {
                            Room room = new SpawnRoom(new Coord(x, y), map);
                            allRooms.Add(room);
                            roomSpawned = true;
                        }
                    }
                    else if (levels[levelsPassed].roomList[i] == 1)
                    {
                        if (isCircleEmptyWalls(new Coord(x, y), spawnRoomSpaceRequired))
                        {
                            Room room = new ExitRoom(new Coord(x, y), map);
                            allRooms.Add(room);
                            roomSpawned = true;
                        }
                    }
                    else if (levels[levelsPassed].roomList[i] == 2)
                    {
                        if (isCircleEmptyWalls(new Coord(x, y), enemyRoom1SpaceRequired))
                        {
                            Room room = new EnemyRoom1(new Coord(x, y), map);
                            allRooms.Add(room);
                            roomSpawned = true;
                        }
                    }
                    else if (levels[levelsPassed].roomList[i] == 3)
                    {
                        if (isCircleEmptyWalls(new Coord(x, y), enemyRoom2SpaceRequired))
                        {
                            Room room = new EnemyRoom2(new Coord(x, y), map);
                            allRooms.Add(room);
                            roomSpawned = true;
                        }
                    }
                    else if (levels[levelsPassed].roomList[i] == 4)
                    {
                        if (isCircleEmptyWalls(new Coord(x, y), caveRoomSpaceRequired))
                        {
                            Room room = new CaveRoom(new Coord(x, y), map);
                            allRooms.Add(room);
                            roomSpawned = true;
                        }
                    }
                    else if (levels[levelsPassed].roomList[i] == 5)
                    {
                        if (isCircleEmptyWalls(new Coord(x, y), testRoomSpaceRequired))
                        {
                            Room room = new TestRoom(new Coord(x, y), map);
                            allRooms.Add(room);
                            roomSpawned = true;
                        }
                    }
                    else if (levels[levelsPassed].roomList[i] == 10)
                    {
                        if (isCircleEmptyWalls(new Coord(x, y), bossRoomSpaceRequired))
                        {
                            Room room = new BossRoom(new Coord(x, y), map);
                            allRooms.Add(room);
                            roomSpawned = true;
                        }
                    }
                    cannotSpawnRoomCounter++;
                }
                if (!roomSpawned)
                    break;
            }
            if (allRooms.Count == levels[levelsPassed].roomList.Length)
                mapGenerated = true;

        }

        foreach (Room room in allRooms)
        {
            if (room.roomType == 0 || room.roomType == 10)
            {
                mainRoom = room;
                //Debug.Log("Found a spawnroom and made it into a main room");
                room.isMainRoom = true;
                room.SetAccessibleFromMainRoom();
                break;
            }
        }
        ConnectClosestRooms(allRooms);

        if (allRooms.Count != 1)
            ConnectExtraRooms(allRooms, 6);
        foreach (Room room in allRooms)
            room.SetEdgeTilesNewType(map, 2);
        AddRoomsToMapNodes(allRooms);
        InstansiateRooms(allRooms);

        CreateSpawnPoints(mainRoom);

        //generate mesh of the map
        meshGenerator.GenerateMesh(map, gridSize);
        //GeneratePlants();

    }

    private void OnDrawGizmos()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                switch (map[x, y])
                {
                    case 0:
                        Gizmos.color = Color.white;
                        break;
                    case 1:
                        Gizmos.color = Color.black;
                        break;
                    case 2:
                        Gizmos.color = Color.red;
                        break;
                    default:
                        Gizmos.color = Color.yellow;
                        break;
                }

                Gizmos.DrawCube(new Vector3(x * 2 - width + 0.5f, y * 2 - height + 0.5f, 0), Vector3.one * 2);
            }
        }
    }

    void AddRoomsToMapNodes(List<Room> allRooms)
    {
        foreach (Room owningRoom in allRooms)
        {
            foreach (Coord tile in owningRoom.tiles)
            {
                mapNodes[tile.tileX, tile.tileY] = new MapNode(tile, owningRoom);
            }
        }
    }
    void ConnectClosestRooms(List<Room> allRooms, bool forceAccessibilityFromMainRoom = false)
    {
        List<Room> roomListA = new List<Room>();
        List<Room> roomListB = new List<Room>();

        if (forceAccessibilityFromMainRoom)
        {
            //Debug.Log("forcing accessability");
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


    void ConnectExtraRooms(List<Room> allRooms, int amountOfNewConnections)
    {
        List<Room> roomListA = new List<Room>();
        List<Room> roomListB = new List<Room>();
        foreach (Room room in allRooms)
        {
            roomListB.Add(room);
        }

        List<int> connectableRoomIntergerList = new List<int>();
        for (int i = 0; i < amountOfNewConnections; i++)
        {

            int randomInt = Random.Range(0, allRooms.Count);
            while (connectableRoomIntergerList.Contains(randomInt))
                randomInt = Random.Range(0, allRooms.Count);
            connectableRoomIntergerList.Add(randomInt);
            roomListA.Add(allRooms[randomInt]);
            roomListB.Remove(allRooms[randomInt]);

        }

        int bestDistance = 0;
        Coord bestTileA = new Coord();
        Coord bestTileB = new Coord();
        Room bestRoomA = new Room();
        Room bestRoomB = new Room();
        bool possibleConnectionFound = false;


        foreach (Room roomA in roomListA)
        {
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
            if (possibleConnectionFound)
            {
                CreatePassage(bestRoomA, bestRoomB, bestTileA, bestTileB);
            }
        }

    }


    void CreatePassage(Room roomA, Room roomB, Coord tileA, Coord tileB)
    {
        //Debug.Log("connecting" + roomA + "" + roomB);
        Room.ConnectRooms(roomA, roomB);
        //Debug.DrawLine(CoordToWorldPoint(tileA), CoordToWorldPoint(tileB), Color.green, 5);
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
                        //Debug.Log("we did it mom");
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

        if (longest <= shortest)
        {
            inverted = true;
            longest = Mathf.Abs(dy);
            shortest = Mathf.Abs(dx);

            step = System.Math.Sign(dy);
            gradientStep = System.Math.Sign(dx);
        }
        int gradientAccumulation = longest / 2;
        for (int i = 0; i <= longest; i++)
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
    public Vector2 CoordToWorldPoint(Coord tile)
    {
        return new Vector2((-width / 2 + tile.tileX + 0.5f) * gridSize, (-height / 2 + tile.tileY - 0.5f) * gridSize);
    }

    public void InstansiateRooms(List<Room> allRooms)
    {
        foreach (Room room in allRooms)
        {
            Vector2 roomPosition = new Vector2((-width / 2 + room.CentreTile.tileX + 0.5f) * gridSize, (-height / 2 + room.CentreTile.tileY - 0.5f) * gridSize);
            switch (room.roomType)
            {
                case 0:
                    {
                        GameObject currentRoom = Instantiate(spawnRoom, roomPosition, Quaternion.identity);
                        currentRoom.transform.parent = gameObject.transform;
                        room.roomObject = currentRoom;
                    }
                    break;
                case 1:
                    {
                        GameObject currentRoom = Instantiate(exitRoom, roomPosition, Quaternion.identity);
                        currentRoom.transform.parent = gameObject.transform;
                        room.roomObject = currentRoom;
                    }
                    break;
                case 2:
                    {
                        GameObject currentRoom = Instantiate(enemyRoom1, roomPosition, Quaternion.identity);
                        currentRoom.transform.parent = gameObject.transform;
                        room.roomObject = currentRoom;
                        currentRoom.GetComponent<DoorScript>().MakeDoors(room);
                    }
                    break;
                case 3:
                    {
                        GameObject currentRoom = Instantiate(enemyRoom2, roomPosition, Quaternion.identity);
                        currentRoom.transform.parent = gameObject.transform;
                        room.roomObject = currentRoom;
                        currentRoom.GetComponent<DoorScript>().MakeDoors(room);
                    }
                    break;
                case 4:
                    {
                        GameObject currentRoom = Instantiate(caveRoom, roomPosition, Quaternion.identity);
                        currentRoom.transform.parent = gameObject.transform;
                        room.roomObject = currentRoom;
                        currentRoom.GetComponent<DoorScript>().MakeDoors(room);
                    }
                    break;
                case 5:
                    {
                        GameObject currentRoom = Instantiate(testRoom, roomPosition, Quaternion.identity);
                        currentRoom.transform.parent = gameObject.transform;
                        room.roomObject = currentRoom;

                    }
                    break;
                case 10:
                    {
                        GameObject currentRoom = Instantiate(bossRoom, roomPosition, Quaternion.identity);
                        currentRoom.transform.parent = gameObject.transform;
                        room.roomObject = currentRoom;
                        currentRoom.GetComponent<DoorScript>().MakeDoors(room);
                    }
                    break;
            }
        }
    }

    //public void GeneratePlants()
    //{
    //    for (int x = 0; x < width; x++)
    //    {
    //        for (int y = 0; y < height; y++)
    //        {
    //            if (map[x, y] == 0 || map[x, y] == 3)
    //            {
    //                if (Random.Range(0, 100) < 6)
    //                {
    //                    GameObject plant = GameObject.Instantiate(testPlantPrefab, CoordToWorldPoint(new Coord(x, y)), Quaternion.identity);
    //                    plant.transform.parent = gameObject.transform;
    //                }
    //            }
    //        }
    //    }
    //}

    public void CreateSpawnPoints(Room spawnRoom)
    {
        if (spawnRoom.roomType != 10)
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (map[x, y] == 0 && !spawnRoom.tiles.Contains(new Coord(x, y)))
                    {
                        bool nextToAWall = false;
                        for (int i = x - 1; i <= x + 1; i++)
                        {
                            for (int j = y - 1; j <= y + 1; j++)
                            {
                                if (map[i, j] == 1)
                                    nextToAWall = true;
                            }
                        }
                        if (!nextToAWall)
                            if (Random.Range(0, 100) < spawnPointCreationPrecentagePerTile)
                            {
                                GameObject spawn = GameObject.Instantiate(spawnPoint, CoordToWorldPoint(new Coord(x, y)), Quaternion.identity);
                                spawn.transform.parent = gameObject.transform;
                            }
                    }
                }
            }
        foreach (MapNode mapNode in mapNodes)
        {
            if (mapNode != null)
                if (mapNode._owningRoom.roomObject.GetComponent<CombatRoomScript>() && mapNode._owningRoom.roomObject.GetComponent<CombatRoomScript>().spawnRandomSpawnPoints)
                {

                    bool nextToAWall = false;
                    for (int x = mapNode._nodeCoord.tileX - 1; x <= mapNode._nodeCoord.tileX + 1; x++)
                    {
                        for (int y = mapNode._nodeCoord.tileY - 1; y <= mapNode._nodeCoord.tileY + 1; y++)
                        {
                            if (map[x, y] == 1)
                                nextToAWall = true;
                        }
                    }
                    if (!nextToAWall)
                        if (Random.Range(0, 100) < combatRoomSpawnPointCreationPrecentagePerTile)
                        {
                            GameObject spawn = GameObject.Instantiate(combatRoomSpawner, CoordToWorldPoint(new Coord(mapNode._nodeCoord.tileX, mapNode._nodeCoord.tileY)), Quaternion.identity);
                            spawn.transform.parent = gameObject.transform;
                            mapNode._owningRoom.roomObject.GetComponent<CombatRoomScript>().spawnPoints.Add(spawn);
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

        levelsPassed++;
        if (levelsPassed == levels.Length)
            levelsPassed = 0;
        StartCoroutine(doneLoading());
        GenerateMap();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject[] playerSpawnPoint = GameObject.FindGameObjectsWithTag("PlayerSpawnPoint");
        player.transform.position = playerSpawnPoint[1].transform.position;
        //Debug.Log(playerSpawnPoint[1].transform.position);

    }
    IEnumerator doneLoading()
    {
        yield return new WaitForEndOfFrame();
        GameObject.FindGameObjectWithTag("UI").GetComponent<PauseMenu>().StopLoad();
    }

    [System.Serializable]
    public class Level
    {
        public int[] roomList;
    }

}
