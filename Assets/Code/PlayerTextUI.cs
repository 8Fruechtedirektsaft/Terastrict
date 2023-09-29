using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTextUI : MonoBehaviour
{
    public void SetPlayerText(Player player)
    {
        showName.text = player.Name;
        showName.fontSize = player.FontSize;
        showName.color = player.FontColor;
    }

    public void SetWinMessage(Player player)
    {
        winMessage.text = player.Name + " has won!";
    }

    public void SetDrawMessage()
    {
        winMessage.text = "Both Players played equally well!";
    }

    [SerializeField]
    private Text showName;
    [SerializeField]
    private Text winMessage;
}
