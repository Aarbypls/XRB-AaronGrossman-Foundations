using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public enum KeyColor
    {
        red = 0,
        blue = 1
    }

    public KeyColor keyColor;

    public void Update()
    {
        transform.Rotate(0, 1f, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player"))
        {
            return;
        }

        Inventory playerInventory = other.GetComponent<Inventory>();
        
        playerInventory.UpdateKeys(keyColor);
    }
}
