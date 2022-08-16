using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.XR.Interaction.Toolkit;

public class PullPlunger : MonoBehaviour
{
    [SerializeField] private GameObject _pullPoint;
    [SerializeField] private XRGrabInteractable _grabInteractable;
    [SerializeField] private float _magnitude = 1f;
    [SerializeField] private float _minimumDistance = .3f;
    [SerializeField] private PushPlunger _pushPlunger;

    private List<GameObject> _objectsInRange = new List<GameObject>();
    private bool _pulling = false;
    private bool _pushPlungerIsPushing = false;
    
    // Start is called before the first frame update
    void Start()
    {
        Assert.IsNotNull(_pullPoint, "You have not assigned a pull point to " + name);
        Assert.IsNotNull(_grabInteractable, "You have not assigned a grab interactable " + name);
        
        _grabInteractable.activated.AddListener(StartPull);
        _grabInteractable.deactivated.AddListener(StopPull);
    }
    
    public void ChangePushPlungerStatus(bool isPushPlungerPushing)
    {
        _pushPlungerIsPushing = isPushPlungerPushing;
    }

    private void StartPull(ActivateEventArgs arg0)
    {
        _pulling = true;
        _pushPlunger.ChangePullPlungerStatus(true);
        foreach (GameObject _object in _objectsInRange)
        {
            _object.GetComponent<Rigidbody>().useGravity = false;
        }
    }

    private void StopPull(DeactivateEventArgs arg0)
    {
        _pulling = false;
        _pushPlunger.ChangePullPlungerStatus(false);
        foreach (GameObject _object in _objectsInRange)
        {
            _object.GetComponent<Rigidbody>().useGravity = true;
        }    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_objectsInRange.Contains(other.gameObject) && other.gameObject.TryGetComponent(out Rigidbody rigidbody))
        {
            _objectsInRange.Add(other.gameObject);

            if (_pulling)
            {
                rigidbody.useGravity = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (_objectsInRange.Contains(other.gameObject) && other.gameObject.TryGetComponent(out Rigidbody rigidbody))
        {
            _objectsInRange.Remove(other.gameObject);
            rigidbody.useGravity = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_pulling && !_pushPlungerIsPushing)
        {
            foreach (GameObject _object in _objectsInRange)
            {
                if (Vector3.Distance(_pullPoint.transform.position, _object.transform.position) > _minimumDistance)
                {
                    Vector3 directionVector = (_pullPoint.transform.position - _object.transform.position);
                    _object.GetComponent<Rigidbody>().AddForce(directionVector * _magnitude);
                }
                else
                {
                    _object.GetComponent<Rigidbody>().velocity = Vector3.zero;
                }
            }
        }
        else if (_pulling && _pushPlungerIsPushing)
        {
            List<GameObject> allObjects = new List<GameObject>();
            allObjects.AddRange(_objectsInRange);
            allObjects.AddRange(_pushPlunger.GetPushPlungerObjectsInRange());
            allObjects = allObjects.Distinct().ToList();

            float totalX = 0f;
            float totalY = 0f;
            float totalZ = 0f;
            
            foreach (GameObject allObject in allObjects)
            {
                totalX += allObject.transform.position.x;
                totalY += allObject.transform.position.y;
                totalZ += allObject.transform.position.z;
            }

            Vector3 center = new Vector3(totalX / allObjects.Count, totalY / allObjects.Count,
                totalZ / allObjects.Count);

            foreach (GameObject allObject in allObjects)
            {
                if (Vector3.Distance(center, allObject.transform.position) > _minimumDistance)
                {
                    Vector3 directionVector = (center - allObject.transform.position);
                    allObject.GetComponent<Rigidbody>().AddForce(directionVector * _magnitude);
                }
                else
                {
                    allObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                }
            }
        }
    }
}
