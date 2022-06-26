using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisTrigger : MonoBehaviour
{
    [SerializeField] private GameObject debris;
    private bool drop = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<DebrisInteractor>())
        {
            drop = true;
            StartCoroutine(DropDebris());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<DebrisInteractor>())
        {
            drop = false;
        }
    }

    private IEnumerator DropDebris()
    {
        yield return new WaitForSeconds(3);

        if (drop)
        {
            debris.SetActive(true);
        }
    }
}
