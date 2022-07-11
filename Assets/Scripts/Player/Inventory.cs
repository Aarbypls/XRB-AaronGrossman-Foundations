using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private GameObject _redKeyImage;
    [SerializeField] private GameObject _blueKeyImage;

    private bool _hasRedKey = false;
    private bool _hasBlueKey = false;

    public void UpdateKeys(Key.KeyColor keyColor)
    {
        if (keyColor == Key.KeyColor.red)
        {
            _hasRedKey = true;
            _redKeyImage.SetActive(true);
        }
        else if (keyColor == Key.KeyColor.blue)
        {
            _hasBlueKey = true;
            _blueKeyImage.SetActive(true);
        }
    }

    public bool HasRedKey()
    {
        return _hasRedKey;
    }

    public bool HasBlueKey()
    {
        return _hasBlueKey;
    }

    public bool HasKeyOfColor(Key.KeyColor keyColor)
    {
        if (keyColor == Key.KeyColor.red)
        {
            return HasRedKey();
        }
        else if (keyColor == Key.KeyColor.blue)
        {
            return HasBlueKey();
        }
        else
        {
            return false;
        }
    }
}
