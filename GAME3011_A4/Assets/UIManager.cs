using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text timerText;
    public int timer;

    public Text playerSkillText;
    public Text difficultyText;

    void Update()
    {
        timer = (int)GetComponent<GameManager>().timer;
        timerText.text = $"Countdown:\n{timer.ToString()} seconds";

        playerSkillText.text = $"Player Skill:\n{GetComponent<GameManager>().playerSkill.ToString()}";
        difficultyText.text = $"Hack Level:\n{GetComponent<GameManager>().difficultylvl.ToString()}";
    }
}
