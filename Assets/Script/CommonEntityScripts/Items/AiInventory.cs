using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiInventory : MonoBehaviour
{
    private EntityAbilityManager abilityManager;
    [SerializeField] private RuneObject[] equippedArmorRunes;
    public WeaponObject rightHandWeapon;
    public WeaponObject leftHandWeapon;

    [SerializeField] private WeaponObject[] possibleWeapons;
    [SerializeField] private RuneObject[] armorRunes;
    [SerializeField] private int minRuneCount;
    [SerializeField] private int maxRuneCount;
    [SerializeField] private bool canDualWield;


    private void Awake()
    {
        abilityManager = GetComponent<EntityAbilityManager>();
    }
    private void Start()
    {
        RandomizeEquippedWeapons();
        RandomizeEquippedRunes();

        if (rightHandWeapon != null) AbilitySetUp(rightHandWeapon, WeaponHand.RightHand);
        if (leftHandWeapon != null) AbilitySetUp(leftHandWeapon, WeaponHand.LeftHand);
    }

    private void RandomizeEquippedWeapons()
    {
        rightHandWeapon = possibleWeapons[Random.Range(0, possibleWeapons.Length)];
        if(rightHandWeapon.isTwoHander)
        {
            leftHandWeapon = rightHandWeapon;
        }
        if(canDualWield && !rightHandWeapon.isTwoHander)
        {
            WeaponObject temp = possibleWeapons[Random.Range(0, possibleWeapons.Length)];
            if(!temp.isTwoHander) leftHandWeapon = temp;
        }
    }
    private void RandomizeEquippedRunes()
    {
        int runeCount = Random.Range(minRuneCount, maxRuneCount+1);
        equippedArmorRunes = new RuneObject[runeCount];
        for(int i = 0; i < runeCount; i++)
        {
            equippedArmorRunes[i] = armorRunes[Random.Range(0, armorRunes.Length)];
        }
    }
    private enum WeaponHand{
        RightHand,
        LeftHand
    }
    private void AbilitySetUp(WeaponObject weapon, WeaponHand hand)
    {
        switch (weapon.weaponType)
        {
            case (WeaponObject.WeaponType.OneHandedSword):
                if (hand == WeaponHand.RightHand)
                {
                    //Replace with enemy weapon equip player_Animations.SwitchToSingleHandedSword(weapon.inGameObject);
                    StingRight stingRight = gameObject.AddComponent<StingRight>();
                    abilityManager.SetAbility(2, stingRight);
                }
                if (hand == WeaponHand.LeftHand)
                {
                    //Replace with enemy weapon equip player_Animations.SwitchToOffHandSingleHandedSword(weapon.inGameObject);
                    StingLeft stingLeft = gameObject.AddComponent<StingLeft>();
                    abilityManager.SetAbility(1, stingLeft);
                }
                break;
            case (WeaponObject.WeaponType.TwoHandedSword):
                Debug.Log("Equip 2 hander");
                //Replace with enemy weapon equip player_Animations.SwitchToTwoHandedSword(weapon.inGameObject);
                Sting2Handed sting2Handed = gameObject.AddComponent<Sting2Handed>();
                abilityManager.SetAbility(2, sting2Handed);
                SpinAttack spinAttack = gameObject.AddComponent<SpinAttack>();
                abilityManager.SetAbility(1, spinAttack);
                break;
            case (WeaponObject.WeaponType.Shield):
                if (hand == WeaponHand.RightHand)
                {
                    //Replace with enemy weapon equip player_Animations.SwitchToShield(weapon.inGameObject);
                    ShieldToss shieldToss = gameObject.AddComponent<ShieldToss>();
                    abilityManager.SetAbility(2, shieldToss);
                }
                if (hand == WeaponHand.LeftHand)
                {
                    //Replace with enemy weapon equip player_Animations.SwitchToOffHandShield(weapon.inGameObject);
                    Block block = gameObject.AddComponent<Block>();
                    abilityManager.SetAbility(1, block);
                }
                break;
            case (WeaponObject.WeaponType.Bow):
                //Replace with enemy weapon equip player_Animations.SwitchToBow(weapon.inGameObject);
                PowerShot powerShot = gameObject.AddComponent<PowerShot>();
                abilityManager.SetAbility(2, powerShot);
                TripleShot tripleShot = gameObject.AddComponent<TripleShot>();
                abilityManager.SetAbility(1, tripleShot);
                break;
            case (WeaponObject.WeaponType.Staff):
                //Replace with enemy weapon equip player_Animations.SwitchToStaff(weapon.inGameObject);
                BigProjectile bigProjectile = gameObject.AddComponent<BigProjectile>();
                abilityManager.SetAbility(2, bigProjectile);
                NonProjectile nonProjectile = gameObject.AddComponent<NonProjectile>();
                abilityManager.SetAbility(1, nonProjectile);
                break;
        }
    }
}