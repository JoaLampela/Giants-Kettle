using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class entityDropItemOnDeath : MonoBehaviour
{
    public ItemOnGround itemOnGround;
    public bool dropLegendaryItem = false;
    public ItemObject easterEggItem;
    [SerializeField] private int dropChance;
    private EntityEvents events;
    private GameEventManager gameEventManager;

    private void Awake()
    {
        gameEventManager = GameObject.Find("Game Manager").GetComponent<GameEventManager>();
        events = GetComponent<EntityEvents>();
    }

    void Start()
    {
        Subscribe();
    }

    private void OnDisable()
    {
        Unsubscribe();
    }

    private void Subscribe()
    {
        events.OnDie += DropItem;
    }
    private void Unsubscribe()
    {
        events.OnDie -= DropItem;
    }
    private void DropItem(GameObject killer, GameObject killed)
    {
        if (Random.Range(0, 100) <= dropChance)
        {
            if (easterEggItem != null)
            {
                ItemOnGround groundItem = Instantiate(itemOnGround, gameObject.transform.position, Quaternion.identity);
                Debug.Log(easterEggItem);
                Item easterEgg = new Item(easterEggItem);
                groundItem.SetItem(easterEgg);

                EquipmentObject equipment = (EquipmentObject)easterEgg.item;
                GameObject.Find("Game Manager").GetComponent<GameEventManager>().EquipmentDropped(equipment);
            }
            else if (dropLegendaryItem)
            {
                ItemOnGround groundItem = Instantiate(itemOnGround, gameObject.transform.position, Quaternion.identity);
                ItemTierListScript tierList = GameObject.Find("Game Manager").GetComponent<ItemTierListScript>();
                Item item = new Item(tierList.GiveRandomLegendaryItem());
                Debug.Log(item);
                groundItem.SetItem(item);

                EquipmentObject equipment = (EquipmentObject)item.item;
                GameObject.Find("Game Manager").GetComponent<GameEventManager>().EquipmentDropped(equipment);
            }
            else
            {

                
                ItemTierListScript tierList = GameObject.Find("Game Manager").GetComponent<ItemTierListScript>();
                Item item = new Item(tierList.GiveRandomItem(gameEventManager.globalLevel + 1));

                EquipmentObject equipmentObject = (EquipmentObject)item.item;

                if(equipmentObject.baseRune != null || CheckPlayerEquipment(item))
                {
                    ItemOnGround groundItem = Instantiate(itemOnGround, gameObject.transform.position, Quaternion.identity);
                    groundItem.SetItem(item);

                    EquipmentObject equipment = (EquipmentObject)item.item;
                    GameObject.Find("Game Manager").GetComponent<GameEventManager>().EquipmentDropped(equipment);
                }  
            }
        }
    }

    private bool CheckPlayerEquipment(Item item)
    {
        Inventory inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();

        foreach (UiButtonClick slot in inventory.equipmentSlots)
        {
            Debug.Log(slot);
        }


            
        foreach(UiButtonClick slot in inventory.inventorySlots)
        {
            Debug.Log("SLOT " + slot);
            if (slot._item != null)
            {
                if(slot._item.item.type == item.item.type)
                {
                    if(item.item.type == ItemType.Weapon)
                    {
                        WeaponObject newWeapon = (WeaponObject)item.item;
                        WeaponObject oldWeapn = (WeaponObject)slot._item.item;

                        Debug.Log("new item weapon type " + newWeapon.weaponType + " old item weapon type " + oldWeapn.weaponType);
                        if(newWeapon.weaponType == oldWeapn.weaponType)
                        {
                            Debug.Log("new item rune slots " + newWeapon.runeSlots + " old item rune slot " + oldWeapn.runeSlots);
                            if (newWeapon.runeSlots <= oldWeapn.runeSlots)
                            {
                                return false;
                            }
                        }
                    }
                    else if(item.item.type != ItemType.Rune)
                    {
                        if (item._runeList.Length <= slot._item._runeList.Length)
                        {
                            return false;
                        }
                    }
                }
            }
        }
        Debug.Log("Done with inventory slots");
        foreach(UiButtonClick slot in inventory.equipmentSlots)
        {
            Debug.Log("eq SLOT " + slot);
            if (slot._item != null)
            {
                Debug.Log("slot not full " + slot);
                if (slot._item.item.type == item.item.type)
                {
                    Debug.Log(slot._item.item.type + " type match " + item.item.type + " " + slot);
                    if (item.item.type == ItemType.Weapon)
                    {
                        Debug.Log("is weapon " + slot);
                        WeaponObject newWeapon = (WeaponObject)item.item;
                        WeaponObject oldWeapn = (WeaponObject)slot._item.item;

                        if (newWeapon.weaponType == oldWeapn.weaponType)
                        {
                            if (newWeapon.runeSlots <= oldWeapn.runeSlots)
                            {
                                return false;
                            }
                        }
                    }
                    else if (item.item.type != ItemType.Rune)
                    {
                        if (item._runeList.Length <= slot._item._runeList.Length)
                        {
                            return false;
                        }
                    }
                }
            }
        }
        return true;
    }
}
