using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityTargetPositionMouse : MonoBehaviour, IAbilityTargetPosition
{
    public Vector2 GetTargetPosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
