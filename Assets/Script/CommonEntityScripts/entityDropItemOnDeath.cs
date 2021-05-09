using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class entityDropItemOnDeath : MonoBehaviour
{
    public ItemOnGround itemOnGround;
    public bool dropLegendaryItem = false;
    public bool dropEasterEggItem = false;
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
            if (dropEasterEggItem)
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
                ItemOnGround groundItem = Instantiate(itemOnGround, gameObject.transform.position, Quaternion.identity);
                ItemTierListScript tierList = GameObject.Find("Game Manager").GetComponent<ItemTierListScript>();
                Item item = new Item(tierList.GiveRandomItem(gameEventManager.globalLevel + 1));
                Debug.Log(item);
                groundItem.SetItem(item);

                EquipmentObject equipment = (EquipmentObject)item.item;
                GameObject.Find("Game Manager").GetComponent<GameEventManager>().EquipmentDropped(equipment);
            }
        }
    }
}
