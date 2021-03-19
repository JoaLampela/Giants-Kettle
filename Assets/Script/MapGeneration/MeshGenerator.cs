using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGenerator : MonoBehaviour
{
    public SquareGrid squareGrid;

    public void GenerateMesh(int[,] map, float squareSize)
    {
        squareGrid = new SquareGrid(map, squareSize);
    }
        
    void OnDrawGizmos()
    {
        //Debug.Log("DRAW GIZMOSSS");
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
    }

    public class SquareGrid 
    {
        public Square[,] squares; 

        public SquareGrid(int[,] map, float squareSize)
        {
            int nodeCountX = map.GetLength(0);
            int nodeCountY = map.GetLength(1);
            Debug.Log(nodeCountX + " " + nodeCountY);

            float mapWidth = nodeCountX * squareSize;
            float mapHeight = nodeCountY * squareSize;

            ControlNode[,] controlNodes = new ControlNode[nodeCountX, nodeCountY];

            for (int x = 0; x < nodeCountX; x++) {
                for (int y = 0; y < nodeCountY; y++) {
                    Vector2 position = new Vector2(-mapWidth / 2 + x * squareSize + squareSize / 2, -mapHeight / 2 + y * squareSize + squareSize / 2);
                    controlNodes[x, y] = new ControlNode(position, map[x, y] != 0, squareSize);
                }
            }

            squares = new Square[nodeCountX - 1, nodeCountY - 1];

            
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
