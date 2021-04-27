using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapNode
{

    public Room _owningRoom = null;
    public Coord _nodeCoord = new Coord();

    public MapNode() { }
    public MapNode(Coord nodeCoord, Room owningRoom)
    {
        _owningRoom = owningRoom;
        _nodeCoord = nodeCoord;
    }
    public void LocationAction()
    {
        _owningRoom.RoomAction(_nodeCoord);
    }
}
