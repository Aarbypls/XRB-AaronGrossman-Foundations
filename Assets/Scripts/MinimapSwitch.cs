using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapSwitch : MonoBehaviour
{
    [SerializeField] private Camera _minimapCamera;
    [SerializeField] private LayerMask _waterMask;
    [SerializeField] private LayerMask _normalMask;
    
    private bool _waterView = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != 10) // Hand)
        {
            return;
        }
        
        if (_waterView)
        {
            _waterView = false;
            _minimapCamera.cullingMask = _normalMask;
        }
        else
        {
            _waterView = true;
            _minimapCamera.cullingMask = _waterMask;
        }
    }
}
