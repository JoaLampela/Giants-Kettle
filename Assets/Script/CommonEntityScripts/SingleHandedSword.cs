using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleHandedSword : MonoBehaviour
{
    public GameObject player;

    public void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log(collision);
        if (collision.GetComponent<EntityEvents>())
            //collision.GetComponent<EntityEvents>().ChangeTeam(2);
            collision.GetComponent<EntityEvents>().HitThis(new Damage(player, player.GetComponent<EntityStats>().currentMaxPhysicalDamage, 0));
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision);
        if (collision.GetComponent<EntityEvents>())
            //collision.GetComponent<EntityEvents>().ChangeTeam(2);
            collision.GetComponent<EntityEvents>().HitThis(new Damage(player, player.GetComponent<EntityStats>().currentMaxPhysicalDamage, 0));
    }
    public void OnTriggerLeave2D(Collider2D collision)
    {
        Debug.Log(collision);
        if (collision.GetComponent<EntityEvents>())
            //collision.GetComponent<EntityEvents>().ChangeTeam(2);
            collision.GetComponent<EntityEvents>().HitThis(new Damage(player, player.GetComponent<EntityStats>().currentMaxPhysicalDamage, 0));
    }
}
