using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{

    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public TMP_Text healthText;
    private EntityEvents _events;
    private GameObject player;
    private int health;
    private int maxHealth;


    private void Start()
    {
        Subscribe();
    }

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        _events = player.GetComponent<EntityEvents>();
    }

    private void Update()
    {
        health = player.GetComponent<EntityHealth>().health;
        SetHealth(health);
    }

    public void SetMaxHealth(int hp)
    {
        slider.maxValue = hp;
        slider.value = hp;
        fill.color = gradient.Evaluate(1f);
        maxHealth = hp;
    }

    public void SetHealth(int hp)
    {
        slider.value = hp;
        fill.color = gradient.Evaluate(slider.normalizedValue);
        healthText.text = hp + " / " + maxHealth;
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
