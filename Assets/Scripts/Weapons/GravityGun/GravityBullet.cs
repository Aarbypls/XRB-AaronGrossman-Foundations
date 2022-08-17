using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class GravityBullet : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _radius = 10f;
    [SerializeField] private GameObject _spitOutPoint;
    private int _bounceCount = 0;
    private bool _isGhost = false;
    private bool _bombInitiated = false;
    private List<GameObject> _bombedObjects = new List<GameObject>();
    private List<GameObject> _alreadyBombedObjects = new List<GameObject>();


    public void Init(Vector3 velocity, bool isGhost)
    {
        _isGhost = isGhost;
        _rb.AddForce(velocity, ForceMode.Impulse);
    }

    private void Start()
    {
        Destroy(gameObject, 6f);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (_isGhost)
        {
            return;
        }

        if (other.gameObject.layer == 6)
        {
            _bounceCount++;

            if (_bounceCount == 2)
            {
                InitiateGravityBomb();
            }
        }
    }

    private void InitiateGravityBomb()
    {
        Collider[] hitColliders = Physics.OverlapSphere(gameObject.transform.position, _radius);

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.GetComponent<Rigidbody>() && !hitCollider.gameObject.CompareTag("GravityBall"))
            {
                _bombedObjects.Add(hitCollider.gameObject);
                _bombInitiated = true;
            }
        }
    }

    private void GravityBombOnObject(GameObject bombedObject)
    {
        //bombedObject.transform.position = Vector3.Lerp(bombedObject.transform.position, transform.position, Time.deltaTime * 2f);
        
        
        Vector3 directionVector = (gameObject.transform.position - bombedObject.transform.position);
        bombedObject.GetComponent<Rigidbody>().AddForce(directionVector * 5f);
    }

    private void Update()
    {
        if (!_bombInitiated)
        {
            return;
        }

        foreach (GameObject bombedObject in _bombedObjects)
        {
            // if (_alreadyBombedObjects.Contains(bombedObject))
            // {
            //     return;
            // }

            if (Vector3.Distance(gameObject.transform.position, bombedObject.transform.position) > 1f)
            {
                GravityBombOnObject(bombedObject);
            }
            else
            {
                // _alreadyBombedObjects.Add(bombedObject);
                Rigidbody rb = GetComponent<Rigidbody>();
                bombedObject.transform.position = _spitOutPoint.transform.position;
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.down;
                _bombedObjects.Remove(bombedObject);
            }
        }
    }
}
