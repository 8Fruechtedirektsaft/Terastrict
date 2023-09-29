using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ValueTextMapUI : MonoBehaviour
{
    private void Start()
    {
        valueMap.Add(new Vector3Int(1, 0, 0), values[0]);
        valueMap.Add(new Vector3Int(0, 1, 0), values[1]);
        valueMap.Add(new Vector3Int(-1, 1, 0), values[2]);
        valueMap.Add(new Vector3Int(-1, 0, 0), values[3]);
        valueMap.Add(new Vector3Int(-1, -1, 0), values[4]);
        valueMap.Add(new Vector3Int(0, -1, 0), values[5]);

        valueMap.Add(new Vector3Int(2, 0, 0), values[6]);
        valueMap.Add(new Vector3Int(1, 1, 0), values[7]);
        valueMap.Add(new Vector3Int(1, 2, 0), values[8]);
        valueMap.Add(new Vector3Int(0, 2, 0), values[9]);
        valueMap.Add(new Vector3Int(-1, 2, 0), values[10]);
        valueMap.Add(new Vector3Int(-2, 1, 0), values[11]);
        valueMap.Add(new Vector3Int(-2, 0, 0), values[12]);
        valueMap.Add(new Vector3Int(-2, -1, 0), values[13]);
        valueMap.Add(new Vector3Int(-1, -2, 0), values[14]);
        valueMap.Add(new Vector3Int(0, -2, 0), values[15]);
        valueMap.Add(new Vector3Int(1, -2, 0), values[16]);
        valueMap.Add(new Vector3Int(1, -1, 0), values[17]);
    }

    public void SetValue(Vector3Int key, TileData tileData)
    {     
        if (tileData.Value >= 0)
        {
            valueMap[key].text = tileData.Value.ToString();
        }
        else
        {
            valueMap[key].text = "(" + tileData.Value.ToString() + ")";
        }
        valueMap[key].color = tileData.Owner.FontColor;
    }

    [SerializeField]
    private Text[] values;
    private Dictionary<Vector3Int, Text> valueMap = new Dictionary<Vector3Int, Text>();
}
