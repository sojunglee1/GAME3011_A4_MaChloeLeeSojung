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

public class GameManager : MonoBehaviour
{
    public float timer;
    public DifficultyLevel difficultylvl;
    public int blockedTile;
    public int ICETile;
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
        ResetBlockedTiles();
        ResetICETiles();
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

    void SetDifficultyLevel()
    {
        switch (difficultylvl)
        {
            case DifficultyLevel.Easy:
                timer = 60;
                blockedTile = 1;
                ICETile = 0;
                break;

            case DifficultyLevel.Medium:
                timer = 30;
                blockedTile = 2;
                ICETile = 1;
                break;

            case DifficultyLevel.Hard:
                timer = 15;
                blockedTile = 3;
                ICETile = 2;
                break;

            default:
                timer = 30;
                blockedTile = 0;
                ICETile = 0;
                break;
        }
    }
}
