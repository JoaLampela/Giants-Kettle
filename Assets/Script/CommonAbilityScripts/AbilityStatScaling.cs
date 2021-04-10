using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityStatScaling : MonoBehaviour
{
    private AbilityEvents _events;
    private EntityStats _stats;

    [Header("ability damage info")]
    public int spellDamageLevel;
    public float maxDamageScale;
    public float levelUpSpeed;

    [Header("current resources (%-scaling)")]
    public int physicalHealthScaling;
    public int physicalSpiritScaling;
    public int physicalEnergyScaling;
    public int physicalRageScaling;

    [Header("current resource regens (%-scaling)")]
    public int physicalHealthRegenScaling;
    public int physicalRageDepletionScaling;
    public int physicalSpiritRegenScaling;
    public int physicalEnergyRegenScaling;
    public int physicalRageGainScaling;

    [Header("current movement speed stats (%-scaling)")]
    public int physicalSpeedScaling;
    public int physicalSlowScaling;
    public int physicalTenacityScaling;

    [Header("current combat stats (%-scaling)")]
    public int physicalPhysicalDamageScaling;
    public int physicalSpiritDamageScaling;
    public int physicalCriticalStrikeChanceScaling;
    public int physicalSpellHasteScaling;
    public int physicalArmorScaling;
    public int physicalAttackSpeedScaling;

    //---------------------------------------------------

    [Header("current resources (%-scaling)")]
    public int spiritHealthScaling;
    public int spiritSpiritScaling;
    public int spiritEnergyScaling;
    public int spiritRageScaling;

    [Header("current resource regens (%-scaling)")]
    public int spiritHealthRegenScaling;
    public int spiritRageDepletionScaling;
    public int spiritSpiritRegenScaling;
    public int spiritEnergyRegenScaling;
    public int spiritRageGainScaling;

    [Header("current movement speed stats (%-scaling)")]
    public int spiritSpeedScaling;
    public int spiritSlowScaling;
    public int spiritTenacityScaling;

    [Header("current combat stats (%-scaling)")]
    public int spiritPhysicalDamageScaling;
    public int spiritSpiritDamageScaling;
    public int spiritCriticalStrikeChanceScaling;
    public int spiritSpellHasteScaling;
    public int spiritArmorScaling;
    public int spiritAttackSpeedScaling;

    private void Start()
    {
        _stats = _events._abilityCastSource.GetComponent<EntityStats>();
        GetDamage();
    }

    private void Awake()
    {
        _events = GetComponent<AbilityEvents>();
    }

    public void GetDamage()
    {
        _events._damage = new Damage(_events._abilityCastSource, (int)(CalculatePhysicalDamage()), (int)(CalculateSpiritDamage()));
    }

    private int CalculatePhysicalDamage()
    {
        float physicalDamage = 0f;
        physicalDamage += _stats.currentMaxHealth * physicalHealthScaling / 100f;
        physicalDamage += _stats.currentMaxHealth * physicalSpiritScaling / 100f;
        physicalDamage += _stats.currentMaxHealth * physicalEnergyScaling / 100f;
        physicalDamage += _stats.currentMaxHealth * physicalRageScaling / 100f;

        physicalDamage += _stats.currentMaxHealth * physicalHealthRegenScaling / 100f;
        physicalDamage += _stats.currentMaxHealth * physicalRageDepletionScaling / 100f;
        physicalDamage += _stats.currentMaxHealth * physicalSpiritRegenScaling / 100f;
        physicalDamage += _stats.currentMaxHealth * physicalEnergyRegenScaling / 100f;
        physicalDamage += _stats.currentMaxHealth * physicalRageGainScaling / 100f;

        physicalDamage += _stats.currentMaxHealth * physicalSpeedScaling / 100f;
        physicalDamage += _stats.currentMaxHealth * physicalSlowScaling / 100f;
        physicalDamage += _stats.currentMaxHealth * physicalTenacityScaling / 100f;

        physicalDamage += _stats.currentMaxHealth * physicalPhysicalDamageScaling / 100f;
        physicalDamage += _stats.currentMaxHealth * physicalSpiritDamageScaling / 100f;
        physicalDamage += _stats.currentMaxHealth * physicalCriticalStrikeChanceScaling / 100f;
        physicalDamage += _stats.currentMaxHealth * physicalSpellHasteScaling / 100f;
        physicalDamage += _stats.currentMaxHealth * physicalArmorScaling / 100f;
        physicalDamage += _stats.currentMaxHealth * physicalAttackSpeedScaling / 100f;

        return (int)physicalDamage;
    }

    private int CalculateSpiritDamage()
    {
        float spiritDamage = 0f;
        spiritDamage += _stats.currentMaxHealth * spiritHealthScaling / 100f;
        spiritDamage += _stats.currentMaxHealth * spiritSpiritScaling / 100f;
        spiritDamage += _stats.currentMaxHealth * spiritEnergyScaling / 100f;
        spiritDamage += _stats.currentMaxHealth * spiritRageScaling / 100f;

        spiritDamage += _stats.currentMaxHealth * spiritHealthRegenScaling / 100f;
        spiritDamage += _stats.currentMaxHealth * spiritRageDepletionScaling / 100f;
        spiritDamage += _stats.currentMaxHealth * spiritSpiritRegenScaling / 100f;
        spiritDamage += _stats.currentMaxHealth * spiritEnergyRegenScaling / 100f;
        spiritDamage += _stats.currentMaxHealth * spiritRageGainScaling / 100f;

        spiritDamage += _stats.currentMaxHealth * spiritSpeedScaling / 100f;
        spiritDamage += _stats.currentMaxHealth * spiritSlowScaling / 100f;
        spiritDamage += _stats.currentMaxHealth * spiritTenacityScaling / 100f;

        spiritDamage += _stats.currentMaxHealth * spiritPhysicalDamageScaling / 100f;
        spiritDamage += _stats.currentMaxHealth * spiritSpiritDamageScaling / 100f;
        spiritDamage += _stats.currentMaxHealth * spiritCriticalStrikeChanceScaling / 100f;
        spiritDamage += _stats.currentMaxHealth * spiritSpellHasteScaling / 100f;
        spiritDamage += _stats.currentMaxHealth * spiritArmorScaling / 100f;
        spiritDamage += _stats.currentMaxHealth * spiritAttackSpeedScaling / 100f;

        return (int)spiritDamage;
    }
}
