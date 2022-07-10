using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private float _impulseThreshold = 0.5f;

    private float _collisionTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_collisionTimer < 1f)
        {
            _collisionTimer += Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_collisionTimer < 1f)
        {
            return;
        }

        if (collision.impulse.magnitude > _impulseThreshold && collision.gameObject.TryGetComponent(out EnemyController enemyController))
        {
            enemyController.Explode();
        }
    }
}