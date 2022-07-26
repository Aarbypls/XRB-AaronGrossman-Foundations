using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.TryGetComponent(out Flammable flammable))
        {
            return;
        }

        flammable.SetOnFire();
    }
}
