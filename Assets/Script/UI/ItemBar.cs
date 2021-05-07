using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemBar : MonoBehaviour
{
    //private EntityEvents entityEvents;
    //private AbilityEvents abilityEvents;
    private GameObject player;

    /*
    void Start()
    {
        SubscribeUseItem();
        SubscribeAddNewItemToSlot();
        SubscribeRemoveItem();
    }
    */

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //entityEvents = player.GetComponent<EntityEvents>();
    }

    [System.Obsolete]
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && Time.timeScale != 0)
        {
            UseItem(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && Time.timeScale != 0)
        {
            UseItem(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && Time.timeScale != 0)
        {
            UseItem(2);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4) && Time.timeScale != 0)
        {
            UseItem(3);
        }
    }

    [System.Obsolete]
    void UseItem(int slot)
    {
        player.GetComponent<Inventory>().inventorySlots[slot].HotbarUseItem();
    }

    /*
    void ItemUsed(UiButtonClick invSlot)
    {

    }

    void AddToSlot(ItemObject item, int slot)
    {

    }

    void RemoveFromSlot(int slot)
    {

    }

    void SubscribeUseItem()
    {
        entityEvents.OnUseItem += ItemUsed;
    }

    void SubscribeAddNewItemToSlot()
    {
        entityEvents.OnAddNewItemToSlot += AddToSlot;
    }

    void SubscribeRemoveItem()
    {
        entityEvents.OnRemoveItem += RemoveFromSlot;
    }

    void UnsubscribeUseItem()
    {
        entityEvents.OnUseItem -= ItemUsed;
    }

    void UnsubscribeAddNewItemToSlot()
    {
        entityEvents.OnAddNewItemToSlot -= AddToSlot;
    }

    void UnsubscribeRemoveItem()
    {
        entityEvents.OnRemoveItem -= RemoveFromSlot;
    }

    private void OnDisable()
    {
        UnsubscribeUseItem();
        UnsubscribeAddNewItemToSlot();
        UnsubscribeRemoveItem();
    }
    */
}
