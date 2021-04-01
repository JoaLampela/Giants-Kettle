using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityTargetPositionMouse : MonoBehaviour, IAbilityTargetPosition
{
    public Vector2 GetTargetPosition()
    {
        Vector2 targetVC2 = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
        return targetVC2;
    }
}
