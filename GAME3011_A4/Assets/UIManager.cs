using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    public GameManager gm;

    public Text timerText;
    public int timer;

    public Text playerSkillText;
    public Text difficultyText;

    public Text Prompt;

    public Button MainMenuButton;

    private void Start()
    {
        gm = GetComponent<GameManager>();
    }

    void Update()
    {
        timer = (int)gm.timer;
        timerText.text = $"Countdown:\n{timer.ToString()} seconds";

        playerSkillText.text = $"Player Skill:\n{gm.playerSkill.ToString()}";
        difficultyText.text = $"Hack Level:\n{gm.difficultylvl.ToString()}";

        switch(gm.gamestatus)
        {
            case GameStatus.won:
                Prompt.text = "Game Won!";
                Time.timeScale = 0;
                MainMenuButton.gameObject.SetActive(true);
                break;

            case GameStatus.lost:
                Prompt.text = "Game Lost!";
                Time.timeScale = 0;
                MainMenuButton.gameObject.SetActive(true);
                break;

            case GameStatus.ingame:
                Prompt.text = "Goal:\nTry to click on three 'safe' consecutive tiles!";
                Time.timeScale = 1;
                MainMenuButton.gameObject.SetActive(false);
                break;
        }
    }
}
