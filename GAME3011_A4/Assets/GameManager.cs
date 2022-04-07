using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameStatus
{
    won,
    lost,
    ingame
}

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

    public GameStatus gamestatus = GameStatus.ingame;
    public DifficultyLevel difficultylvl;
    public PlayerSkill playerSkill;
    public int blockedTile;
    public int criticalTile;
    public int exposedTile;
    public List<Tile> buttons;
    public List<Tile> freeButtons;

    private void Awake()
    {
        Random.seed = Random.Range(0, 100);
    }

    public void StartGame()
    {
        foreach (Tile button in buttons)
        {
            button.ResetTile();
        }

        freeButtons.Clear();
        freeButtons.AddRange(buttons);

        gamestatus = GameStatus.ingame;

        SetDifficultyLevel();
        SetPlayerLevel();
        ResetBlockedTiles();
        ResetCriticalTiles();
        ShowExposedTiles();
    }

    private void Update()
    {
        if (timer > 0) timer -= Time.deltaTime;
        if (gamestatus == GameStatus.ingame && timer <= 0) gamestatus = GameStatus.lost;

        if (gamestatus != GameStatus.ingame)
        {
            foreach (Tile button in buttons)
            {
                button.GetComponent<Button>().interactable = false;
            }
        }
        else
        {
            foreach (Tile button in buttons)
            {
                button.GetComponent<Button>().interactable = true;
            }
        }
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

    void ResetCriticalTiles()
    {
        for (int i = 0; i < criticalTile; i++)
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
        difficultylvl = (DifficultyLevel)Random.Range(0, (int)DifficultyLevel.Hard + 1);

        switch (difficultylvl)
        {
            case DifficultyLevel.Easy:
                blockedTile = 0;
                criticalTile = 1;
                break;

            case DifficultyLevel.Medium:
                blockedTile = 1;
                criticalTile = 2;
                break;

            case DifficultyLevel.Hard:
                blockedTile = 2;
                criticalTile = 3;
                break;

            default:
                blockedTile = 0;
                criticalTile = 0;
                break;
        }
    }

    void SetPlayerLevel()
    {
        playerSkill = (PlayerSkill)Random.Range(0, (int)PlayerSkill.Expert + 1);

        switch(playerSkill)
        {
            case PlayerSkill.New:
                timer = 5;
                exposedTile = 0;
                break;

            case PlayerSkill.Intermediate:
                timer = 10;
                exposedTile = 1;
                break;

            case PlayerSkill.Expert:
                exposedTile = 2;
                timer = 15;
                break;

            default:
                break;
        }
    }

    public void ExposeNextTile(Tile currentbutton)
    {
        int index = buttons.IndexOf(currentbutton) + 1;
        if (index < buttons.Count)
        {
            if (!buttons[index].blocked && !buttons[index].critical)
            {
                buttons[index].selected = true;
                buttons[index].GetComponent<Image>().color = Color.green;
            }
        }
    }

    public void CheckTiles()
    {
        foreach (Tile button in buttons)
        {
            if (CollidingTile(button, Vector2.right) != null && CollidingTile(button, Vector2.left) != null)
            {
                if (CheckSelectedTile(CollidingTile(button, Vector2.right)) &&
                    CheckSelectedTile(CollidingTile(button, Vector2.left)))
                {
                    gamestatus = GameStatus.won;
                }
            }

            if (CollidingTile(button, Vector2.up) != null && CollidingTile(button, Vector2.down) != null)
            {
                if (CheckSelectedTile(CollidingTile(button, Vector2.up)) &&
                    CheckSelectedTile(CollidingTile(button, Vector2.down)))
                {
                    gamestatus = GameStatus.won;
                }
            }

        }
    }

    private Tile CollidingTile(Tile button, Vector2 direction)
    {
        var hitData = Physics2D.Raycast(button.transform.position, direction, 150.0f);
        if (hitData.collider != null)
        {
            return hitData.collider.GetComponent<Tile>();
        }
        else return null;
    }

    private bool CheckSelectedTile(Tile button)
    {
        if (button.selected && !button.blocked && !button.critical)
        {
            return true;
        }
        else return false;
    }
}
