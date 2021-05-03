using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartItemSelectScript : MonoBehaviour
{
    public ItemOnGround itemOnGround;
    public ItemObject sword;
    public ItemObject shield;
    public ItemObject twoHandedSword;
    public ItemObject staff;

    public void SelectSwordSword()
    {
        ItemOnGround groundItem = Instantiate(itemOnGround, GameObject.FindGameObjectWithTag("Player").transform.position , Quaternion.identity);
        groundItem.SetItem(new Item(sword));
        groundItem.pickedUp = true;

        groundItem = Instantiate(itemOnGround, GameObject.FindGameObjectWithTag("Player").transform.position, Quaternion.identity);
        groundItem.SetItem(new Item(sword));
        groundItem.pickedUp = true;

        gameObject.active = false;
    }

    public void SelectShieldShield()
    {
        ItemOnGround groundItem = Instantiate(itemOnGround, GameObject.FindGameObjectWithTag("Player").transform.position, Quaternion.identity);
        groundItem.SetItem(new Item(shield));
        groundItem.pickedUp = true;

        groundItem = Instantiate(itemOnGround, GameObject.FindGameObjectWithTag("Player").transform.position, Quaternion.identity);
        groundItem.SetItem(new Item(shield));
        groundItem.pickedUp = true;

        gameObject.active = false;
    }

    public void SelectShieldSword()
    {
        ItemOnGround groundItem = Instantiate(itemOnGround, GameObject.FindGameObjectWithTag("Player").transform.position, Quaternion.identity);
        groundItem.SetItem(new Item(shield));
        groundItem.pickedUp = true;

        groundItem = Instantiate(itemOnGround, GameObject.FindGameObjectWithTag("Player").transform.position, Quaternion.identity);
        groundItem.SetItem(new Item(sword));
        groundItem.pickedUp = true;

        gameObject.active = false;
    }

    public void SelectStaff()
    {
        ItemOnGround groundItem = Instantiate(itemOnGround, GameObject.FindGameObjectWithTag("Player").transform.position, Quaternion.identity);
        groundItem.SetItem(new Item(staff));
        groundItem.pickedUp = true;

        gameObject.active = false;
    }
    public void Select2HSword()
    {
        ItemOnGround groundItem = Instantiate(itemOnGround, GameObject.FindGameObjectWithTag("Player").transform.position, Quaternion.identity);
        groundItem.SetItem(new Item(twoHandedSword));
        groundItem.pickedUp = true;

        gameObject.active = false;
    }
}
