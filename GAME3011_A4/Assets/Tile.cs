using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Tile : MonoBehaviour
{
    Button button;
    public bool selected = false;
    public bool blocked = false;

    private void Start()
    {
        button = GetComponent<Button>();

    }

    private void Update()
    {
        if (blocked)
        {
            GetComponent<Image>().color = Color.red;
        }
    }

    public void Toggle()
    {
        selected = !selected;

        if (selected)
        {
            GetComponent<Image>().color = Color.green;
        }
    }

    public void ResetTile()
    {
        blocked = false;
        selected = false;

        GetComponent<Image>().color = Color.cyan;
    }
}
