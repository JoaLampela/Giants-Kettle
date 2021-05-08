using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    private List<GameObject> DoorList;
    public GameObject doorPrefab;
    public void MakeDoors(Room room)
    {
        DoorList = new List<GameObject>();
        foreach (Coord doorCoord in room.hallWayTiles)
        {
            GameObject door = Instantiate(doorPrefab, GameObject.FindGameObjectWithTag("MapGenerator").GetComponent<MapGeneration>().CoordToWorldPoint(doorCoord), Quaternion.identity);
            DoorList.Add(door);
            door.transform.parent = gameObject.transform;
            door.SetActive(false);
        }
    }
    public void CloseDoors()
    {
        foreach (GameObject door in DoorList)
        {
            door.SetActive(true);
            SoundManager.PlayUISound(SoundManager.Sound.RoomEnter);
        }
    }
    public void OpenDoors()
    {
        foreach (GameObject door in DoorList)
        {
            door.SetActive(false);
        }
    }
}
