using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiInventory : MonoBehaviour
{
    private EntityAbilityManager abilityManager;
    [SerializeField] private RuneObject[] equippedArmorRunes;
    public WeaponObject rightHandWeapon;
    public WeaponObject leftHandWeapon;
    public Item dashItem;
    public WeaponObject dashItemObject;

    [SerializeField] private WeaponObject[] possibleWeapons;
    [SerializeField] private RuneObject[] armorRunes;
    [SerializeField] private int minRuneCount;
    [SerializeField] private int maxRuneCount;
    [SerializeField] private bool canDualWield;
    [SerializeField] private Humanoid_Animations humanoid_Animations;

    private void Awake()
    {
        abilityManager = GetComponent<EntityAbilityManager>();
        humanoid_Animations = GetComponent<Humanoid_Animations>();
    }
    private void Start()
    {
        dashItem = new Item(dashItemObject);
        if (possibleWeapons.Length > 0)
        {
            RandomizeEquippedWeapons();
            RandomizeEquippedRunes();
            if (rightHandWeapon != null) AbilitySetUp(rightHandWeapon, WeaponHand.RightHand);
            if (leftHandWeapon != null && !leftHandWeapon.isTwoHander) AbilitySetUp(leftHandWeapon, WeaponHand.LeftHand);
        }
    }

    private void RandomizeEquippedWeapons()
    {
        rightHandWeapon = possibleWeapons[Random.Range(0, possibleWeapons.Length)];
        if (rightHandWeapon.isTwoHander)
        {
            leftHandWeapon = rightHandWeapon;
        }
        if (canDualWield && !rightHandWeapon.isTwoHander)
        {
            WeaponObject temp = possibleWeapons[Random.Range(0, possibleWeapons.Length)];
            if (!temp.isTwoHander) leftHandWeapon = temp;
        }
    }
    private void RandomizeEquippedRunes()
    {
        int runeCount = Random.Range(minRuneCount, maxRuneCount + 1);
        equippedArmorRunes = new RuneObject[runeCount];
        for (int i = 0; i < runeCount; i++)
        {
            equippedArmorRunes[i] = armorRunes[Random.Range(0, armorRunes.Length)];
        }
    }
    private enum WeaponHand
    {
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
                    humanoid_Animations.SwitchToSingleHandedSword(weapon.inGameObject);
                    StingRight stingRight = gameObject.AddComponent<StingRight>();
                    abilityManager.SetAbility(2, stingRight);
                    OneHandedBasicAttack oneHandedBasicAttack = gameObject.AddComponent<OneHandedBasicAttack>();
                    abilityManager.SetAbility(4, oneHandedBasicAttack);
                }
                if (hand == WeaponHand.LeftHand)
                {
                    humanoid_Animations.SwitchToOffHandSingleHandedSword(weapon.inGameObject);
                    StingLeft stingLeft = gameObject.AddComponent<StingLeft>();
                    abilityManager.SetAbility(1, stingLeft);
                }
                break;
            case (WeaponObject.WeaponType.TwoHandedSword):
                humanoid_Animations.SwitchToTwoHandedSword(weapon.inGameObject);
                Sting2Handed sting2Handed = gameObject.AddComponent<Sting2Handed>();
                abilityManager.SetAbility(2, sting2Handed);
                SpinAttack spinAttack = gameObject.AddComponent<SpinAttack>();
                abilityManager.SetAbility(1, spinAttack);
                TwoHandedBasicAttack twoHandedBasicAttack = gameObject.AddComponent<TwoHandedBasicAttack>();
                abilityManager.SetAbility(4, twoHandedBasicAttack);
                break;
            case (WeaponObject.WeaponType.Shield):
                if (hand == WeaponHand.RightHand)
                {
                    humanoid_Animations.SwitchToShield(weapon.inGameObject);
                    ShieldToss shieldToss = gameObject.AddComponent<ShieldToss>();
                    abilityManager.SetAbility(2, shieldToss);
                }
                if (hand == WeaponHand.LeftHand)
                {
                    humanoid_Animations.SwitchToOffHandShield(weapon.inGameObject);
                    Block block = gameObject.AddComponent<Block>();
                    abilityManager.SetAbility(1, block);
                }
                break;
            case (WeaponObject.WeaponType.Bow):
                humanoid_Animations.SwitchToBow(weapon.inGameObject);
                PowerShot powerShot = gameObject.AddComponent<PowerShot>();
                abilityManager.SetAbility(2, powerShot);
                TripleShot tripleShot = gameObject.AddComponent<TripleShot>();
                abilityManager.SetAbility(1, tripleShot);
                break;
            case (WeaponObject.WeaponType.Staff):
                humanoid_Animations.SwitchToStaff(weapon.inGameObject);
                BigProjectile bigProjectile = gameObject.AddComponent<BigProjectile>();
                abilityManager.SetAbility(2, bigProjectile);
                NonProjectile nonProjectile = gameObject.AddComponent<NonProjectile>();
                abilityManager.SetAbility(1, nonProjectile);
                break;
        }
    }
}
