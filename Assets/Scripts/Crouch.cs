using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crouch : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private float _crouchHeight = 1;
    private float _originalHeight;
    private bool _crouched = false;
    
    // Start is called before the first frame update
    void Start()
    {
        _originalHeight = _characterController.height;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCrouch()
    {
        if (_crouched)
        {
            _crouched = false;
            _characterController.height = _originalHeight;
        }
        else
        {
            _crouched = true;
            _characterController.height = _crouchHeight;
        }
    }
}
