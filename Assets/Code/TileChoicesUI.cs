using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileChoicesUI : MonoBehaviour
{
    public void LockButtons()
    {
        foreach (Toggle button in buttons)
        {
            if (button.isOn == true)
            {
                button.interactable = false;
            }
        }
    }

    [SerializeField]
    private Toggle[] buttons;
}
