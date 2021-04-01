using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Slider slider;
    public Gradient gradient;
    public Image fill;
    private EntityEvents _events;
    private GameObject player;
    private int health;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Subscribe();
    }

    private void Awake()
    {
        _events = GetComponent<EntityEvents>();
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
    }

    public void SetHealth(int hp)
    {
        slider.value = hp;
        fill.color = gradient.Evaluate(slider.normalizedValue);
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
