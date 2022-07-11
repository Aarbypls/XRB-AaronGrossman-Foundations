using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField] private GameObject _door;
    [SerializeField] private Key.KeyColor _doorColor;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<DoorInteractor>())
        {
            if (other.TryGetComponent(out Inventory inventory) && !inventory.HasKeyOfColor(_doorColor))
            {
                return;
            }
            
            _door.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<DoorInteractor>())
        {
            _door.SetActive(true);
        }
    }
}
