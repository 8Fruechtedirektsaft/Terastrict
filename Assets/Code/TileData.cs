using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileData
{
    public TileData(TileType type, int value, Player owner)
    {
        this.type = type;
        this.value = value;
        this.owner = owner;
    }
    
    public void UpdateValue(int modifier)
    {
        value += modifier;
    }

    private TileType type;
    private int value;
    private Player owner;

    public TileType Type {
        get
        {
            return type;
        }
    }

    public int Value
    {
        get
        {
            return value;
        }
    }

    public Player Owner {
        get
        {
            return owner;
        }
    }
}
public enum TileType { Fire, Water, Nature, Wasteland}
