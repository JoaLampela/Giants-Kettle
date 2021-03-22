using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Interface to ensure that Ability script is able to set target's position
public interface IAbilityTargetPosition
{
    Vector2 GetTargetPosition(); //Used to get target position of the Ability
}
