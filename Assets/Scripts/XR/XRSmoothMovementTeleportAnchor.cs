using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.XR.Interaction.Toolkit;

public class XRSmoothMovementTeleportAnchor : TeleportationAnchor
{
    [SerializeField] private GameObject _xrRig;
    [SerializeField] private Volume _volume;

    private float _speed = 10f;
    private bool _teleporting;
    private Vector3 _fromLocation;
    private Vector3 _toLocation;
    
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        _fromLocation = _xrRig.transform.position;
        _toLocation = transform.position;
        _teleporting = true;
        _volume.weight = 1;
    }

    private void Update()
    {
        if (!_teleporting)
        {
            return;
        }

        if (Vector3.Distance(_toLocation, _xrRig.transform.position) > 1f)
        {
            _xrRig.transform.position = Vector3.Lerp(_xrRig.transform.position, transform.position, Time.deltaTime * _speed);
        }
        else
        {
            _xrRig.transform.position = transform.position;
            _teleporting = false;
            _volume.weight = 0;
        }
    }
}
