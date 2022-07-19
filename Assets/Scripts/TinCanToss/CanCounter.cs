using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanCounter : MonoBehaviour
{
    [SerializeField] private List<CarnivalBall> _balls;
    [SerializeField] private List<CarnivalCan> _cans;
    [SerializeField] private BallCounter _ballCounter;

    public int _count = 0;
    private float _resetCounter = 0f;
    public bool startReset = false;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<CarnivalCan>())
        {
            _count++;
        }

        if (_count >= 14)
        {
            StartReset();
        }
    }

    public void StartReset()
    {
        _resetCounter = 0f;
        _count = 0;
        startReset = true;
    }

    private void Update()
    {
        if (startReset == false)
        {
            return;
        }
        
        if (_resetCounter < 3.0f)
        {
            _resetCounter += Time.deltaTime;
        }
        else
        {
            startReset = false;
            _resetCounter = 0f;
            ResetBalls();
            ResetCans();
        }
    }

    private void ResetCans()
    {
        foreach (var can in _cans)
        {
            can.Reset();
        }

        _count = 0;
    }

    private void ResetBalls()
    {
        foreach (var ball in _balls)
        {
            ball.Reset();
        }

        _ballCounter.count = 0;
    }
}
