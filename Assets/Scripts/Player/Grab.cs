using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{
    [SerializeField] private Transform _cameraPosition;
    [SerializeField] private Transform _holdPosition;
    [SerializeField] private float _grabRange = 2f;
    [SerializeField] private float _throwForce = 20f;
    [SerializeField] private float _snapSpeed = 20f;
    [SerializeField] private GameObject _headBox;

    public bool _wearingBox = false;
    private Rigidbody _grabbedObject;
    private bool _grabPressed = false;
    private GameObject _wornBox;
    
    void FixedUpdate()
    {
        if (_grabbedObject)
        {
            _grabbedObject.velocity = (_holdPosition.position - _grabbedObject.transform.position) * _snapSpeed;
        }
    }

    private void OnGrab()
    {
        if (_grabPressed)
        {
            _grabPressed = false;
            
            if (!_grabbedObject && !_wearingBox)
            {
                return;
            }

            if (!_wearingBox)
            {
                DropGrabbedObject();
            }
            else
            {
                UnHideInBox();
            }
        }
        else
        {
            _grabPressed = true;
            
            if (Physics.Raycast(_cameraPosition.position, _cameraPosition.forward, out RaycastHit hit, _grabRange))
            {
                if (!hit.transform.gameObject.CompareTag("Grabbable") && !hit.transform.gameObject.CompareTag("Box"))
                {
                    return;
                }

                if (hit.transform.gameObject.CompareTag("Grabbable"))
                {
                    _grabbedObject = hit.transform.GetComponent<Rigidbody>();
                    _grabbedObject.transform.parent = _holdPosition;
                }
                else if (hit.transform.gameObject.CompareTag("Box"))
                {
                    HideInBox(hit);
                }
            }
        }
    }

    private void UnHideInBox()
    {
        _wornBox.SetActive(true);
        _wornBox.transform.position = transform.position + transform.forward * _grabRange;
        _wearingBox = false;
        _headBox.SetActive(false);
    }

    private void HideInBox(RaycastHit hit)
    {
        _wearingBox = true;
        _headBox.SetActive(true);
        _wornBox =  hit.transform.gameObject;
        _wornBox.SetActive(false);
    }

    private void DropGrabbedObject()
    {
        _grabbedObject.transform.parent = null;
        _grabbedObject = null;
    }

    private void OnThrow()
    {
        if (!_grabbedObject)
        {
            return;
        }
        
        _grabbedObject.AddForce(_cameraPosition.forward * _throwForce, ForceMode.Impulse);

        if (_grabbedObject.TryGetComponent(out ProximityMine proximityMine))
        {
            proximityMine.SetThrownStatus(true);
        }
        
        DropGrabbedObject();
    }
}