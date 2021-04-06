using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public ItemObject testItemObject;
    public ItemOnGround itemOnGround;
    public ItemObject testItemObject2;


    private void Update()
    {
        if(Input.GetMouseButtonDown(0)) 
        {
            ItemOnGround groundItem = Instantiate(itemOnGround, transform.position, Quaternion.identity);
            groundItem.SetItem(new Item(testItemObject2));
        }
        if (Input.GetMouseButtonDown(1))
        {
            ItemOnGround groundItem = Instantiate(itemOnGround, transform.position, Quaternion.identity);
            groundItem.SetItem(new Item(testItemObject));
        }
    }
}
