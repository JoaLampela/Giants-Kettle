using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AbilityBar : MonoBehaviour
{
    private Image dashIcon;
    private Image leftIcon;
    private Image rightIcon;
    private EntityEvents entityEvents;
    private AbilityEvents abilityEvents;
    private GameObject player;
    private IAbility dash;
    private IAbility left;
    private IAbility right;


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
        left = player.GetComponent<EntityAbilityManager>().ability1;
        right = player.GetComponent<EntityAbilityManager>().ability2;
        dash = player.GetComponent<EntityAbilityManager>().ability3;
    }
}
