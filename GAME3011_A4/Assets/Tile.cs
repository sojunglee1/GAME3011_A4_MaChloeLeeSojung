using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    Button button;
    public bool selected = false;       //user clicks on button
    public bool blocked = false;        //this tile is blocked (red)
    public bool critical = false;       //this tile is critical (dark blue)
                                            //if this tile is clicked on, there's a chance for game over

    public bool exposed = false;        //checks if the critical tile is exposed
                                            //exposure depends on player's skill

    private void Start()
    {
        button = GetComponent<Button>();
    }

    private void Update()
    {
        if (blocked)
        {
            GetComponent<Image>().color = Color.red;
            selected = false;
        }
        else if (exposed)
        {
            GetComponent<Image>().color = Color.blue;
        }
    }

    public void Toggle()
    {
        if (!blocked)
        {
            selected = !selected;

            if (selected && !exposed)
            {
                if (!critical) GetComponent<Image>().color = Color.green;
                else GetComponent<Image>().color = Color.blue;
            }
            else
            {
                GetComponent<Image>().color = Color.cyan;
            }
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
