using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashWhiteOnDamage : MonoBehaviour
{
    public float flashTime = 0.1f;
    public Material whiteMaterial;
    EntityEvents events;
    SpriteRenderer[] spriteRenderers;
    List<Material> defaultMaterials;
    private void Awake()
    {
        defaultMaterials = new List<Material>();
        events = GetComponent<EntityEvents>();
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer sr in spriteRenderers)
        {
            defaultMaterials.Add(sr.material);
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        Subscribe();
    }
    private void OnDisable()
    {
        Unsubscribe();
    }
    private void Subscribe()
    {
        events.OnLoseHealth += FlashWhite;
    }

    private void Unsubscribe()
    {
        events.OnLoseHealth -= FlashWhite;
    }
    private void FlashWhite(int damage)
    {
        StartCoroutine("FlashWhiteCoroutine");
    }


    private IEnumerator FlashWhiteCoroutine()
    {
        foreach (SpriteRenderer sr in spriteRenderers)
        {
            sr.material = whiteMaterial;
        }
        yield return new WaitForSeconds(flashTime);
        List<Material>.Enumerator matEnumerator = defaultMaterials.GetEnumerator();
        foreach (SpriteRenderer sr in spriteRenderers)
        {
            if (!matEnumerator.MoveNext())
                break;
            sr.material = matEnumerator.Current;
        }
    }
}
