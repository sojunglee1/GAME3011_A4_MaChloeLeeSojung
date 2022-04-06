using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    Button button;
    public bool selected = false;
    public bool blocked = false;
    public bool critical = false;

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
    }

    public void Toggle()
    {
        if (!blocked)
        {
            selected = !selected;

            if (selected)
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
        GetComponent<Image>().color = Color.cyan;
    }
}
