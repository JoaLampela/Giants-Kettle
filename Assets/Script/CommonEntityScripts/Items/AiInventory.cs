using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiInventory : MonoBehaviour
{
    [SerializeField]private WeaponObject defaultItem;
    private EntityAbilityManager abilityManager;
    [SerializeField] private RuneObject[] equippedArmorRunes;
    public WeaponObject rightHandWeapon;
    public WeaponObject leftHandWeapon;
    public Item dashItem;
    public WeaponObject dashItemObject;

    [SerializeField] private WeaponObject[] possibleWeapons;
    [SerializeField] private WeaponObject[] lunaticWeapons;

    [SerializeField] private RuneObject[] armorRunes;
    [SerializeField] private RuneObject[] lunaticRunes;

    [SerializeField] private int minRuneCount;
    [SerializeField] private int maxRuneCount;

    [SerializeField] private int minLunaticRuneCount;
    [SerializeField] private int maxLunaticRuneCount;

    [SerializeField] private bool canDualWield;
    [SerializeField] private Humanoid_Animations humanoid_Animations;

    [SerializeField] 

    private void Awake()
    {
        abilityManager = GetComponent<EntityAbilityManager>();
        humanoid_Animations = GetComponent<Humanoid_Animations>();
    }
    private void Start()
    {
        dashItem = new Item(dashItemObject);
        if (GameObject.Find("Game Manager").GetComponent<GameEventManager>().difficulty == GameDifficultyManagerScript.Difficulty.Lunatic)
        {
            if (lunaticWeapons.Length > 0)
            {
                RandomizeEquippedWeapons(lunaticWeapons);
                
                if (rightHandWeapon != null) AbilitySetUp(rightHandWeapon, WeaponHand.RightHand);
                if (leftHandWeapon != null && !leftHandWeapon.isTwoHander) AbilitySetUp(leftHandWeapon, WeaponHand.LeftHand);
            }
            RandomizeEquippedRunes(lunaticRunes, minLunaticRuneCount, maxLunaticRuneCount);
        }
        else
        {
            if (possibleWeapons.Length > 0)
            {
                RandomizeEquippedWeapons(possibleWeapons);
                
                if (rightHandWeapon != null) AbilitySetUp(rightHandWeapon, WeaponHand.RightHand);
                if (leftHandWeapon != null && !leftHandWeapon.isTwoHander) AbilitySetUp(leftHandWeapon, WeaponHand.LeftHand);
                
            }
            RandomizeEquippedRunes(armorRunes, minRuneCount, maxRuneCount);
        }
        AbilitySetUp();


    }

    private void RandomizeEquippedWeapons(WeaponObject[] weapons)
    {
        rightHandWeapon = weapons[Random.Range(0, weapons.Length)];
        if (rightHandWeapon.isTwoHander)
        {
            leftHandWeapon = rightHandWeapon;
        }
        if (canDualWield && !rightHandWeapon.isTwoHander)
        {
            WeaponObject temp = weapons[Random.Range(0, weapons.Length)];
            if (!temp.isTwoHander) leftHandWeapon = temp;
        }
    }
    private void RandomizeEquippedRunes(RuneObject[] possibleRunes ,int minRunes, int maxRunes)
    {
        int runeCount = Random.Range(minRunes, maxRunes + 1);
        equippedArmorRunes = new RuneObject[runeCount];
        for (int i = 0; i < runeCount; i++)
        {
            equippedArmorRunes[i] = possibleRunes[Random.Range(0, possibleRunes.Length)];
        }
    }
    private enum WeaponHand
    {
        RightHand,
        LeftHand
    }
    private void AbilitySetUp(WeaponObject weapon, WeaponHand hand)
    {
        List<ItemObject> item = new List<ItemObject>();
        if (weapon.baseRune != null) item.Add(weapon.baseRune);

        if(hand == WeaponHand.RightHand)
        {
            StartCoroutine(NewAffectingRune(new Item(weapon), item, IRuneScript.Hand.right));
        }
        if(hand == WeaponHand.LeftHand)
        {
            StartCoroutine(NewAffectingRune(new Item(weapon), item, IRuneScript.Hand.left));
        }
        

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
                StaffBasicAttack staffBasicAttack = gameObject.AddComponent<StaffBasicAttack>();
                abilityManager.SetAbility(4, staffBasicAttack);
                break;
        }
    }


    private void AbilitySetUp()
    {
        List<ItemObject> item = new List<ItemObject>();
        foreach (RuneObject rune in equippedArmorRunes)
        {
            item.Add(rune);
        }

        StartCoroutine(NewAffectingRuneNonWeapon(new Item(defaultItem), item, IRuneScript.Hand.indeterminate));
    }



    public IEnumerator NewAffectingRune(Item newItem, List<ItemObject> newRunes, IRuneScript.Hand hand)
    {
        Debug.Log("New effecting rune enemy");

        yield return 0;
        if (true)
        {
            foreach (ItemObject item in newRunes)
            {
                RuneObject rune = (RuneObject)item;

                if (!gameObject.GetComponent(rune._IruneContainer.Result.GetType()))
                {
                    gameObject.AddComponent(rune._IruneContainer.Result.GetType());
                    IRuneScript tempRuneScript = (IRuneScript)gameObject.GetComponent(rune._IruneContainer.Result.GetType());
                    tempRuneScript.SetEntity(gameObject);
                    tempRuneScript.SetContainerItem(newItem, hand);
                }

                IRuneScript runeScript = (IRuneScript)gameObject.GetComponent(rune._IruneContainer.Result.GetType());

                if (rune.runeTier == RuneObject.RuneTier.basic)
                {
                    runeScript.IncrementDuplicateCountWeapon(1, hand);
                }
                else if (rune.runeTier == RuneObject.RuneTier.refined)
                {
                    runeScript.IncrementDuplicateCountWeapon(2, hand);
                }
                else if (rune.runeTier == RuneObject.RuneTier.perfected)
                {
                    runeScript.IncrementDuplicateCountWeapon(3, hand);
                }
                else if (rune.runeTier == RuneObject.RuneTier.superb)
                {
                    runeScript.IncrementDuplicateCountWeapon(4, hand);
                }

            }
            List<IRuneScript> dublicateComponents = new List<IRuneScript>();
            foreach (ItemObject item in newRunes)
            {
                RuneObject rune = (RuneObject)item;
                if (gameObject.GetComponent(rune._IruneContainer.Result.GetType()) && !dublicateComponents.Contains(rune._IruneContainer.Result))
                {
                    IRuneScript tempRuneScript = (IRuneScript)gameObject.GetComponent(rune._IruneContainer.Result.GetType());
                    bool duplicate = false;
                    foreach (IRuneScript runeScript in dublicateComponents)
                    {
                        if (runeScript.GetType() == tempRuneScript.GetType())
                        {
                            duplicate = true;
                        }
                    }
                    if (!duplicate)
                    {
                        tempRuneScript.SetUpPermanentEffects();
                        dublicateComponents.Add(tempRuneScript);
                    }
                }
            }
        }
    }


    public IEnumerator NewAffectingRuneNonWeapon(Item newItem, List<ItemObject> newRunes, IRuneScript.Hand hand)
    {
        Debug.Log("New effecting rune");

        yield return 0;
        if (true)
        {
            foreach (ItemObject item in newRunes)
            {
                RuneObject rune = (RuneObject)item;

                if (!gameObject.GetComponent(rune._IruneContainer.Result.GetType()))
                {
                    gameObject.AddComponent(rune._IruneContainer.Result.GetType());
                    IRuneScript tempRuneScript = (IRuneScript)gameObject.GetComponent(rune._IruneContainer.Result.GetType());
                    tempRuneScript.SetEntity(gameObject);
                    tempRuneScript.SetContainerItem(newItem, hand);
                }


                IRuneScript runeScript = (IRuneScript)gameObject.GetComponent(rune._IruneContainer.Result.GetType());

                if (rune.runeTier == RuneObject.RuneTier.basic)
                {
                    runeScript.IncrementDuplicateCountArmor(1);
                }
                else if (rune.runeTier == RuneObject.RuneTier.refined)
                {
                    runeScript.IncrementDuplicateCountArmor(2);
                }
                else if (rune.runeTier == RuneObject.RuneTier.perfected)
                {
                    runeScript.IncrementDuplicateCountArmor(3);
                }
                else if (rune.runeTier == RuneObject.RuneTier.superb)
                {
                    runeScript.IncrementDuplicateCountArmor(4);
                }
            }
            List<IRuneScript> dublicateComponents = new List<IRuneScript>();
            foreach (ItemObject item in newRunes)
            {
                RuneObject rune = (RuneObject)item;
                if (gameObject.GetComponent(rune._IruneContainer.Result.GetType()) && !dublicateComponents.Contains(rune._IruneContainer.Result))
                {
                    IRuneScript tempRuneScript = (IRuneScript)gameObject.GetComponent(rune._IruneContainer.Result.GetType());
                    bool duplicate = false;
                    foreach (IRuneScript runeScript in dublicateComponents)
                    {
                        if (runeScript.GetType() == tempRuneScript.GetType())
                        {
                            duplicate = true;
                        }
                    }
                    if (!duplicate)
                    {
                        tempRuneScript.SetUpPermanentEffects();
                        dublicateComponents.Add(tempRuneScript);
                    }
                }
            }
        }
    }
}
