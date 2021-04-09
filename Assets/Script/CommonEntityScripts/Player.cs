using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public ItemObject testItemObject;
    public ItemOnGround itemOnGround;
    public ItemObject testItemObject2;
    public ItemObject testItemObject3;
    public ItemObject testItemObject4;
    public ItemObject testItemObject5;
    public ItemObject testItemObject6;
    public ItemObject testItemObject7;
    public ItemObject testItemObject8;


    private void Start()
    {
        Debug.Log("VITTU");
        ItemOnGround groundItem = Instantiate(itemOnGround, transform.position, Quaternion.identity);
        groundItem.SetItem(new Item(testItemObject2));
        groundItem = Instantiate(itemOnGround, transform.position, Quaternion.identity);
        groundItem.SetItem(new Item(testItemObject));
        groundItem = Instantiate(itemOnGround, transform.position, Quaternion.identity);
        groundItem.SetItem(new Item(testItemObject3));
        groundItem = Instantiate(itemOnGround, transform.position, Quaternion.identity);
        groundItem.SetItem(new Item(testItemObject4));
        groundItem = Instantiate(itemOnGround, transform.position, Quaternion.identity);
        groundItem.SetItem(new Item(testItemObject5));
        groundItem = Instantiate(itemOnGround, transform.position, Quaternion.identity);
        groundItem.SetItem(new Item(testItemObject6));
        groundItem = Instantiate(itemOnGround, transform.position, Quaternion.identity);
        groundItem.SetItem(new Item(testItemObject7));
        groundItem = Instantiate(itemOnGround, transform.position, Quaternion.identity);
        groundItem.SetItem(new Item(testItemObject8));
    }
}
