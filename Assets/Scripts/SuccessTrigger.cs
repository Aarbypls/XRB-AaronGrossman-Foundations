using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuccessTrigger : MonoBehaviour
{
    [SerializeField] private GameObject successCanvas;
    [SerializeField] private GameObject successParticles;
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<SuccessInteractor>())
        {
            successCanvas.SetActive(true);
            successParticles.SetActive(true);
        }
    }
}
