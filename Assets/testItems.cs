using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testItems : MonoBehaviour
{
    [SerializeField] float count = 0;

    [SerializeField] EquipmentObject helmet;
    [SerializeField] EquipmentObject chest;
    [SerializeField] EquipmentObject boots;
    [SerializeField] EquipmentObject shield;
    [SerializeField] EquipmentObject sword;
    [SerializeField] EquipmentObject twoHander;
    [SerializeField] EquipmentObject staff;
    [SerializeField] ItemOnGround itemOnGround;

    bool dropped = false;


    private void Update()
    {
        if(Input.GetKey(KeyCode.P))
        {
            count += Time.deltaTime;
        }
        else
        {
            count = 0;
        }

        if(count >= 5 && !dropped)
        {
            dropped = true;
            ItemOnGround groundItem = Instantiate(itemOnGround, transform.position, Quaternion.identity);
            groundItem.GetComponent<ItemOnGround>()._item = new Item(helmet);

            groundItem = Instantiate(itemOnGround, transform.position, Quaternion.identity);
            groundItem.GetComponent<ItemOnGround>()._item = new Item(chest);

            groundItem = Instantiate(itemOnGround, transform.position, Quaternion.identity);
            groundItem.GetComponent<ItemOnGround>()._item = new Item(boots);

            groundItem = Instantiate(itemOnGround, transform.position, Quaternion.identity);
            groundItem.GetComponent<ItemOnGround>()._item = new Item(shield);

            groundItem = Instantiate(itemOnGround, transform.position, Quaternion.identity);
            groundItem.GetComponent<ItemOnGround>()._item = new Item(sword);

            groundItem = Instantiate(itemOnGround, transform.position, Quaternion.identity);
            groundItem.GetComponent<ItemOnGround>()._item = new Item(twoHander);

            groundItem = Instantiate(itemOnGround, transform.position, Quaternion.identity);
            groundItem.GetComponent<ItemOnGround>()._item = new Item(staff);

            groundItem = Instantiate(itemOnGround, transform.position, Quaternion.identity);
            groundItem.GetComponent<ItemOnGround>()._item = new Item(staff);

        }
    }
}
