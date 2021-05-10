using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class menuToolTip : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        transform.position = Input.mousePosition;
    }

    public void DisplayText(string message)
    {
        GetComponent<CanvasGroup>().alpha = 1;
        text.text = message;
    }
    public void HideToolTip()
    {
        GetComponent<CanvasGroup>().alpha = 0;
    }
}
