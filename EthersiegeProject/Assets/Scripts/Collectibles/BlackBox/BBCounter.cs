using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BBCounter : MonoBehaviour
{
    public GameObject loreText;
    public static int loreCollect;

    private TMP_Text m_TextComponent;

    private void Awake()
    {
        m_TextComponent = GetComponent<TMP_Text>();
    }

    void Update()
    {
        loreText.GetComponent<TMP_Text>().text = loreCollect + "/ 5 Black Box Collected";
    }

}
