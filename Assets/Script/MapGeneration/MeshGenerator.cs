using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;
using Pathfinding;

enum Tiles{
    ground, bottomLeft, bottomRight, bottomCave, topRight, leftToTopRight,
    rightWall, missingTopLeft, topLeft, leftCave, rightToTopLeft, missingTopRight,
    topCave, missingBottomright, missingBottomLeft, fullCave
};


public class NamedArrayAttribute : PropertyAttribute
{
    public readonly string[] names;
    public NamedArrayAttribute(string[] names) { this.names = names; }
}


public class MeshGenerator : MonoBehaviour
{
    public bool wallsOn = false;
    public bool caveOn = false;

    public SquareGrid squareGrid;
    public MeshFilter wallMeshFilter;
    public GameObject wallTilemapGO;
    public GameObject caveTilemapGO;
    public GameObject grassPrefab;
    public Tilemap groundTilemap;
    public Tilemap caveTilemap;
    public Tilemap wallTilemap;
    public Tilemap wallBorderTilemap;
    public GridLayout grid;
    public GameObject brushTargetGO;
    public Transform brushTargetTrans;
    public List<Tile> tileList;

    public UnityEditor.Tilemaps.PrefabBrush grassBrush;

    public Transform playerTransform;
    private List<Vector3> vertices;
    private List<int> triangles;
    //these is private but are they really?
    private Dictionary<int, List<Triangle>> triangleDictionary;
    private List<List<int>> outlines;
    private HashSet<int> checkedVertices;

    private void Awake()
    {
        triangleDictionary = new Dictionary<int, List<Triangle>>();
        outlines = new List<List<int>>();
        checkedVertices = new HashSet<int>();

    }
    ////this update is purely for debug services, delete later?
    //private void Update()
    //{
    //    MeshRenderer caveMF = GetComponent<MeshRenderer>();
    //    caveMF.enabled = caveOn;
    //    MeshRenderer wallsMF = transform.GetChild(0).GetComponent<MeshRenderer>();
    //    wallsMF.enabled = wallsOn;
    //}

    public void GenerateMesh(int[,] map, float squareSize)
    {
        squareGrid = new SquareGrid(map, squareSize);
        vertices = new List<Vector3>();
        triangles = new List<int>();

        outlines.Clear();
        checkedVertices.Clear();
        triangleDictionary.Clear();


        for (int x = 0; x < squareGrid.squares.GetLength(0); x++)
        {
            for (int y = 0; y < squareGrid.squares.GetLength(1); y++)
            {
                TriangulateSquare(squareGrid.squares[x, y]);
            }
        }

        /*rip mesh creation code :'(
        //create mesh
        Mesh mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        //vertices list has to be Vector3 so that the ToArray() method works
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        Debug.Log("MADE THE MESH");
        //just to be safe:
        mesh.RecalculateNormals();
        CreateWallMesh();
        */

        CreateTiles();
        StartCoroutine("updateAStar");
    }

    IEnumerator updateAStar()
    {
        yield return new WaitForEndOfFrame();
        AstarPath.active.Scan();
    }

    void InstantiatePrefab(GridLayout grid, GameObject targetGO, Vector3Int position, UnityEditor.Tilemaps.PrefabBrush prefabBrush)
    {
        // prefabBrush.Paint(grid, targetGO, prefab, position);
        prefabBrush.Paint(grid, targetGO, position);
        prefabBrush.Paint(grid, targetGO, position);
    }

    public void DestroyChildren(Transform trans)
    {
      
        foreach (Transform child in trans.transform) {
            Destroy(child.gameObject);
        }
    }

    void CreateTiles()
    {
        Vector3Int wallVector;
        System.Random rnd = new System.Random();
        int randInt;

        caveTilemap.ClearAllTiles();
        wallTilemap.ClearAllTiles();
        wallBorderTilemap.ClearAllTiles();
        DestroyChildren(brushTargetTrans);

        
        //set the tiles to the map
        for (int x = 0; x < squareGrid.squares.GetLength(0); x++)
        {
            rnd = new System.Random();
            for (int y = 0; y < squareGrid.squares.GetLength(1); y++) {
                int configuration = squareGrid.squares[x, y]._configuration;

                Vector3Int cellVector = caveTilemap.WorldToCell(squareGrid.squares[x, y]._centerBottom._position + new Vector2(0, 0.5f));
                switch (configuration) {
                    case 0:
                        //ground
                        
                        randInt = rnd.Next(24, 27);
                       
                        groundTilemap.SetTile(cellVector, tileList[0]);

                        randInt = rnd.Next(1, 100);

                        if (randInt <= 10) {
                            InstantiatePrefab(grid, brushTargetGO, cellVector, grassBrush);
                        }

                        break;
                    //only 1 controlNodes is active
                    case 1:
                        //bottomleft triangle
                        caveTilemap.SetTile(cellVector, tileList[configuration]);

                        //set the ground underneath this tile
                        randInt = rnd.Next(24, 27);
                        groundTilemap.SetTile(cellVector, tileList[0]);
                        break;
                    case 2:
                        //bottom right triangle
                        caveTilemap.SetTile(cellVector, tileList[configuration]);

                        //set the ground underneath this tile
                        randInt = rnd.Next(24, 27);
                        groundTilemap.SetTile(cellVector, tileList[0]);
                        break;
                    case 4:
                        //top right triangle
                        caveTilemap.SetTile(cellVector, tileList[configuration]);
                        //wallVector = caveTilemap.WorldToCell(squareGrid.squares[x, y - 1]._centerBottom._position + new Vector2(0, 0.5f));
                        //bottom left wall square
                        wallTilemap.SetTile(cellVector, tileList[18]);

                        wallVector = caveTilemap.WorldToCell(squareGrid.squares[x, y - 1]._centerBottom._position + new Vector2(0, 0.5f));
                        //if the block to the right is a upside wall
                        if (squareGrid.squares[x + 1, y]._configuration == 12) {
                            //top right wall triangle
                            wallTilemap.SetTile(wallVector, tileList[22]);
                        } else if (squareGrid.squares[x + 1, y]._configuration == 8) {
                            //if the block to the right is top left triangle
                            wallTilemap.SetTile(wallVector, tileList[22]);
                        }

                        //if the block to the right is a top left triangle??

                        break;
                    case 8:
                        //top left triangle
                        caveTilemap.SetTile(cellVector, tileList[configuration]);
                        //bottom right wall triangle
                        wallTilemap.SetTile(cellVector, tileList[19]);

                        wallVector = caveTilemap.WorldToCell(squareGrid.squares[x, y - 1]._centerBottom._position + new Vector2(0, 0.5f));

                        //if the block to the left is a upside wall
                        if (squareGrid.squares[x - 1, y]._configuration == 12) {
                            //top left wall square
                            wallTilemap.SetTile(wallVector, tileList[21]);
                        } else if (squareGrid.squares[x - 1, y]._configuration == 4) {
                            //if the block to the left is a top right triangle
                            wallTilemap.SetTile(wallVector, tileList[21]);
                        }
                        break;

                    //two nodes
                    case 3:
                        //bottom wall
                        caveTilemap.SetTile(cellVector, tileList[configuration]);

                        break;
                    case 6:
                        //right side wall
                        caveTilemap.SetTile(cellVector, tileList[configuration]);
                        break;
                    case 9:
                        //left side wall
                        caveTilemap.SetTile(cellVector, tileList[configuration]);
                        break;
                    case 12:
                        //top wall
                        caveTilemap.SetTile(cellVector, tileList[configuration]);
                        //full wall square
                        wallVector = caveTilemap.WorldToCell(squareGrid.squares[x, y - 1]._centerBottom._position + new Vector2(0, 0.5f));

                        randInt = rnd.Next(28, 30);
                        //set random wall tile from three options
                        wallTilemap.SetTile(wallVector, tileList[20]);
                        break;

                    //diagonal two nodes
                    case 5:
                        //diagonal left to right top
                        wallBorderTilemap.SetTile(cellVector, tileList[configuration]);
                        //set the borders for this tile
                        caveTilemap.SetTile(cellVector, tileList[23]);

                        //if the tile to the upper left is the same as this, or if it is a bottom left triangle, or if tile to the bottom left is a top right triangle
                        if ((squareGrid.squares[x + 1, y + 1]._configuration == 1) || (squareGrid.squares[x + 1, y + 1]._configuration == 5) || (squareGrid.squares[x + 1, y + 1]._configuration == 4))
                        {
                            //if the tile to the right is not a top wall
                            if (!(squareGrid.squares[x + 1, y]._configuration == 12)) {
                                wallVector = caveTilemap.WorldToCell(squareGrid.squares[x + 1, y - 1]._centerBottom._position + new Vector2(0, 0.5f));
                                wallTilemap.SetTile(wallVector, tileList[21]);
                            }
                        }
                        ////if tile to the bottom left is a top right triangle
                        //if (squareGrid.squares[x + 1, y + 1]._configuration == 4)
                        //{
                        //    wallVector = caveTilemap.WorldToCell(squareGrid.squares[x + 1, y - 1]._centerBottom._position + new Vector2(0, 0.5f));
                        //    wallTilemap.SetTile(wallVector, tileList[21]);
                        //}

                        break;
                    case 10:
                        //diagonal right to left top
                        wallBorderTilemap.SetTile(cellVector, tileList[configuration]);
                        //set the borders for this tile
                        caveTilemap.SetTile(cellVector, tileList[23]);

                        //if the tile to the upper left is the same as this, or if it is a bottom right triangle, or if the tile to the bottom right is a top left triangle
                        if ((squareGrid.squares[x - 1, y + 1]._configuration == 2) || (squareGrid.squares[x - 1, y + 1]._configuration == 10) || (squareGrid.squares[x + 1, y - 1]._configuration == 8)) {
                            //if the tile to the left is not a top wall
                            if (!(squareGrid.squares[x - 1, y]._configuration == 12)) {
                                wallVector = caveTilemap.WorldToCell(squareGrid.squares[x - 1, y - 1]._centerBottom._position + new Vector2(0, 0.5f));
                                wallTilemap.SetTile(wallVector, tileList[22]);
                            }
                        }

                        ////if the tile to the bottom right is a top left triangle
                        //if (squareGrid.squares[x + 1, y - 1]._configuration == 8) {
                        //    wallVector = caveTilemap.WorldToCell(squareGrid.squares[x - 1, y - 1]._centerBottom._position + new Vector2(0, 0.5f));
                        //    wallTilemap.SetTile(wallVector, tileList[22]);
                        //}

                        break;

                    //three nodes
                    case 7:
                        //missing top left square
                        //add border top left triangle
                        wallBorderTilemap.SetTile(cellVector, tileList[configuration]);
                        //full cave tile
                        caveTilemap.SetTile(cellVector, tileList[23]);
                        break;
                    case 11:
                        //missing top right square
                        //border top right triangle
                        wallBorderTilemap.SetTile(cellVector, tileList[configuration]);
                        //full cave tile
                        caveTilemap.SetTile(cellVector, tileList[23]);

                        break;
                    case 13:
                        //missing bottom right square
                        //border bottom right triangle
                        wallBorderTilemap.SetTile(cellVector, tileList[configuration]);
                        //full cave tile
                        caveTilemap.SetTile(cellVector, tileList[23]);

                        wallVector = caveTilemap.WorldToCell(squareGrid.squares[x + 1, y - 1]._centerBottom._position + new Vector2(0, 0.5f));

                        //if the block to the right is a upside wall
                        if (squareGrid.squares[x + 1, y]._configuration == 12)
                        {
                            //if this block is in a 90 degree corner, do not add the wall tile square
                            if (!(squareGrid.squares[x, y - 1]._configuration == 6))
                            {
                                //full wall square
                                Debug.Log("configuration is 12!!");
                                wallTilemap.SetTile(wallVector, tileList[20]);
                            }

                        }
                        else if (squareGrid.squares[x + 1, y]._configuration == 14)
                        {
                            //if block to the right is missing a bottom left square do nothing,
                            //because the triangle under this already sets th right wall
                        }
                        else
                        {
                            //top left wall square
                            wallTilemap.SetTile(wallVector, tileList[21]);
                        }

                        break;
                    case 14:
                        //missing bottom left square
                        //border bottom right triangle
                        wallBorderTilemap.SetTile(cellVector, tileList[configuration]);
                        //full cave tile
                        caveTilemap.SetTile(cellVector, tileList[23]);

                        wallVector = caveTilemap.WorldToCell(squareGrid.squares[x - 1, y - 1]._centerBottom._position + new Vector2(0, 0.5f));

                        //if the block to the left is a upside wall
                        if (squareGrid.squares[x - 1, y]._configuration == 12)
                        {
                            //see if the wall below is not right side wall
                            if (!(squareGrid.squares[x, y - 1]._configuration == 6))
                            {
                                //wallVector = caveTilemap.WorldToCell(squareGrid.squares[x, y - 1]._centerBottom._position + new Vector2(0, 0.5f));
                                //full wall square
                                wallTilemap.SetTile(wallVector, tileList[20]);
                            }

                        }
                        else if (squareGrid.squares[x - 1, y]._configuration == 13)
                        {
                            //if the block to the left is missing bottom right square, do nothing
                            //because the triangle under this already sets the right tile
                        }
                        else
                        {
                            //top right wall square
                            wallTilemap.SetTile(wallVector, tileList[22]);
                        }
                        break;
                    //four nodes
                    case 15:
                        //full square
                        caveTilemap.SetTile(cellVector, tileList[configuration]);
                        break;
                }

            }

        }
        /*
        Vector3Int cellVector = caveTilemap.WorldToCell(squareGrid.squares[30, 30]._topLeft._position);
         caveTilemap.SetTile(cellVector, testTile);
        int configuration = squareGrid.squares[1, 2]._configuration;
       
        */

        wallTilemapGO.AddComponent<TilemapCollider2D>();
        caveTilemapGO.AddComponent<TilemapCollider2D>();
    }


    void CreateWallMesh()
    {
        CalculateMeshOutlines();

        List<Vector3> wallVertices = new List<Vector3>();
        List<int> wallTriangles = new List<int>();
        Mesh wallMesh = new Mesh();
        float wallHeight = 5;

        foreach (List<int> outline in outlines)
        {
            for (int i = 0; i < outline.Count - 1; i++)
            {
                int startIndex = wallVertices.Count;
                wallVertices.Add(vertices[outline[i]]); //left
                wallVertices.Add(vertices[outline[i + 1]]); //right
                wallVertices.Add(vertices[outline[i]] + Vector3.forward * wallHeight); //bottom left
                wallVertices.Add(vertices[outline[i + 1]] + Vector3.forward * wallHeight); //bottom right

                //add a first triangle to the list of wall vertices
                wallTriangles.Add(startIndex);
                wallTriangles.Add(startIndex + 2);
                wallTriangles.Add(startIndex + 3);

                //the second triangle of the square
                wallTriangles.Add(startIndex + 3);
                wallTriangles.Add(startIndex + 1);
                wallTriangles.Add(startIndex);
            }
        }
        //wallMeshFilter = GameObject.Find("Walls(Clone)").GetComponent<MeshFilter>();

        wallMesh.vertices = wallVertices.ToArray();
        wallMesh.triangles = wallTriangles.ToArray();


        wallMeshFilter.mesh = wallMesh;
        GetComponent<MeshCollider>().sharedMesh = wallMesh;
    }

    //translate the square configuration to mesh points
    void TriangulateSquare(Square square)
    {
        switch (square._configuration)
        {
            case 0:
                break;
            //only 1 controlNodes is active
            case 1:
                //bottomleft square
                MeshFromPoints(square._centerLeft, square._centerBottom, square._bottomLeft);
                break;
            case 2:
                //bottom right square
                MeshFromPoints(square._bottomRight, square._centerBottom, square._centerRight);
                break;
            case 4:
                //top right squre
                MeshFromPoints(square._topRight, square._centerRight, square._centerTop);
                break;
            case 8:
                //top left square
                MeshFromPoints(square._topLeft, square._centerTop, square._centerLeft);
                break;

            //two nodes
            case 3:
                //downside wall
                MeshFromPoints(square._centerRight, square._bottomRight, square._bottomLeft, square._centerLeft);
                break;
            case 6:
                //right side wall
                MeshFromPoints(square._centerTop, square._topRight, square._bottomRight, square._centerBottom);
                break;
            case 9:
                //left side wall
                MeshFromPoints(square._topLeft, square._centerTop, square._centerBottom, square._bottomLeft);
                break;
            case 12:
                //upside wall
                MeshFromPoints(square._topLeft, square._topRight, square._centerRight, square._centerLeft);
                break;
            //diagonal two nodes
            case 5:
                //diagonal left ro right
                MeshFromPoints(square._centerTop, square._topRight, square._centerRight, square._centerBottom, square._bottomLeft, square._centerLeft);
                break;
            case 10:
                //diagonal right to left
                MeshFromPoints(square._topLeft, square._centerTop, square._centerRight, square._bottomRight, square._centerBottom, square._centerLeft);
                break;

            //three nodes
            case 7:
                //missing top left square
                MeshFromPoints(square._centerTop, square._topRight, square._bottomRight, square._bottomLeft, square._centerLeft);
                break;
            case 11:
                //missing top right square
                MeshFromPoints(square._topLeft, square._centerTop, square._centerRight, square._bottomRight, square._bottomLeft);
                break;
            case 13:
                //missing bottom right square
                MeshFromPoints(square._topLeft, square._topRight, square._centerRight, square._centerBottom, square._bottomLeft);
                break;
            case 14:
                //missing bottom left square
                MeshFromPoints(square._topLeft, square._topRight, square._bottomRight, square._centerBottom, square._centerLeft);
                break;

            //four nodes
            case 15:
                //full
                MeshFromPoints(square._topLeft, square._topRight, square._bottomRight, square._bottomLeft);
                /*
                 *  because this case means that none of the triangles adjacent are
                 *  outline vertices, we can already add these vertices to the checked vertices list
                 */
                checkedVertices.Add(square._topLeft._vertexIndex);
                checkedVertices.Add(square._topRight._vertexIndex);
                checkedVertices.Add(square._bottomRight._vertexIndex);
                checkedVertices.Add(square._bottomLeft._vertexIndex);
                break;
        }
    }


    //params keyword makes the parameters given into a list of nodes
    void MeshFromPoints(params Node[] points)
    {
        AssignVerticies(points);

        //create a triangle if the points list has 3 veticies
        if (points.Length >= 3)
        {
            CreateTriangle(points[0], points[1], points[2]);
        }
        //two triangles
        if (points.Length >= 4)
        {
            CreateTriangle(points[0], points[2], points[3]);
        }
        //three triangles
        if (points.Length >= 5)
        {
            CreateTriangle(points[0], points[3], points[4]);
        }
        //four triangles, (two control nodes diagonally)
        if (points.Length >= 6)
        {
            CreateTriangle(points[0], points[4], points[5]);
        }
    }

    private void AssignVerticies(Node[] points)
    {
        for (int i = 0; i < points.Length; i++)
        {
            if (points[i]._vertexIndex == -1)
            {
                //set an index for a point using the amount of vertecies
                points[i]._vertexIndex = vertices.Count;

                vertices.Add((Vector3)points[i]._position);
            }
        }
    }

    void CreateTriangle(Node cornerA, Node cornerB, Node cornerC)
    {
        triangles.Add(cornerA._vertexIndex);
        triangles.Add(cornerB._vertexIndex);
        triangles.Add(cornerC._vertexIndex);

        Triangle triangle = new Triangle(cornerA._vertexIndex, cornerB._vertexIndex, cornerC._vertexIndex);
        /*
         * add the triangle to the dictionary by it's vertex index, so that we can later compare 
         * if there is more triangles that share the same edge
         */
        AddTriangleToDictionary(triangle.vertexIndexA, triangle);
        AddTriangleToDictionary(triangle.vertexIndexB, triangle);
        AddTriangleToDictionary(triangle.vertexIndexC, triangle);

    }

    void AddTriangleToDictionary(int vertexIndexKey, Triangle triangle)
    {
        /*
         * if the triangle vertex is already in the dictionary, we use a list
         * to store the new triangle that has the same vertex index
         */
        if (triangleDictionary.ContainsKey(vertexIndexKey))
        {
            triangleDictionary[vertexIndexKey].Add(triangle);
        }
        else
        {
            /*
             * if vertex is new in the dictionary, make a new list 
             * for the vertex index and add it to dictionary
             */
            List<Triangle> triangleList = new List<Triangle>();
            triangleList.Add(triangle);
            triangleDictionary.Add(vertexIndexKey, triangleList);
        }
    }

    /*
     * goes through every vertex in the map and checks if it's an outline,
     * if it is, then it runs along the outline until it meets up with itself again
     * and adds it to the outlines list
     */
    void CalculateMeshOutlines()
    {
        for (int vertexIndex = 0; vertexIndex < vertices.Count; vertexIndex++)
        {
            if (!checkedVertices.Contains(vertexIndex))
            {
                int newOutlineVertex = GetConnectedOutlineVertex(vertexIndex);
                if (newOutlineVertex != -1)
                {
                    checkedVertices.Add(vertexIndex);
                    List<int> newOutline = new List<int>();

                    newOutline.Add(vertexIndex);
                    outlines.Add(newOutline);
                    FollowOutline(newOutlineVertex, outlines.Count - 1);
                    outlines[outlines.Count - 1].Add(vertexIndex);
                }
            }
        }
    }

    void FollowOutline(int vertexIndex, int outlineIndex)
    {
        outlines[outlineIndex].Add(vertexIndex);
        checkedVertices.Add(vertexIndex);

        int nextVertexIndex = GetConnectedOutlineVertex(vertexIndex);

        if (nextVertexIndex != -1)
        {
            FollowOutline(nextVertexIndex, outlineIndex);
        }
    }

    //find the vertex that connects an outline edge, if there is none, return -1
    int GetConnectedOutlineVertex(int vertexIndex)
    {
        List<Triangle> trianglesContainingVertex = triangleDictionary[vertexIndex];

        for (int i = 0; i < trianglesContainingVertex.Count; i++)
        {
            Triangle triangle = trianglesContainingVertex[i];

            for (int j = 0; j < 3; j++)
            {
                int vertexB = triangle[j];

                if (vertexIndex != vertexB && !checkedVertices.Contains(vertexB))
                {
                    if (IsOutlineEdge(vertexIndex, vertexB))
                    {
                        return vertexB;
                    }
                }
            }
        }

        return -1;
    }

    /*
    * go through the list that contains the triangles containing vertex A. if the list contains
    * vertex B more than one time, then the line connecting the two vertices is not an outline
    */
    bool IsOutlineEdge(int vertexA, int vertexB)
    {
        List<Triangle> trianglesContainingVertexA = triangleDictionary[vertexA];
        int sharedTriangleCount = 0;
        bool isOutline = true;


        for (int i = 0; i < trianglesContainingVertexA.Count; i++)
        {
            if (trianglesContainingVertexA[i].Contains(vertexB))
            {
                sharedTriangleCount++;
                if (sharedTriangleCount > 1)
                {
                    isOutline = false;
                    break;
                }
            }
        }

        return isOutline;
    }

    struct Triangle
    {
        public int vertexIndexA;
        public int vertexIndexB;
        public int vertexIndexC;
        int[] vertices;

        public Triangle(int a, int b, int c)
        {
            vertexIndexA = a;
            vertexIndexB = b;
            vertexIndexC = c;

            vertices = new int[3];
            vertices[0] = vertexIndexA;
            vertices[1] = vertexIndexB;
            vertices[2] = vertexIndexC;

        }

        public bool Contains(int vertedIndex)
        {
            return vertedIndex == vertexIndexA || vertedIndex == vertexIndexB || vertedIndex == vertexIndexC;
        }

        //this is like overloading [] operator in c++
        public int this[int i]
        {
            get
            {
                return vertices[i];
            }
        }
    }

    //the grid of squares, which copies the values from the map
    public class SquareGrid
    {
        public Square[,] squares;

        public SquareGrid(int[,] map, float squareSize)
        {
            int nodeCountX = map.GetLength(0);
            int nodeCountY = map.GetLength(1);
            float mapWidth = nodeCountX * squareSize;
            float mapHeight = nodeCountY * squareSize;

            ControlNode[,] controlNodes = new ControlNode[nodeCountX, nodeCountY];

            //fill the controlnodes array
            for (int x = 0; x < nodeCountX; x++)
            {
                for (int y = 0; y < nodeCountY; y++)
                {
                    //count the new square position
                    Vector2 position = new Vector2(-mapWidth / 2 + x * squareSize + squareSize / 2, -mapHeight / 2 + y * squareSize + squareSize / 2);
                    controlNodes[x, y] = new ControlNode(position, map[x, y] == 1, squareSize);
                }
            }

            squares = new Square[nodeCountX - 1, nodeCountY - 1];

            //fill the squares array with control nodes
            for (int x = 0; x < nodeCountX - 1; x++)
            {
                for (int y = 0; y < nodeCountY - 1; y++)
                {
                    squares[x, y] = new Square(controlNodes[x, y + 1], controlNodes[x + 1, y + 1], controlNodes[x + 1, y], controlNodes[x, y]);
                }
            }
        }
    }


    public class Square
    {
        public ControlNode _topLeft;
        public ControlNode _topRight;
        public ControlNode _bottomLeft;
        public ControlNode _bottomRight;
        public Node _centerTop;
        public Node _centerRight;
        public Node _centerLeft;
        public Node _centerBottom;
        public int _configuration;

        public Square(ControlNode topLeft, ControlNode topRight, ControlNode bottomRight, ControlNode bottomLeft)
        {
            _topLeft = topLeft;
            _topRight = topRight;
            _bottomLeft = bottomLeft;
            _bottomRight = bottomRight;

            _centerTop = _topLeft.rightNode;
            _centerRight = _bottomRight.aboveNode;
            _centerBottom = _bottomLeft.rightNode;
            _centerLeft = _bottomLeft.aboveNode;
            _configuration = 0;

            if (_topLeft._active)
            {
                _configuration += 8;
            }
            if (_topRight._active)
            {
                _configuration += 4;
            }
            if (_bottomRight._active)
            {
                _configuration += 2;
            }
            if (_bottomLeft._active)
            {
                _configuration += 1;
            }
        }
    }

    //the node on the sides of the "cube"
    public class Node
    {
        public Vector2 _position;
        public int _vertexIndex = -1;

        public Node(Vector2 pos)
        {
            _position = pos;
        }
    }

    //The node on the corner of a cube
    public class ControlNode : Node
    {
        public bool _active;
        public Node aboveNode;
        public Node rightNode;

        //base means that the position will be assigned through the base constructor
        public ControlNode(Vector2 pos, bool active, float squareSize) : base(pos)
        {
            _active = active;
            //declare the nodes which the controlnode owns
            aboveNode = new Node(_position + Vector2.up * squareSize / 2f);
            rightNode = new Node(_position + Vector2.right * squareSize / 2f);
        }
    }

}
