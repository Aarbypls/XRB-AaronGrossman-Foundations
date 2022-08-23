using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberReader : MonoBehaviour
{
    [SerializeField] private float _numberTimer = 2f;
    [SerializeField] private float _time;
    [SerializeField] private GameObject _safeDoor;
    [SerializeField] private GameObject _safeCrackingBounds;
    [SerializeField] private int _selectedSafeNumber = int.MinValue;
    [SerializeField] private int _safeCode1 = 7;
    [SerializeField] private int _safeCode2 = 5;
    [SerializeField] private int _safeCode3 = 11;
    [SerializeField] private List<int> _inputs;
    
    private JointLimits _jointLimits;
    private bool _startNumberTimer = false;

    private void Start()
    {
        _inputs = new List<int>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("DialNumber"))
        {
            // Debug.Log("yeah");

            _selectedSafeNumber = Int32.Parse(collision.gameObject.name);
            
            if (_selectedSafeNumber == _safeCode1 || _selectedSafeNumber == _safeCode2 || _selectedSafeNumber == _safeCode3)
            {
                _startNumberTimer = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _time = 0;
    }

    private void Update()
    {
        if (CheckSafeInput())
        {
            OpenSafe();
        }

        if (_startNumberTimer)
        {
            _time += Time.deltaTime;

            if (_time >= _numberTimer)
            {
                if (_selectedSafeNumber != _safeCode1 && _selectedSafeNumber != _safeCode2 && _selectedSafeNumber != _safeCode3)
                {
                    _startNumberTimer = false;
                    _inputs.Clear();
                }
                else
                {
                    // Debug.Log("Adding " + _selectedSafeNumber);
                    _inputs.Add(_selectedSafeNumber);
                }
            }
        }
    }

    private bool CheckSafeInput()
    {
        if (_inputs.Contains(7) && _inputs.Contains(5) && _inputs.Contains(11))
        {
            // Debug.Log("SafeOpened");

            return true;

            /*if (_inputs == null)
            {
                return false;
            }
    
            if (_inputs.Count != 3)
            {
                return false;
            }
    
            if (_inputs[0] != _safeCode1)
            {
                Debug.Log("Got 1");
                return false;
            }
            
            if (_inputs[1] != _safeCode2)
            {
                Debug.Log("Got 1");
                return false;
            }
            
            if (_inputs[2] != _safeCode3)
            {
                Debug.Log("Got 1");
                return false;
            }
    
            return true;*/
        }

        return false;
    }

    private void OpenSafe()
    {
        HingeJoint safeHingeJoint = _safeDoor.GetComponent<HingeJoint>();
        JointLimits limits = safeHingeJoint.limits;
        limits.max = 90f;
        safeHingeJoint.limits = limits;
        
        _safeCrackingBounds.SetActive(false);
    }
}
