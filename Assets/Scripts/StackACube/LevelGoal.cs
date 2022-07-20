using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGoal : MonoBehaviour
{
    public bool won = false;
    
    [SerializeField] private int _goalLevel;
    [SerializeField] private CubeSummoner _cubeSummoner;
    [SerializeField] private GameObject _star;

    private float _goalTimer = 0f;
    private bool _inGoal = false;
    private GameObject _cubeInGoal = null;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("StackCube"))
        {
            _cubeInGoal = other.gameObject;
            _inGoal = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("StackCube") && _cubeInGoal != null && other.gameObject == _cubeInGoal)
        {
            _inGoal = false;
            _cubeInGoal = null;
        }
    }

    private void Update()
    {
        if (_inGoal && !won)
        {
            if (_goalTimer > 5f)
            {
                // Debug.Log("You finished the Level " + _goalLevel + " Cube Stacking Challenge!");
                won = true;
                _star.SetActive(true);
            }
            else
            {
                _goalTimer += Time.deltaTime;
            }
        }
        else
        {
            _goalTimer = 0f;
        }
    }

    public void Reset()
    {
        won = false;
        _goalTimer = 0f;
        _inGoal = false;
        _cubeInGoal = null;
        _star.SetActive(false);
    }

    public bool WonGame()
    {
        return won && (_goalLevel == 3);
    }
}
