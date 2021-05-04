
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopup : MonoBehaviour
{


    public static DamagePopup Create(Vector3 position, int damageAmount, bool isCriticalHit, bool isTrueDamage)
    {
        Vector2 rand = new Vector2(UnityEngine.Random.Range(0f, 2f), UnityEngine.Random.Range(0f, 2f));
        Transform damagePopupTransform = Instantiate(GameAssets.i.pfDamagePopup, (Vector2)position + rand, Quaternion.identity);
        DamagePopup damagePopup = damagePopupTransform.GetComponent<DamagePopup>();
        damagePopup.Setup(damageAmount, isCriticalHit, isTrueDamage);
        return damagePopup;
    }
    private float dissapearTimer;
    private TextMeshPro textMesh;
    private Color textColor;

    private float maxDamageSizeDamage = 1000;
    private int maxDamageSize = 15;
    private int minDamageFontSize = 5;

    private Vector3 moveVector;

    private const float disappearTimerMax = 0.5f;


    private int CalculateDamagePopupFontSize(int damage, bool isCriticalHit)
    {
        float fontSize = minDamageFontSize += (int)(((float)(maxDamageSize - minDamageFontSize) / maxDamageSizeDamage) * damage);
        if (isCriticalHit) fontSize += (0.5f * fontSize);
        if(fontSize > maxDamageSize) fontSize = maxDamageSize;
        return (int)fontSize;
    }



    private void Awake()
    {
        textMesh = transform.GetComponent<TextMeshPro>();
    }
    public void Setup(int damageAmount, bool isCriticalHit, bool isTrueDamage)
    {
        
        textMesh.SetText(damageAmount.ToString());
        if (!isCriticalHit)
        {
            textMesh.fontSize = CalculateDamagePopupFontSize(damageAmount, isCriticalHit); ;
            if(isTrueDamage) textColor = GetColorFromString("0D89FF");
            else textColor = GetColorFromString("FFC500");

        }
        else
        {
            textMesh.fontSize = CalculateDamagePopupFontSize(damageAmount, isCriticalHit);
            if(isTrueDamage) textColor = GetColorFromString("003199");
            else textColor = GetColorFromString("FF2B00");

        }
        textMesh.color = textColor;
        dissapearTimer = disappearTimerMax;

        moveVector = new Vector3(0.7f, 1) * 3f;
    }
    public static int Hex_to_Dec(string hex)
    {
        return Convert.ToInt32(hex, 16);
    }
    public static float Hex_to_Dec01(string hex)
    {
        return Hex_to_Dec(hex) / 255f;
    }

    public static Color GetColorFromString(string color)
    {
        float red = Hex_to_Dec01(color.Substring(0, 2));
        float green = Hex_to_Dec01(color.Substring(2, 2));
        float blue = Hex_to_Dec01(color.Substring(4, 2));
        float alpha = 1f;
        if (color.Length >= 8)
        {
            // Color string contains alpha
            alpha = Hex_to_Dec01(color.Substring(6, 2));
        }
        return new Color(red, green, blue, alpha);
    }

    private void Update()
    {
        transform.position += moveVector * Time.deltaTime;
        moveVector -= moveVector * 0.8f * Time.deltaTime;


        if(dissapearTimer > disappearTimerMax * 0.5f)
        {
            float decreaseScaleAmount = 1f;
            transform.localScale += Vector3.one * decreaseScaleAmount * Time.deltaTime;
        }
        else
        {
            float decreaseScaleAmount = 1f;
            transform.localScale -= Vector3.one * decreaseScaleAmount * Time.deltaTime;
        }
        dissapearTimer -= Time.deltaTime;
        if (dissapearTimer < 0)
        {
            float disappearSpeed = 3f;
            textColor.a -= disappearSpeed * Time.deltaTime;
            textMesh.color = textColor;
            if (textColor.a < 0)
            {
                Destroy(gameObject);
            }
        }
    }
}

