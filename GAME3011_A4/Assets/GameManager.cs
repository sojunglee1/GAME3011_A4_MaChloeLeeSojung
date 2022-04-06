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
    public DifficultyLevel difficultylvl;
    public int blockedTile;
    public List<Tile> buttons;

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
        SetDifficultyLevel();
        ResetBlockedTiles();
    }

    void ResetBlockedTiles()
    {
        int index = Random.Range(0, buttons.Count - 1);
        print(index);
        for (int i = 0; i < blockedTile; i++)
        {
            if (!buttons[index].blocked)
            {
                buttons[index].blocked = true;
            }
            else
            {
                if (index < buttons.Count) buttons[index++].blocked = true;
                else buttons[index--].blocked = true;
            }
        }
    }

    void SetDifficultyLevel()
    {
        switch (difficultylvl)
        {
            case DifficultyLevel.Easy:
                blockedTile = 1;
                break;

            case DifficultyLevel.Medium:
                blockedTile = 2;
                break;

            case DifficultyLevel.Hard:
                blockedTile = 3;
                break;

            default:
                blockedTile = 0;
                break;
        }
    }
}
