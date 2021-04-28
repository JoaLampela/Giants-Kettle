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
        
        if(Random.Range(0,100) <= dropChance)
        {
            Debug.Log("item dropped");
            int enemyLevel =  GetComponent<EntityStats>().level;
            ItemOnGround groundItem = Instantiate(itemOnGround, gameObject.transform.position, Quaternion.identity);
            ItemTierListScript tierList = GameObject.Find("Game Manager").GetComponent<ItemTierListScript>();
            groundItem.SetItem(new Item(tierList.GiveRandomItem(enemyLevel-1)));
        }
    }
}
