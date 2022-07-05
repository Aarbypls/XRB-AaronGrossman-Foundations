using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(AudioSource))]
public class SoundEmitter : MonoBehaviour
{
    [SerializeField] private float _soundRadius = 6.5f;
    [SerializeField] private float _impulseThreshold = 2f;
    
    private AudioSource _audioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.impulse.magnitude > _impulseThreshold || other.gameObject.CompareTag("Player"))
        {
            _audioSource.Play();
            
            Collider[] _colliders = Physics.OverlapSphere(transform.position, _soundRadius);

            foreach (var col in _colliders)
            {
                if (col.TryGetComponent(out EnemyController enemyController))
                {
                    float randForEnemyStateChange = Random.Range(1f, 100f);
                    
                    // TO-DO: Change the 2f number to be more realistic, this is for testing
                    if (randForEnemyStateChange < 2f)
                    {
                        enemyController.SetInvestigatePoint(transform.position);
                    }
                    else
                    {
                        if (!enemyController.FindPartnerForInvestigation(transform.position))
                        {
                            enemyController.SetInvestigatePoint(transform.position);
                        }
                    }
                }
            }   
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, _soundRadius);
    }
}
