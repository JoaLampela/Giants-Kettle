using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InfoPanelScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI healthRegenText;
    [SerializeField] private TextMeshProUGUI armorText;
    [SerializeField] private TextMeshProUGUI damageText;
    [SerializeField] private TextMeshProUGUI CritChanceText;
    [SerializeField] private TextMeshProUGUI hasteText;
    [SerializeField] private TextMeshProUGUI attackSpeedText;
    [SerializeField] private TextMeshProUGUI tenacityText;
    [SerializeField] private TextMeshProUGUI speedText;

    private bool infoPanelOn = false;
    [SerializeField] private CanvasGroup canvasGroup;

    private EntityStats stats;

    public void ToggleInfoPanel()
    {
        if(!infoPanelOn)
        {
            infoPanelOn = true;
            canvasGroup.alpha = 1;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;

        }
        else
        {
            infoPanelOn = false;
            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;

        }
    }

    private void Update()
    {
        if (stats == null) stats = GameObject.FindGameObjectWithTag("Player").GetComponent<EntityStats>();

        healthText.text = stats.currentMaxHealth.ToString();
        healthRegenText.text = stats.currentHealthRegen.ToString();
        armorText.text = stats.currentArmor.ToString();
        damageText.text = stats.currentPhysicalDamage.ToString();
        CritChanceText.text = stats.currentCriticalStrikeChance.ToString();
        hasteText.text = stats.currentSpellHaste.ToString();
        attackSpeedText.text = stats.currentAttackSpeed.ToString();
        tenacityText.text = stats.currentTenacity.ToString();
        speedText.text = stats.currentSpeed.ToString();
    }
}
