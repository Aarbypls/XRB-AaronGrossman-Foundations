using System.Collections;
using System.Collections.Generic;
using Unity.XR.OpenVR;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class Gun : MonoBehaviour
{
    [SerializeField] private GameObject _shootPointObject;
    private XRBaseControllerInteractor _controller;
    private Vector3 _shootPoint;
    private int _currentPoints = 0;
    
    private 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _shootPoint = _shootPointObject.transform.position;
    }

    public void Shoot()
    {
        if (Physics.Raycast(_shootPoint, transform.forward, out RaycastHit hit, 100f))
        {
            if (hit.collider.gameObject.CompareTag("TargetReset"))
            {
                TargetHuntReset targetHuntReset = hit.collider.gameObject.GetComponent<TargetHuntReset>();
                _currentPoints = 0;
                targetHuntReset.ResetGame();
            }
            else if (hit.collider.gameObject.CompareTag("Target"))
            {
                Target target = hit.collider.gameObject.GetComponent<Target>();
                int points = target.Hit();
                _currentPoints += points;
                Debug.Log("Target Hit! Points: " + _currentPoints);
            }
        }
    }
}
