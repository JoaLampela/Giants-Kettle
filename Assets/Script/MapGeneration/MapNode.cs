using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapNode
{

    public Room _owningRoom;
    public Coord _nodeCoord;

    public MapNode() { }
    public MapNode(Coord nodeCoord, Room owningRoom)
    {
        _owningRoom = owningRoom;
        _nodeCoord = nodeCoord;
    }
    public void LocationAction()
    {
        _owningRoom.RoomAction();
    }
}
