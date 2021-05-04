using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class entityDropItemOnDeath : MonoBehaviour
{
    public ItemOnGround itemOnGround;
    [SerializeField] private int dropChance;
    private EntityEvents events;

    private void Awake()
    {
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
        int enemyLevel = GetComponent<EntityStats>().level;
        


        if (Random.Range(0,100) <= dropChance)
        {            
            ItemOnGround groundItem = Instantiate(itemOnGround, gameObject.transform.position, Quaternion.identity);
            ItemTierListScript tierList = GameObject.Find("Game Manager").GetComponent<ItemTierListScript>();
            Item item = new Item(tierList.GiveRandomItem(enemyLevel));
            Debug.Log(item);
            groundItem.SetItem(item);
        }
    }
}
