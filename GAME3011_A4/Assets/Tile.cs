using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    GameManager gm;
    Button button;
    public bool selected = false;       //user clicks on button
    public bool blocked = false;        //this tile is blocked (red)
    public bool critical = false;       //this tile is critical (dark blue)
                                            //if this tile is clicked on, there's a chance for game over

    public bool exposed = false;        //checks if the critical tile is exposed
                                        //exposure depends on player's skill

    private Ray rightRay;

    private void Start()
    {
        button = GetComponent<Button>();
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        if (blocked)
        {
            selected = false;
            GetComponent<Image>().color = Color.red;   
        }
        else if (exposed)
        {
            GetComponent<Image>().color = Color.blue;
        }
    }

    public void Select()
    {
        if (gm.gamestatus != GameStatus.won || gm.gamestatus != GameStatus.lost || !blocked)
        {
            selected = true;
            if (selected)
            {
                if (critical)
                {
                    GetComponent<Image>().color = Color.blue;
                    gm.gamestatus = GameStatus.lost;
                }
                else
                {
                    GetComponent<Image>().color = Color.green;
                }
            }
            else
            {
                GetComponent<Image>().color = Color.cyan;
            }

            gm.ExposeNextTile(this);
            gm.CheckTiles();
        }

    }

    public void ResetTile()
    {
        blocked = false;
        selected = false;
        critical = false;
        exposed = false;
        GetComponent<Image>().color = Color.cyan;
    }

}
