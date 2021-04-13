using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AbilityBar : MonoBehaviour
{
    public Image fill;
    private EntityEvents entityEvents;
    private AbilityEvents abilityEvents;
    private GameObject player;
    private IAbility ability1;
    private IAbility ability2;


    private void Start()
    {

    }

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        entityEvents = player.GetComponent<EntityEvents>();
    }

    private void Update()
    {
        ability1 = player.GetComponent<EntityAbilityManager>().ability1;
    }
}
