using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.XR.Interaction.Toolkit;

public class PushPlunger : MonoBehaviour
{
    [SerializeField] private GameObject _pushPoint;
    [SerializeField] private XRGrabInteractable _grabInteractable;
    [SerializeField] private float _magnitude = 1f;
    [SerializeField] private PullPlunger _pullPlunger;

    private List<GameObject> _objectsInRange = new List<GameObject>();
    private bool _pushing = false;
    private bool _pullPlungerIsPulling = false;

    // Start is called before the first frame update
    void Start()
    {
        Assert.IsNotNull(_grabInteractable, "You have not assigned a grab interactable " + name);

        _grabInteractable.activated.AddListener(StartPush);
        _grabInteractable.deactivated.AddListener(StopPush);
    }
    
    public void ChangePullPlungerStatus(bool isPullPlungerPulling)
    {
        _pullPlungerIsPulling = isPullPlungerPulling;
    }

    public List<GameObject> GetPushPlungerObjectsInRange()
    {
        return _objectsInRange;
    }

    private void StartPush(ActivateEventArgs arg0)
    {
        _pushing = true;
        _pullPlunger.ChangePushPlungerStatus(true);
        foreach (GameObject _object in _objectsInRange)
        {
            _object.GetComponent<Rigidbody>().useGravity = false;
        }
    }

    private void StopPush(DeactivateEventArgs arg0)
    {
        _pushing = false;
        _pullPlunger.ChangePushPlungerStatus(false);
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

            if (_pushing)
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
        }    }

    // Update is called once per frame
    void Update()
    {
        if (_pushing && !_pullPlungerIsPulling)
        {
            foreach (GameObject _object in _objectsInRange)
            {
                Vector3 directionVector = -(_pushPoint.transform.position - _object.transform.position);
                _object.GetComponent<Rigidbody>().AddForce(directionVector * _magnitude);
            }
        }
    }
}
