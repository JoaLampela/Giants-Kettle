using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MeshGenerator : MonoBehaviour
{
    public SquareGrid squareGrid;
    private List<Vector3> vertices;
    private List<int> triangles;

    public void GenerateMesh(int[,] map, float squareSize)
    {
        squareGrid = new SquareGrid(map, squareSize);
        vertices = new List<Vector3>();
        triangles = new List<int>();


        for (int x = 0; x < squareGrid.squares.GetLength(0); x++) {
            for (int y = 0; y < squareGrid.squares.GetLength(1); y++) {
                TriangulateSquare(squareGrid.squares[x, y]);
            }
        }

        //create mesh
        Mesh mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        //vertices list has to be Vector3 so that the ToArray() method works
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        Debug.Log("MADE THE MESH");
        //just to be safe:
        mesh.RecalculateNormals();
    }



    //translate the square configuration to mesh points
    void TriangulateSquare(Square square)
    {
        switch(square._configuration) {
            case 0:
                break;
            //only 1 controlNodes is active
            case 1:
                MeshFromPoints(square._centerBottom, square._bottomLeft, square._centerLeft);
                break;
            case 2:
                MeshFromPoints(square._centerRight, square._bottomRight, square._centerBottom);
                break;
            case 4:
                MeshFromPoints(square._centerTop, square._topRight, square._centerRight);
                break;
            case 8:
                MeshFromPoints(square._topLeft, square._centerTop, square._centerLeft);
                break;

            //two nodes
            case 3:
                MeshFromPoints(square._centerRight, square._bottomRight, square._bottomLeft, square._centerLeft);
                break;
            case 6:
                MeshFromPoints(square._centerTop, square._topRight, square._bottomRight, square._centerBottom);
                break;
            case 9:
                MeshFromPoints(square._topLeft, square._centerTop, square._centerBottom, square._bottomLeft);
                break;
            case 12:
                MeshFromPoints(square._topLeft, square._topRight, square._centerRight, square._centerLeft);
                break;
            //diagonal two nodes
            case 5:
                MeshFromPoints(square._centerTop, square._topRight, square._centerRight, square._centerBottom, square._bottomLeft, square._centerLeft);
                break;
            case 10:
                MeshFromPoints(square._topLeft, square._centerTop, square._centerRight, square._bottomRight, square._centerBottom, square._centerLeft);
                break;

            //three nodes
            case 7:
                MeshFromPoints(square._centerTop, square._topRight, square._bottomRight, square._bottomLeft, square._centerLeft);
                break;
            case 11:
                MeshFromPoints(square._topLeft, square._centerTop, square._centerRight, square._bottomRight, square._bottomLeft);
                break;
            case 13:
                MeshFromPoints(square._topLeft, square._topRight, square._centerRight, square._centerBottom, square._bottomLeft);
                break;
            case 14:
                MeshFromPoints(square._topLeft, square._topRight, square._bottomRight, square._centerBottom, square._centerLeft);
                break;

            //four nodes
            case 15:
                MeshFromPoints(square._topLeft, square._topRight, square._bottomRight, square._bottomLeft);
                break;
        }
    }


    //params keyword makes the parameters given into a list of nodes
    void MeshFromPoints(params Node[] points)
    {
        AssignVerticies(points);

        //create a triangle if the points list has 3 veticies
        if (points.Length >= 3) {
            CreateTriangle(points[0], points[1], points[2]);
        }
        //two triangles ()
        if (points.Length >= 4) {
            CreateTriangle(points[0], points[2], points[3]);
        }
        //three triangles
        if (points.Length >= 5) {
            CreateTriangle(points[0], points[3], points[4]);
        }
        //four triangles, (two control nodes diagonally)
        if (points.Length >= 6) {
            CreateTriangle(points[0], points[4], points[5]);
        }
    }

    private void AssignVerticies(Node[] points)
    {
        for (int i = 0; i < points.Length; i++) {
            if (points[i]._vertexIndex == -1) {
                //set an index for a point using the amount of vertecies
                points[i]._vertexIndex = vertices.Count;
                //this is different than in the tutorial? added the _position for it to work
                vertices.Add((Vector3)points[i]._position);
            }
        }
    }

    void CreateTriangle(Node cornerA, Node cornerB, Node cornerC)
    {
        triangles.Add(cornerA._vertexIndex);
        triangles.Add(cornerB._vertexIndex);
        triangles.Add(cornerC._vertexIndex);
    }

    void OnDrawGizmos()
    {
        /*
        if (squareGrid != null) {
            for (int x = 0; x < squareGrid.squares.GetLength(0); x++) {
                for (int y = 0; y < squareGrid.squares.GetLength(1); y++) {
                  
                    Gizmos.color = squareGrid.squares[x, y]._topLeft._active ? Color.black : Color.white;
                    Gizmos.DrawCube(squareGrid.squares[x, y]._topLeft._position, Vector2.one * 0.4f);

                    Gizmos.color = squareGrid.squares[x, y]._topRight._active ? Color.black : Color.white;
                    Gizmos.DrawCube(squareGrid.squares[x, y]._topRight._position, Vector2.one * 0.4f);

                    Gizmos.color = squareGrid.squares[x, y]._bottomLeft._active ? Color.black: Color.white;
                    Gizmos.DrawCube(squareGrid.squares[x, y]._bottomLeft._position, Vector2.one * 0.4f);

                    Gizmos.color = squareGrid.squares[x, y]._bottomRight._active ? Color.black : Color.white;
                    Gizmos.DrawCube(squareGrid.squares[x, y]._bottomRight._position, Vector2.one * 0.4f);

                    //nodes
                    Gizmos.color = Color.grey;
                    Gizmos.DrawCube(squareGrid.squares[x, y]._centerTop._position, Vector2.one * 0.15f);
                    Gizmos.DrawCube(squareGrid.squares[x, y]._centerRight._position, Vector2.one * 0.15f);
                    Gizmos.DrawCube(squareGrid.squares[x, y]._centerBottom._position, Vector2.one * 0.15f);
                    Gizmos.DrawCube(squareGrid.squares[x, y]._centerLeft._position, Vector2.one * 0.15f);
                }
            }
        }
        */
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
            for (int x = 0; x < nodeCountX; x++) {
                for (int y = 0; y < nodeCountY; y++) {
                    //count the new square position
                    Vector2 position = new Vector2(-mapWidth / 2 + x * squareSize + squareSize / 2, -mapHeight / 2 + y * squareSize + squareSize / 2);
                    controlNodes[x, y] = new ControlNode(position, map[x, y] != 0, squareSize);
                }
            }

            squares = new Square[nodeCountX - 1, nodeCountY - 1];
            
            //fill the squares array
            for (int x = 0; x < nodeCountX - 1; x++) {
                for (int y = 0; y < nodeCountY - 1; y++) {
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

            if (_topLeft._active) {
                _configuration += 8;
            }
            if (_topRight._active) {
                _configuration += 4;
            }
            if (_bottomRight._active) {
                _configuration += 2;
            }
            if (_bottomLeft._active) {
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
