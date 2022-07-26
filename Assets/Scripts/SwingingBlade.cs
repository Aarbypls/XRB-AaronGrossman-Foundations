using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.Mathematics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class SwingingBlade : MonoBehaviour
{
    [SerializeField] private Quaternion _targetAngle_0 = Quaternion.Euler(0,0,0);
    [SerializeField] private Quaternion _targetAngle_180 = Quaternion.Euler(0, 180,180);

    private float t = 0.0f;
    
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
        t = Mathf.PingPong(Time.time * 1f, 1.0f);
        transform.rotation = Quaternion.Slerp(_targetAngle_0, _targetAngle_180, t);
    }
}
