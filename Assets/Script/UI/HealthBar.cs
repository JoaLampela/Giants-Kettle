using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    public TMP_Text healthText;
    private EntityEvents _events;
    private GameObject player;
    private int health;
    private float shield;
    private int maxHealth;
    private EntityHealth healthScript;
    private EntityStats stats;
    [SerializeField] private Image healthBar;
    [SerializeField] private Image shieldBar;



    private void Start()
    {
        Subscribe();
    }

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        healthScript = player.GetComponent<EntityHealth>();
        
        _events = player.GetComponent<EntityEvents>();
        stats = player.GetComponent<EntityStats>();
    }

    private void Update()
    {
        health = healthScript.health;
        shield = stats.currentShield;
        SetHealthAndShield(health);
    }

    public void SetMaxHealth(int hp)
    {
        maxHealth = hp;
    }

    public void SetHealthAndShield(int hp)
    {

        healthBar.fillAmount = (float)hp / (float)maxHealth;
        shieldBar.fillAmount = shield / (float)maxHealth;
        healthText.text = (hp+(int)shield).ToString();
    }

    private void Subscribe()
    {
        _events.OnSetHealth += SetMaxHealth;
    }

    private void Unsubscribe()
    {
        _events.OnSetHealth -= SetMaxHealth;
    }

    private void OnDisable()
    {
        Unsubscribe();
    }
}
