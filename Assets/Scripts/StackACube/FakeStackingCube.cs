using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeStackingCube : MonoBehaviour
{
    private Vector3 originalPosition;
    private Quaternion originalRotation;

    private void Start()
    {
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    public void Reset()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
        transform.rotation = originalRotation;
        transform.position = originalPosition;
    }
}
