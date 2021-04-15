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
        StartCoroutine(SpawnItems());
    }

    IEnumerator SpawnItems()
    {
        yield return new WaitForSeconds(0.1f);
        ItemOnGround groundItem = Instantiate(itemOnGround, gameObject.transform.position, Quaternion.identity);
        groundItem.SetItem(new Item(testItemObject2));
        groundItem = Instantiate(itemOnGround, gameObject.transform.position, Quaternion.identity);
        groundItem.SetItem(new Item(testItemObject));
        groundItem = Instantiate(itemOnGround, gameObject.transform.position, Quaternion.identity);
        groundItem.SetItem(new Item(testItemObject3));
        groundItem = Instantiate(itemOnGround, gameObject.transform.position, Quaternion.identity);
        groundItem.SetItem(new Item(testItemObject4));
        groundItem = Instantiate(itemOnGround, gameObject.transform.position, Quaternion.identity);
        groundItem.SetItem(new Item(testItemObject5));
        groundItem = Instantiate(itemOnGround, gameObject.transform.position, Quaternion.identity);
        groundItem.SetItem(new Item(testItemObject6));
        groundItem = Instantiate(itemOnGround, gameObject.transform.position, Quaternion.identity);
        groundItem.SetItem(new Item(testItemObject7));
        groundItem = Instantiate(itemOnGround, gameObject.transform.position, Quaternion.identity);
        groundItem.SetItem(new Item(testItemObject8));

        GetComponent<EntityEvents>().NewBuff("Start", EntityStats.BuffType.Burning, 1, 30);
    }
}
