using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RageBar : MonoBehaviour
{

    public Slider slider;
    private EntityEvents _events;
    private GameObject player;
    [SerializeField] private int rage;


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
        //rage = player.GetComponent<EntityRage>().rage;
        SetRage(rage);
    }

    public void SetMaxRage(int rage)
    {
        slider.maxValue = rage;
    }

    public void SetRage(int rage)
    {
        slider.value = rage;
    }

    private void Subscribe()
    {
        //_events.;
    }

    private void Unsubscribe()
    {
        //_events.;
    }

    private void OnDisable()
    {
        Unsubscribe();
    }

}
