using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public Player(string name, Color fontColor, bool isPlayerOne)
    {
        this.fontColor = fontColor;
        this.isPlayerOne = isPlayerOne;
        if (name == "" || name == null)
        {
            this.name = isPlayerOne ? "One" : "Two";
        }
        else
        {
            this.name = name;
        }
        fontSize = 35;
        for (int i = 4; i < name.Length && i < 7; i++)
        {
            fontSize += -(10 - i);
        }
    }

    private string name;
    private int fontSize = 25;
    private Color fontColor = Color.white;
    private bool isPlayerOne;

    public string Name
    {
        get
        {
            return name;
        }
    }

    public int FontSize
    {
        get
        {
            return fontSize;
        }
    }

    public Color FontColor
    {
        get
        {
            return fontColor;
        }
    }

    public bool IsPlayerOne
    {
        get
        {
            return isPlayerOne;
        }
    }

    public int PlayerNumber
    {
        get
        {
            return isPlayerOne ? 0 : 1;
        }
    }
}
