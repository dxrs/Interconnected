using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InGame : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textMainObjective;
    [SerializeField] TextMeshProUGUI textMaxGarbageCollected;
    [SerializeField] TextMeshProUGUI textPlayerSpeed;

    private void Update()
    {
        textMainObjective.text = "Sampah yang di bawa " + GarbageCollector.garbageCollector.garbageCollected + ", Terkumpul " + GarbageCollector.garbageCollector.currentGarbageStored;
        textMaxGarbageCollected.text = "Sampah yang harus dikumpulkan " + GarbageCollector.garbageCollector.targetGarbageStored;
        textPlayerSpeed.text = "Player Power Speed " + Player1Movement.player1Movement.curMaxSpeed;
    }
}
