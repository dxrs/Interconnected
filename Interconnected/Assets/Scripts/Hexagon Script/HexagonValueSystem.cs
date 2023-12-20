using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HexagonValueSystem : MonoBehaviour
{
    public int hexaValueP1;
    public int hexaValueP2;

    public Image[] hexaImageP1;
    public Image[] hexaImageP2;

    [SerializeField] Color hexaPickColorP1;
    [SerializeField] Color hexaPickColorP2;

    [SerializeField] int totalHexaValue;

    [SerializeField] TextMeshProUGUI textValueHexa;

    private void Update()
    {
        totalHexaValue = hexaValueP1 + hexaValueP2;
        textValueHexa.text = totalHexaValue.ToString();

        for (int i = 0; i < hexaImageP1.Length; i++)
        {
            if (hexaValueP1 == i + 1)
            {
                hexaImageP1[i].color = hexaPickColorP1;
            }
        }

        for(int j = 0; j < hexaImageP2.Length; j++) 
        {
            if (hexaValueP2 == j + 1) 
            {
                hexaImageP2[j].color = hexaPickColorP2;
            }
        }
    }
}
