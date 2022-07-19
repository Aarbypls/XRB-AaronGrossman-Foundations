using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCounter : MonoBehaviour
{
    [SerializeField] private CanCounter _canCounter;
    public int count = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<CarnivalBall>())
        {
            count++;
        }

        if (count >= 3)
        {
            count = 0;

            if (_canCounter.startReset == false)
            {
                _canCounter.StartReset();
            }
        }
    }
}
