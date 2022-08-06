using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeHead : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("AxeWall"))
        {
            return;
        }
        
        _rigidbody.isKinematic = true;
    }

    private void OnTriggerExit(Collider other)
    {
        _rigidbody.isKinematic = false;
    }
}
