using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField] private GameObject _door;

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<DoorInteractor>())
        {
            OpenDoor();
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<DoorInteractor>())
        {
            CloseDoor();
        }
    }

    protected virtual void OpenDoor()
    {
        _door.SetActive(false);
    }

    protected virtual void CloseDoor()
    {
        _door.SetActive(true);
    }
}
