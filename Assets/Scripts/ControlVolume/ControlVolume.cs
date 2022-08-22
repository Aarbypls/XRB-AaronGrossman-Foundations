using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlVolume : MonoBehaviour
{
    [SerializeField] private Bounds _bounds;
    [SerializeField] private Vector3 _boundSize;

    [SerializeField] public float _scaledValueX;
    [SerializeField] public float _scaledValueY;
    [SerializeField] public float _scaledValueZ;
    [SerializeField] private float _distanceFromCenter;
    
    [SerializeField] private bool _handInBounds = false;

    [SerializeField] public GameObject hand;
    
    [SerializeField] private Spaceship _spaceship;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateBounds();
        CheckBounds();

        if (_handInBounds)
        {
            _spaceship.isBeingControlled = true;
            
            UpdateScaledValues();
            SetTransformToUnscaledValues();

            _spaceship.gameObject.transform.rotation = hand.transform.rotation;
        }
        else
        {
            _spaceship.isBeingControlled = false;

            _scaledValueX = _scaledValueY = _scaledValueZ = 0f;
            
            _spaceship.gameObject.transform.rotation = Quaternion.identity;

        }
    }

    private void SetTransformToUnscaledValues()
    {
        //_spaceship.transform.position = new Vector3();
        
    }
    
    private void UpdateScaledValues()
    {
        _scaledValueX = GetScaledValue(hand.transform.position.x,
            transform.position.x,
            _bounds.extents.x,
            transform.position.x);
        
        _scaledValueY = GetScaledValue(hand.transform.position.y,
            transform.position.y,
            _bounds.extents.y,
            transform.position.y);
        
        _scaledValueZ = GetScaledValue(hand.transform.position.z,
            transform.position.z,
            _bounds.extents.z,
            transform.position.z);
    }

    private float GetScaledValue(float rawValue, float minValue, float maxValue, float centerWorldSpacePos = 0.0f)
    {
        float returnScaledValue = 0f;

        if (minValue == 0f)
        {
            returnScaledValue = rawValue / maxValue;
        }
        else
        {
            returnScaledValue = (rawValue - minValue) / (maxValue - minValue);
        }

        return returnScaledValue;
    }

    private void UpdateBounds()
    {
        _bounds = new Bounds(transform.position, _boundSize);
    }

    private void CheckBounds()
    {
        Vector3 handPosition = hand.transform.position;

        if (_bounds.Contains(handPosition))
        {
            _handInBounds = true;
        }
        else
        {
            _handInBounds = false;
        }
    }
}
