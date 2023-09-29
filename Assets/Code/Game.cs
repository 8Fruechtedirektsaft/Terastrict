using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    void Start()
    {
        tileChoicesPlayerTwo.gameObject.SetActive(false);
        tileChoicesPlayerOne.gameObject.SetActive(false);
        gameOver.SetActive(false);
        names.SetActive(false);
        options.SetActive(false);
        playerOne = new Player("", new Color(172f / 255f, 156f / 255f, 29f / 255f), true);
        playerTwo = new Player("", new Color(123f / 255f, 0f / 255f, 101f / 255f), false);
        state = States.Menue;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && (state == States.TileChosen))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            clickLocation = tilemap.WorldToCell(mousePosition);
            StartCoroutine(PlaceTile(clickLocation, tileToPlaceData));
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            OptionsButton();
        }
    }

    private IEnumerator PlaceTile(Vector3Int location, TileData tileData)
    {
        if (LocationWithinBounds(location))
        {
            if (!dataMap.ContainsKey(location))
            { 
                dataMap.Add(location, tileData);
                valueMap.SetValue(location, tileData);
                state = States.TilePlaced;
                tilemap.SetTile(location, null);
                playTypeSound(tileData.Type);
                UpdateTiles(location);
                int scorePlayerOne = CountTileSum(playerOne);
                int scorePlayerTwo = CountTileSum(playerTwo);
                score.SetScore(scorePlayerOne, scorePlayerTwo);
                yield return new WaitForSeconds(0.6f);

                if (dataMap.Count == 18)
                {
                    state = States.Menue;
                    gameOver.SetActive(true);
                    winSound.Play();
                    if (scorePlayerOne > scorePlayerTwo)
                    {
                        playerText.SetWinMessage(playerOne);
                    }
                    else if (scorePlayerOne < scorePlayerTwo)
                    {
                        playerText.SetWinMessage(playerTwo);
                    }
                    else
                    {
                        playerText.SetDrawMessage();
                    }                
                }
                else
                {
                    state = States.TurnStart;
                    TurnSwap();
                }              
            }
        }
    }

    private bool LocationWithinBounds(Vector3Int location)
    {
        int q = location.y;
        int r = location.x - (location.y - (Mathf.Abs(location.y) % 2)) / 2;
        Vector3Int cube = new(q, r, -q - r);
        int distance = (Mathf.Abs(cube.x) + Mathf.Abs(cube.y) + Mathf.Abs(cube.z)) / 2;
        if (distance == 0)
        {
            return false;
        }
        return distance < 3 ? true : false;
    }

    private void UpdateTiles(Vector3Int location)
    {
        int x = location.x;
        int y = location.y;
        int z = location.z;
        if (Mathf.Abs(y) % 2 == 1)
        {
            CompareTiles(location, new Vector3Int(x + 1, y + 1, z));
            CompareTiles(location, new Vector3Int(x + 1, y - 1, z));
        }
        else
        {
            CompareTiles(location, new Vector3Int(x - 1, y - 1, z));
            CompareTiles(location, new Vector3Int(x - 1, y + 1, z));
        }
        CompareTiles(location, new Vector3Int(x + 1, y, z));
        CompareTiles(location, new Vector3Int(x, y + 1, z));
        CompareTiles(location, new Vector3Int(x - 1, y, z));
        CompareTiles(location, new Vector3Int(x, y - 1, z));

        tilemap.SetTile(location, sprites.GetTileSprite(dataMap[location]));
    }

    private void CompareTiles(Vector3Int center, Vector3Int outer)
    {
        if (dataMap.ContainsKey(outer))
        {
            TileType outerType = dataMap[outer].Type;
            TileType centerType = dataMap[center].Type;

            if (outerType == centerType)
            {
                dataMap[center].UpdateValue(1);
                dataMap[outer].UpdateValue(1);
                valueMap.SetValue(center, dataMap[center]);
                valueMap.SetValue(outer, dataMap[outer]);
            }
            else
            {
                if ((centerType == TileType.Fire && outerType == TileType.Water) || (centerType == TileType.Nature && outerType == TileType.Fire) || (centerType == TileType.Water && outerType == TileType.Nature))
                {
                    dataMap[center].UpdateValue(-1);
                    valueMap.SetValue(center, dataMap[center]);
                }
                else
                {
                    dataMap[outer].UpdateValue(-1);
                    valueMap.SetValue(outer, dataMap[outer]);
                }
            }
            tilemap.SetTile(outer, sprites.GetTileSprite(dataMap[outer]));
        }
    }
    
    private void TurnSwap()
    {
        tilemap.SetTile(new Vector3Int(0, 0, 0), null);
        activePlayer = activePlayer.IsPlayerOne ? playerTwo : playerOne;
        playerText.SetPlayerText(activePlayer);
        if (activePlayer.IsPlayerOne)
        {
            tileChoicesPlayerTwo.LockButtons();
            tileChoicesPlayerTwo.gameObject.SetActive(false);
            tileChoicesPlayerOne.gameObject.SetActive(true);
        }
        else
        {
            tileChoicesPlayerOne.LockButtons();
            tileChoicesPlayerTwo.gameObject.SetActive(true);
            tileChoicesPlayerOne.gameObject.SetActive(false);
        }
    }

    private int CountTileSum(Player owner)
    {
        int sum = 0;
        foreach (KeyValuePair<Vector3Int, TileData> entry in dataMap)
        {
            if (entry.Value.Owner.IsPlayerOne == owner.IsPlayerOne && entry.Value.Value > 0)
            {
                sum += entry.Value.Value;
            }
        }
        return sum;
    }

    public void FireButton(int value)
    {
        clickSound.Play();
        if (state == States.TurnStart || state == States.TileChosen)
        {
            tileToPlaceData = new TileData(TileType.Fire, value, activePlayer);
            state = States.TileChosen;
        }
    }

    public void NatureButton(int value)
    {
        clickSound.Play();
        if (state == States.TurnStart || state == States.TileChosen)
        {
            tileToPlaceData = new TileData(TileType.Nature, value, activePlayer);
            state = States.TileChosen;
        }
    }

    public void WaterButton(int value)
    {
        clickSound.Play();
        if (state == States.TurnStart || state == States.TileChosen)
        {
            tileToPlaceData = new TileData(TileType.Water, value, activePlayer);
            state = States.TileChosen;
        }
    }

    public void OKButton()
    {
        clickSound.Play();
        summary.SetActive(false);
        names.SetActive(true);
    }

    public void PlayerNameOneEndEdit(string name)
    {
        playerOne = new Player(name, new Color(172f / 255f, 156f / 255f, 29f / 255f), true);
    }

    public void PlayerNameTwoEndEdit(string name)
    {
        playerTwo = new Player(name, new Color(123f / 255f, 0f / 255f, 101f / 255f), false);
    }

    public void StartGameButton()
    {
        clickSound.Play();
        activePlayer = playerOne;
        playerText.SetPlayerText(activePlayer);
        intro.SetActive(false);
        state = States.TurnStart;
        tileChoicesPlayerTwo.gameObject.SetActive(false);
        tileChoicesPlayerOne.gameObject.SetActive(true);
    }

    public void NewGameButton()
    {
        clickSound.Play();
        SceneManager.LoadScene("SampleScene");
    }

    public void QuitGameButton()
    {
        clickSound.Play();
        Application.Quit();
    }

    public void GiveUpButton()
    {
        clickSound.Play();
        state = States.Menue;
        gameOver.SetActive(true);
        winSound.Play();
        if (activePlayer.IsPlayerOne)
        {
            playerText.SetWinMessage(playerTwo);
        }
        else
        {
            playerText.SetWinMessage(playerOne);
        }
    }

    public void OptionsButton()
    {
        clickSound.Play();
        options.SetActive(!options.activeSelf);
        state = options.activeSelf ? States.Menue : States.TurnStart;
    }

    public void ValuesButton()
    {
        clickSound.Play();
        valueMap.gameObject.SetActive(!valueMap.gameObject.activeSelf);
    }

    public void MusicSlider(float volume)
    {
        music.volume = volume;
    }

    public void EffectSlider(float volume)
    {
        clickSound.volume = volume;
        waterSound.volume = volume;
        fireSound.volume = volume;
        natureSound.volume = volume;
        winSound.volume = volume;
    }

    private void playTypeSound(TileType type)
    {
        switch (type)
        {
            case TileType.Fire:
                fireSound.Play();
                break;
            case TileType.Nature:
                natureSound.Play();
                break;
            case TileType.Water:
                waterSound.Play();
                break;
        }
    }

    private States state;
    private Player playerOne;
    private Player playerTwo;
    private Player activePlayer;
    private Dictionary<Vector3Int, TileData> dataMap = new Dictionary<Vector3Int, TileData>();
    private Vector3Int clickLocation;
    private TileData tileToPlaceData;
    [SerializeField]
    private Tilemap tilemap;
    [SerializeField]
    private SpriteHolder sprites;
    [SerializeField]
    private TileChoicesUI tileChoicesPlayerOne;
    [SerializeField]
    private TileChoicesUI tileChoicesPlayerTwo;
    [SerializeField]
    private ScoreUI score;
    [SerializeField]
    private PlayerTextUI playerText;
    [SerializeField]
    private ValueTextMapUI valueMap;
    [SerializeField]
    private GameObject intro;
    [SerializeField]
    private GameObject summary;
    [SerializeField]
    private GameObject names;
    [SerializeField]
    private GameObject gameOver;
    [SerializeField]
    private GameObject options;
    [SerializeField]
    private AudioSource clickSound;
    [SerializeField]
    private AudioSource waterSound;
    [SerializeField]
    private AudioSource fireSound;
    [SerializeField]
    private AudioSource natureSound;
    [SerializeField]
    private AudioSource music;
    [SerializeField]
    private AudioSource winSound;
}

public enum States { Menue, TurnStart,  TileChosen, TilePlaced}


