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

    public GameStatus gamestatus;
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
    public void Start()
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
        switch (difficultylvl)
        {
            case DifficultyLevel.Easy:
                timer = 60;
                blockedTile = 0;
                criticalTile = 1;
                break;

            case DifficultyLevel.Medium:
                timer = 30;
                blockedTile = 1;
                criticalTile = 2;
                break;

            case DifficultyLevel.Hard:
                timer = 15;
                blockedTile = 2;
                criticalTile = 3;
                break;

            default:
                timer = 30;
                blockedTile = 0;
                criticalTile = 0;
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

    public void ExposeTile(Tile currentbutton)
    {
        int index = buttons.IndexOf(currentbutton);
        buttons[index++].selected = true;
        buttons[index++].GetComponent<Image>().color = Color.green;
    }

    public GameObject tile1, tile2;

    public void CheckTiles(Tile button)
    {
        Ray2D ray = new Ray2D(button.transform.position, Vector2.right * 250);
        var hitData = Physics2D.Raycast(ray.origin, Vector2.right * 250);

        if (hitData.collider != null)
        {
            print("hit");
            print(hitData.collider.name);
        }

        //foreach (Tile button in buttons)
        //{
        //    var leftData = Physics2D.Raycast(button.transform.position, Vector2.left);
        //    var rightData = Physics2D.Raycast(button.transform.position, Vector2.right);
        //    var upData = Physics2D.Raycast(button.transform.position, Vector2.up);
        //    var downData = Physics2D.Raycast(button.transform.position, Vector2.down);

        //    if (button.selected)
        //    {
        //        print(button.name);
        //        while (leftData.collider != null && rightData.collider != null)
        //        {
        //            print(leftData.collider.gameObject.name);
        //            print(rightData.collider.gameObject.name);

        //            if (leftData.collider.gameObject.GetComponent<Tile>().selected &&
        //                rightData.collider.gameObject.GetComponent<Tile>().selected)
        //            {
        //                gamestatus = GameStatus.won;
        //            }
        //        }

        //        while (upData.collider != null && downData.collider != null)
        //        {
        //            print(upData.collider.gameObject.name);
        //            print(downData.collider.gameObject.name);

        //            if (upData.collider.gameObject.GetComponent<Tile>().selected &&
        //                downData.collider.gameObject.GetComponent<Tile>().selected)
        //            {
        //                gamestatus = GameStatus.won;
        //            }
        //        }
        //    }
        //}
    }

    private void OnDrawGizmos()
    {
        
    }
}
