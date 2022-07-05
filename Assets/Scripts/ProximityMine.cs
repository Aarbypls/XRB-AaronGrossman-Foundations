using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityMine : MonoBehaviour
{
    [SerializeField] private float _explosionTriggerRadius = 2f;
    [SerializeField] private LayerMask _blockingLayers;
    [SerializeField] private ParticleSystem _explosion;
    private Rigidbody _rigidbody;
    private SphereCollider _sphereCollider;
    private bool _thrown;
    private bool _activated = false;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _sphereCollider = GetComponent<SphereCollider>();
    }
    
    // Update is called once per frame
    private void Update()
    {
        if (_activated)
        {
            Collider[] targetsInExplosionTriggerRadius =
                Physics.OverlapSphere(transform.position, _explosionTriggerRadius);
            
            foreach (var target in targetsInExplosionTriggerRadius)
            {
                if (target.TryGetComponent(out Creature targetCreature) && target.TryGetComponent(out EnemyController _enemyController))
                {
                    if (targetCreature.team == Creature.Team.Enemy)
                    {
                        Vector3 targetPosition = target.transform.position;
                        
                        if (Physics.Raycast(targetPosition,
                                (targetPosition - transform.position).normalized,
                                Vector3.Distance(transform.position, target.transform.position),
                                _blockingLayers))
                        {
                            continue;
                        }

                        _enemyController.enabled = false;
                        _explosion.gameObject.SetActive(true);
                    }
                }
                
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Collider[] _colliders = Physics.OverlapSphere(transform.position, _sphereCollider.radius);
            
            foreach (var col in _colliders)
            {
                if (col.gameObject.layer == 6 && _thrown)
                {
                    ActivateProximityMine();
                }
            }
    }

    private void ActivateProximityMine()
    {
        _activated = true;
        _rigidbody.isKinematic = true;
    }

    public void SetThrownStatus(bool thrown)
    {
        _thrown = thrown;
    }
}
