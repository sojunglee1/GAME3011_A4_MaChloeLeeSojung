using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text timerText;
    public int timer;

    void Update()
    {
        timer = (int)GetComponent<GameManager>().timer;
        timerText.text = $"Countdown:\n{timer.ToString()} seconds";
    }
}
