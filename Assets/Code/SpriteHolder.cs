using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class SpriteHolder : MonoBehaviour
{
    private void Start()
    {
        tiles = new();
        tiles.Add(TileType.Water, new Tile[][] { waterTilesYellow, waterTilesViolet });
        tiles.Add(TileType.Fire, new Tile[][] { fireTilesYellow, fireTilesViolet });
        tiles.Add(TileType.Nature, new Tile[][] { natureTilesYellow, natureTilesViolet });
    }

    public Tile GetTileSprite(TileData tileData)
    {
        foreach (TileType tileType in new TileType[] { TileType.Fire, TileType.Water, TileType.Nature })
        {
            if (tileData.Type == tileType)
            {
                int value = tileData.Value;
                if (value < 0)
                {
                    value = 0;
                }
                else if (value > 5)
                {
                    value = 5;
                }
                return tiles[tileType][tileData.Owner.PlayerNumber][value];
            }
        }
        return wastelandTiles[0];
    }

    [SerializeField]
    private Tile[] waterTilesYellow;
    [SerializeField]
    private Tile[] fireTilesYellow;
    [SerializeField]
    private Tile[] natureTilesYellow;
    [SerializeField]
    private Tile[] waterTilesViolet;
    [SerializeField]
    private Tile[] fireTilesViolet;
    [SerializeField]
    private Tile[] natureTilesViolet;
    [SerializeField]
    private Tile[] wastelandTiles;

    private Dictionary<TileType, Tile[][]> tiles;
}
