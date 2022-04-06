using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum DifficultyLevel
{
    Easy,
    Medium,
    Hard
}

public enum PlayerSkill
{
    New,
    Intermediate,
    Expert
}

public class GameManager : MonoBehaviour
{
    public float timer;

    public DifficultyLevel difficultylvl;
    public PlayerSkill playerSkill;
    public int blockedTile;
    public int ICETile;
    public int exposedTile;
    public List<Tile> buttons;
    public List<Tile> freeButtons;

    private void Awake()
    {
        Random.seed = Random.Range(0, 100);
    }
    public void Start()
    {
        foreach(Tile button in buttons)
        {
            button.ResetTile();
        }

        freeButtons.Clear();
        freeButtons.AddRange(buttons);

        SetDifficultyLevel();
        SetPlayerLevel();
        ResetBlockedTiles();
        ResetICETiles();
        ShowExposedTiles();
    }

    private void Update()
    {
        if (timer > 0) timer -= Time.deltaTime;
    }

    void ResetBlockedTiles()
    {
        for (int i = 0; i < blockedTile; i++)
        {
            int index = Random.Range(0, freeButtons.Count - 1);
            if (freeButtons.Contains(buttons[index]))
            {
                buttons[index].blocked = true;
                freeButtons.Remove(buttons[index]);
            }
            else i--;
        }
    }

    void ResetICETiles()
    {
        for (int i = 0; i < ICETile; i++)
        {
            int index = Random.Range(0, freeButtons.Count - 1);
            if (freeButtons.Contains(buttons[index]))
            {
                buttons[index].critical = true;
                freeButtons.Remove(buttons[index]);
            }
            else i--;
        }
    }

    void ShowExposedTiles()
    {
        for (int i = 0; i < exposedTile; i++)
        {
            foreach(Tile button in buttons)
            {
                if (button.critical && !button.exposed)
                {
                    button.exposed = true;
                    break;
                }
            }
        }
    }

    void SetDifficultyLevel()
    {
        switch (difficultylvl)
        {
            case DifficultyLevel.Easy:
                timer = 60;
                blockedTile = 0;
                ICETile = 1;
                break;

            case DifficultyLevel.Medium:
                timer = 30;
                blockedTile = 1;
                ICETile = 2;
                break;

            case DifficultyLevel.Hard:
                timer = 15;
                blockedTile = 2;
                ICETile = 3;
                break;

            default:
                timer = 30;
                blockedTile = 0;
                ICETile = 0;
                break;
        }
    }

    void SetPlayerLevel()
    {
        switch(playerSkill)
        {
            case PlayerSkill.New:
                exposedTile = 0;
                break;

            case PlayerSkill.Intermediate:
                exposedTile = 1;
                break;

            case PlayerSkill.Expert:
                exposedTile = 2;
                break;

            default:
                break;
        }
    }
}
