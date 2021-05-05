
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class XpPopup : MonoBehaviour
{


    public static XpPopup Create(Vector3 position, int xpAmount)
    {
        Transform xpPopupTransform = Instantiate(GameAssets.i.pfXpPopup, (Vector2)position + new Vector2(0, 1), Quaternion.identity);
        XpPopup xpPopup = xpPopupTransform.GetComponent<XpPopup>();
        xpPopup.Setup(xpAmount);
        return xpPopup;
    }
    private float dissapearTimer;
    private TextMeshPro textMesh;
    private Color textColor;

    private Vector3 moveVector;

    private const float disappearTimerMax = 0.5f;



    private void Awake()
    {
        textMesh = transform.GetComponent<TextMeshPro>();
    }
    public void Setup(int xpAmount)
    {

        textMesh.SetText(xpAmount.ToString() + " XP");
        dissapearTimer = disappearTimerMax;

        moveVector = new Vector3(0, 1) * 3f;
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


        if (dissapearTimer > disappearTimerMax * 0.5f)
        {
            float decreaseScaleAmount = 0.5f;
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


